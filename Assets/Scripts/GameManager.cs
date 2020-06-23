using System;
using System.Collections.Generic;
using System.Linq;
using Character;
using Data.Character;
using Data.Game;
using EventArguments;
using UnityEngine;
using Utility.System.Publisher_Subscriber_System;

public class GameManager : MonoBehaviour
{
#pragma warning disable 649
    
    #region Dependencies
    
    [Header("GameData")]
    [SerializeField] private GameData gameData;
    
    [Header("Data")] 
    [SerializeField] private EnemyModel enemyModel; 
    [SerializeField] private List<HeroModel> heroModels;
    
    #endregion

    #region Attributes
    
    [Header("Prefabs")]
    [SerializeField] private HeroController heroControllerPrefab;
    [SerializeField] private EnemyController enemyControllerPrefab;

    [Header("Positions")]
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private List<Transform> heroTransforms;
    
#pragma warning restore 649
    
    private List<HeroController> heroControllers;
    private EnemyController enemyController;

    private Subscription<GameEventType> gameEventSubscription;

    #endregion

    #region UnityEngine Methods

    private void OnEnable()
    {
        gameEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(GameEventHandler);
    }

    private void OnDisable()
    {
        PublisherSubscriber.Unsubscribe(gameEventSubscription);
    }

    private void Start()
    {
        CheckGameState();
    }

    #endregion

    
    private void CheckGameState()
    {
        switch (gameData.CurrentGameEventType)
        {
            case GameEventType.StartGame:
                InitializeGame();
                break;
            case GameEventType.SelectHero:
                InitializeHeroModels();
                gameData.SetGameEvent(GameEventType.SelectHero);
                break;
            case GameEventType.StartBattle:
                StartBattle();
                break;
            case GameEventType.EnemyFinishedTurn:
            //GOTO PlayerTurn
            case GameEventType.PlayerTurn:
                LoadBattle(GameEventType.PlayerTurn);
                break;
            case GameEventType.PlayerFinishedTurn:
            //GOTO EnemyTurn
            case GameEventType.EnemyTurn:
                //same with PlayerTurn procedure but continue with enemyTurn
                LoadBattle(GameEventType.EnemyTurn);
                break;
            case GameEventType.PlayerWon:
                //Show UI with Won! text
                gameData.battleResult = "Win!";
                gameData.SetGameEvent(GameEventType.EndBattle);
                break;
            case GameEventType.PlayerDefeated:
                //Show UI with Defeat! text
                gameData.battleResult = "Defeat!";
                gameData.SetGameEvent(GameEventType.EndBattle);
                break;
            case GameEventType.EndBattle:
                //Do nothing ui will handle;
                gameData.SetGameEvent(GameEventType.EndBattle);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void InitializeGame()
    {
        //Game is running first time;
        //set game loop count to zero
        gameData.gameLoopCount = 0;
        
        //set initial heroModel availability
        InitializeHeroModels();
        
        //GOTO SelectHero to actually start game
        gameData.SetGameEvent(GameEventType.SelectHero);    
    }

    private void InitializeHeroModels()
    {
        //load hero data
        for (var i = 0; i < heroModels.Count; i++)
        {
            heroModels[i].ResetModel();
            heroModels[i].IsAvailable = i < gameData.InitialAvailableHeroModelCount;
            heroModels[i].IsSelected = false;
        }
        //Open selection ui -> ui Manager does it
        gameData.SetGameEvent(GameEventType.SelectHero);
    }

    private void GameEventHandler(GameEventType gameEventType)
    {
        switch (gameEventType)
        {
            case GameEventType.StartGame:
                gameData.SetGameEvent(GameEventType.SelectHero);
                break;
            case GameEventType.SelectHero:
                //Do nothing.
                break;
            case GameEventType.StartBattle:
                StartBattle();
                break;
            case GameEventType.PlayerTurn:
                CheckHeroesState();
                break;
            case GameEventType.PlayerFinishedTurn:
                gameData.SetGameEvent(GameEventType.EnemyTurn);
                break;
            case GameEventType.EnemyTurn:
                if (CheckEnemyState()) break;
                enemyController.Attack();
                break;
            case GameEventType.EnemyFinishedTurn:
                gameData.SetGameEvent(GameEventType.PlayerTurn);
                break;
            case GameEventType.PlayerWon:
                gameData.battleResult = "Win!";
                break;
            case GameEventType.PlayerDefeated:
                gameData.battleResult = "defeat!";
                gameData.SetGameEvent(GameEventType.EndBattle);
                break;
            case GameEventType.EndBattle:
                EndBattle();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameEventType), gameEventType, null);
        }
    }

    private void StartBattle()
    {
        //find selected heroes put them in battlefield
        var selectedHeroes = heroModels.Where(heroModel => heroModel.IsSelected).ToList();
        InitializeHeroes(selectedHeroes);

        InitializeEnemy();

        gameData.SetGameEvent(GameEventType.PlayerTurn);
    }

    private void LoadBattle(GameEventType gameEventType)
    {
        //find last selected heroes, put them in battlefield
        //check if there is a dead hero
        var selectedHeroes = heroModels.Where(model => model.IsSelected).ToList();
        for (var i = 0; i < selectedHeroes.Count; i++)
        {
            var selectedHero = selectedHeroes[i];
            if (selectedHero.CurrentHealth <= 0f)
            {
                selectedHeroes.RemoveAt(i--);
            }
        }

        //if all are dead return to playerDefeated
        if (selectedHeroes.Count < 1)
        {
            gameData.SetGameEvent(GameEventType.PlayerDefeated);
            return;
        }

        //load heroes currentHealths
        InitializeHeroes(selectedHeroes, true);

        //create enemy
        //if enemy is dead return to playerWon
        if (enemyModel.CurrentHealth <= 0f)
        {
            gameData.SetGameEvent(GameEventType.PlayerWon);
            return;
        }

        //load its currentHealth from data
        InitializeEnemy(true);
        
        //if anyone in each side stand still then continue with playerTurn
        gameData.SetGameEvent(gameEventType);
    }

    private void InitializeEnemy(bool isLoad = false)
    {
        //create enemy
        enemyController = Instantiate(enemyControllerPrefab);
        enemyController.Construct(enemyModel, isLoad);
        
        PutEnemyInBattlefield();
    }

    private void InitializeHeroes(IEnumerable<HeroModel> selectedHeroes, bool isLoad = false)
    {
        heroControllers = new List<HeroController>();
        foreach (var selectedHero in selectedHeroes)
        {
            var tempController = Instantiate(heroControllerPrefab);
            tempController.Construct(selectedHero, isLoad);

            heroControllers.Add(tempController);
        }

        PutHeroesInBattlefield();
    }

    private void PutHeroesInBattlefield()
    {
        for (var i = 0; i < heroControllers.Count; i++)
        {
            var heroController = heroControllers[i];
            var heroTransform = heroTransforms[i];

            heroController.transform.position = heroTransform.position;
        }
    }

    private void PutEnemyInBattlefield()
    {
        enemyController.transform.position = enemyTransform.position;
    }

    private void CheckHeroesState()
    {
        var isPlayerDefeated = true;
        foreach (var heroController in heroControllers)
        {
            if (heroController.HeroModel.CurrentHealth > 0f)
            {
                isPlayerDefeated = false;
            }
        }

        if (isPlayerDefeated)
        {
            gameData.SetGameEvent(GameEventType.PlayerDefeated);
        }
    }

    private bool CheckEnemyState()
    {
        if (!enemyController.gameObject.activeInHierarchy)
        {
            gameData.SetGameEvent(GameEventType.PlayerWon);
            return true;
        }

        return false;
    }

    private void EndBattle()
    {
        foreach (var heroController in heroControllers)
        {
            Destroy(heroController.gameObject);
        }

        heroControllers.Clear();

        Destroy(enemyController.gameObject);
        enemyController = null;

        //Clear selection data
        foreach (var heroModel in heroModels)
        {
            heroModel.IsSelected = false;
        }

        //add game loop
        gameData.gameLoopCount++;

        //check new availability
        for (var i = 0; i < heroModels.Count; i++)
        {
            var heroModel = heroModels[i];
            heroModel.IsAvailable = i < gameData.InitialAvailableHeroModelCount;
        }
    }
}
