using System.Collections.Generic;
using System.Linq;
using Data.Character;
using Data.Character.Abstract;
using DG.Tweening;
using EventArguments;
using UnityEngine;
using Utility.System.Publisher_Subscriber_System;
using Utility.System.Time_System;
using Utility.System.Time_System.Data;
using Utility.System.Time_System.Type;
using Utility.UI.Progress_Bar.Data;
using Utility.UI.Progress_Bar.Type;
using CharacterController = Character.Abstract.CharacterController;
using Random = UnityEngine.Random;

namespace Character
{
    public class EnemyController : CharacterController
    {
        private EnemyModel enemyModel;
        private List<HeroController> heroControllers;

        public override void Construct(CharacterModel model, bool isLoad = false)
        {
            heroControllers = FindObjectsOfType<HeroController>().ToList();
            var heroModels = heroControllers.Select(controller => controller.HeroModel).ToList();
            
            enemyModel = model as EnemyModel;
            
            if (enemyModel == null)
            {
                Debug.Log("Enemy Model could not resolve in EnemyController");
                return;
            }
            
            if (!isLoad)
            {
                enemyModel.Construct(heroModels);
                enemyModel.CurrentHealth = enemyModel.Health;
            }

            characterGraphicsController.Construct(enemyModel.Sprite);
            
            healthBarController.Construct(enemyModel.name);
            PublisherSubscriber.Publish(new ProgressBarData
            {
                Id = enemyModel.name,
                BarType = ProgressBarType.HealthProgression,
                TotalAmount = enemyModel.Health,
                CurrentAmount = enemyModel.CurrentHealth
            });
        }
        
        public override void TakeDamage(float attackPower)
        {
            enemyModel.CurrentHealth -= attackPower;
            PublisherSubscriber.Publish(new ProgressBarData
            {
                Id = enemyModel.name,
                BarType = ProgressBarType.HealthProgression,
                TotalAmount = enemyModel.Health,
                CurrentAmount = enemyModel.CurrentHealth
            });

            var popup = GetFadingPopup();
            popup.Construct(-1f * attackPower, transform.position);
            
            if (enemyModel.CurrentHealth <= 0f)
            {
                gameObject.SetActive(false);
            }
        }

        public override void Attack()
        {
            var initialPosition = transform.position;

            var randomIndex = Random.Range(0, heroControllers.Count);
            var selectedHeroController = heroControllers[randomIndex];
            while (selectedHeroController.HeroModel.CurrentHealth <= 0f)
            { 
                heroControllers.RemoveAt(randomIndex);
                randomIndex = Random.Range(0, heroControllers.Count);
                selectedHeroController = heroControllers[randomIndex];
            }
            
            TimeSystem.CreateTimer(
                $"{enemyModel.name}'s Attack",
                1f,
                new []
                {
                    new TimerActionData
                    {
                        ActionType = TimerActionType.OnStart,
                        TimerAction = () =>
                        {
                            transform.DOMove(selectedHeroController.transform.position, 0.5f).OnComplete(() =>
                            {
                                selectedHeroController.TakeDamage(enemyModel.AttackPower);
                                transform.DOMove(initialPosition, 0.5f).OnComplete(() =>
                                {
                                    PublisherSubscriber.Publish(GameEventType.EnemyFinishedTurn);
                                });
                            });
                        }
                    } 
                }
            );
        }
    }
}