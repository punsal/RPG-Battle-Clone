using Battlefield;
using Hero;
using UI.Selection;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private HeroSelectionManager heroSelectionManager;
    [SerializeField] private Button buttonBattle;

    [Header("Managers")] 
    [SerializeField] private BattlefieldManager battlefieldManager;

    [Header("Game State")]
    [SerializeField] private bool isBattleStarted;
    
    private HeroManager heroManager;
    private Enemy enemy;

    #region UnityEngine Meyhods

    private void OnEnable()
    {
        buttonBattle.onClick.AddListener(StartBattle);
    }

    private void OnDisable()
    {
        buttonBattle.onClick.RemoveListener(StartBattle);
    }

    private void Start()
    {
        InitializeHeroManager();
        SetHeroesPosition();
        ActivateHeroes();
        
        heroSelectionManager.Construct(heroManager);
    }

    private void Update()
    {
        if (isBattleStarted)
        {
            CheckBattlefield();
        }
    }

    #endregion

    #region HeroManager Methods

    private void InitializeHeroManager()
    {
        heroManager = new HeroManager();
    }

    private void SetHeroesPosition()
    {
        var startPosition = Vector3.zero;
        for (var rowIndex = 0; rowIndex < 2; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < 5; columnIndex++)
            {
                var position = startPosition + (Vector3.right * columnIndex + Vector3.down * rowIndex) * 1.5f;
                heroManager.Heroes[rowIndex * 5 + columnIndex].Move(position);
            }
        }
    }

    private void ActivateHeroes()
    {
        const int activeCount = 3;
        for (var i = 0; i < heroManager.Heroes.Count; i++)
        {
            var hero = heroManager.Heroes[i];
            hero.IsAvailable = i < activeCount;
            hero.SetActive(false);
        }
    }

    #endregion

    #region Batlle Methods

    private void StartBattle()
    {
        #region Update UI

        heroSelectionManager.gameObject.SetActive(false);
        buttonBattle.gameObject.SetActive(false);

        #endregion
        
        #region Initialize Enemy

        enemy = new Enemy(heroManager.SelectedHeroes);
        
        battlefieldManager.PutEnemyInBattlefield(enemy);
        
        #endregion
        
        #region Initialize Heroes

        foreach (var selectedHero in heroManager.SelectedHeroes)
        {
            selectedHero.SetActive(true);
            selectedHero.PrepareForBattle(enemy);
        }

        battlefieldManager.PutHeroesInBattlefield(heroManager.SelectedHeroes);

        #endregion

        #region Change Game State

        isBattleStarted = true;

        #endregion
    }

    private void CheckBattlefield()
    {
        var isHeroesDead = true;
        foreach (var selectedHero in heroManager.SelectedHeroes)
        {
            if (selectedHero.IsActive)
            {
                isHeroesDead = false;
            }
        }

        if (isHeroesDead)
        {
            Debug.Log("Player defeated.");
            isBattleStarted = false;
            return;
        }

        if (!enemy.IsActive)
        {
            isBattleStarted = false;
            Debug.Log("Player won.");
        }
    }

    private void EndBattle()
    {
        
    }

    #endregion
}
