using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character.Data;
using Data.Character;
using Data.Character.Abstract;
using DG.Tweening;
using EventArguments;
using TMPro;
using UI.Popup_UI.Fading;
using UI.Popup_UI.Information;
using UnityEngine;
using Utility.System.Object_Pooler_System;
using Utility.System.Publisher_Subscriber_System;
using Utility.System.Time_System;
using Utility.System.Time_System.Data;
using Utility.System.Time_System.Type;
using Utility.UI.Progress_Bar.Data;
using Utility.UI.Progress_Bar.Type;
using CharacterController = Character.Abstract.CharacterController;

namespace Character
{
    public class HeroController : CharacterController
    {
#pragma warning disable 649
        
        [Header("Prefab")]
        [SerializeField] private InfoPopupUIController infoPopupUIControllerPrefab;
        
#pragma warning restore 649
        
        public HeroModel HeroModel { get; private set; }
        
        private EnemyController enemyController;

        private Vector3 initialPosition;
        private Vector3 enemyPosition;

        private static List<ShowUpgradeFlag> Flags;
        
        private Subscription<GameEventType> gameEventSubscription;
        
        private void OnEnable()
        {
            Flags = new List<ShowUpgradeFlag>();
            
            gameEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(GameEventHandler);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(gameEventSubscription);
        }

        private void GameEventHandler(GameEventType gameEventType)
        {
            if (gameEventType == GameEventType.PlayerWon)
            {
                HeroModel.AddExperience();
                //Show some UI
                Flags.Add(new ShowUpgradeFlag
                {
                    HeroController = this,
                    IsStillShowing = true
                });

                StartCoroutine(ShowUpgrades());
            }
        }
        
        public override void Construct(CharacterModel model, bool isLoad = false)
        {
            HeroModel = model as HeroModel;

            if (HeroModel == null)
            {
                Debug.Log($"HeroModel could not resolve in HeroController");
                return;
            }
            
            if (!isLoad)
            {
                HeroModel.CurrentHealth = HeroModel.Health;
            }

            gameObject.name = HeroModel.name;
            
            characterGraphicsController.Construct(HeroModel.Sprite);
            
            healthBarController.Construct(HeroModel.name);
            PublisherSubscriber.Publish(new ProgressBarData 
            {
                Id = HeroModel.name, 
                BarType = ProgressBarType.HealthProgression, 
                TotalAmount = HeroModel.Health,
                CurrentAmount = HeroModel.CurrentHealth
            });
        }

        public override void TakeDamage(float attackPower)
        {
            HeroModel.CurrentHealth -= attackPower;
            PublisherSubscriber.Publish(new ProgressBarData 
            {
                Id = HeroModel.name, 
                BarType = ProgressBarType.HealthProgression, 
                TotalAmount = HeroModel.Health,
                CurrentAmount = HeroModel.CurrentHealth
            });

            var popup = GetFadingPopup();
            popup.Construct(-1f * attackPower, transform.position);

            if (HeroModel.CurrentHealth <= 0f)
            {
                gameObject.SetActive(false);
            }
        }

        public override void Attack()
        {
            initialPosition = transform.position;
            enemyController = FindObjectOfType<EnemyController>();
            enemyPosition = enemyController.transform.position;

            TimeSystem.CreateTimer(
                $"{HeroModel.name}'s Attack",
                1f,
                new[]
                {
                    new TimerActionData
                    {
                        ActionType = TimerActionType.OnStart,
                        TimerAction = ExecuteTurn
                    }
                }
            );
        }

        public void ShowInfo()
        {
            var infoPopupUIController = Instantiate(infoPopupUIControllerPrefab);
            infoPopupUIController.Construct(HeroModel);
        }

        private IEnumerator ShowUpgrades()
        {
            var position = transform.position;
            var wait = new WaitForSeconds(3f);

            var controller = GetFadingPopup();
            controller.Construct($"Exp +{HeroModel.UpgradeData.experience}", position);

            yield return wait;

            if (HeroModel.UpgradeData.attackPower > 0f)
            {
                controller = GetFadingPopup();
                controller.Construct($"AttP +{HeroModel.UpgradeData.attackPower:F2}", position);
            }
            else
            {
                RemoveFlag();
                CheckFlags();
                
                yield break;
            }

            yield return wait;
            
            if (HeroModel.UpgradeData.health > 0f)
            {
                controller = GetFadingPopup();
                controller.Construct($"HP +{HeroModel.UpgradeData.health:F2}", position);
            }
            else
            {
                RemoveFlag();
                CheckFlags();
                
                yield break;
            }

            yield return wait;
            
            RemoveFlag();
            CheckFlags();
        }

        private void RemoveFlag()
        {
            for (var i = 0; i < Flags.Count; i++)
            {
                var flag = Flags[i];

                if (flag.HeroController != this) continue;
                flag.IsStillShowing = false;
                Flags[i] = flag;
            }
        }

        private static void CheckFlags()
        {
            var isStillShowing = false;

            foreach (var flag in Flags.Where(flag => flag.IsStillShowing))
            {
                isStillShowing = true;
            }

            if (isStillShowing) return;
            PublisherSubscriber.Publish(GameEventType.EndBattle);
        }

        private void ExecuteTurn()
        {
            transform.DOMove(enemyPosition, 0.5f).OnComplete(() =>
            {
                enemyController.TakeDamage(HeroModel.AttackPower);
                transform.DOMove(initialPosition, 0.5f).OnComplete(() =>
                {
                    PublisherSubscriber.Publish(GameEventType.PlayerFinishedTurn);
                });
            });
        }
    }
}
