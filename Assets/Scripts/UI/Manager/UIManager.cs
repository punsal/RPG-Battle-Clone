using System;
using System.Collections.Generic;
using Data.Character;
using EventArguments;
using UI.Controller;
using UI.Hero_Item_UI.Manager;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.System.Publisher_Subscriber_System;

namespace UI.Manager
{
    public class UIManager : MonoBehaviour
    {
#pragma warning disable 649
        
        [Header("UI Controllers")]
        [SerializeField] private List<UIController> uiControllers;

        [FormerlySerializedAs("heroItemManager")]
        [Header("View Management")]
        [SerializeField] private HeroItemUIManager heroItemUIManager;

        [Header("Data Models")]
        [SerializeField] private List<HeroModel> heroModels;
        
#pragma warning restore 649
        
        private Subscription<GameEventType> gameEventSubscription;

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
            foreach (var uiController in uiControllers)
            {
                uiController.gameObject.SetActive(false);
            }
        }

        private void GameEventHandler(GameEventType gameEventType)
        {
            foreach (var uiController in uiControllers)
            {
                uiController.gameObject.SetActive(uiController.gameEventType == gameEventType);
            }

            switch (gameEventType)
            {
                case GameEventType.StartGame:
                    break;
                case GameEventType.SelectHero:
                    SelectHero();
                    break;
                case GameEventType.StartBattle:
                    break;
                case GameEventType.PlayerTurn:
                    break;
                case GameEventType.PlayerFinishedTurn:
                    break;
                case GameEventType.EnemyTurn:
                    break;
                case GameEventType.EnemyFinishedTurn:
                    break;
                case GameEventType.PlayerWon:
                    break;
                case GameEventType.PlayerDefeated:
                    break;
                case GameEventType.EndBattle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameEventType), gameEventType, null);
            }
        }

        private void SelectHero()
        {
            var availableHeroModels = new List<HeroModel>();
            foreach (var heroModel in heroModels)
            {
                if (heroModel.IsAvailable)
                {
                    availableHeroModels.Add(heroModel);
                }
            }

            heroItemUIManager.Construct(availableHeroModels);
        }
    }
}
