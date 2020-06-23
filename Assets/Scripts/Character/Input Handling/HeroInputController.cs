using Character.Input_Handling.Abstract;
using EventArguments;
using UnityEngine;
using Utility.System.Publisher_Subscriber_System;

namespace Character.Input_Handling
{
    public class HeroInputController : InputController
    {
#pragma warning disable 649
        
        [SerializeField] private HeroController heroController;

#pragma warning restore 649
        
        private bool isPlayerTurn;
        
        private Subscription<GameEventType> gameEventSubscription;

        private void OnEnable()
        {
            gameEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(GameEventHandler);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(gameEventSubscription);
        }

        private void GameEventHandler(GameEventType gameEventType)
        {
            if (gameEventType == GameEventType.PlayerTurn || gameEventType ==  GameEventType.EnemyFinishedTurn)
            {
                isPlayerTurn = true;
            }
            else
            {
                isPlayerTurn = false;
            }
        }

        private void OnMouseDown()
        {
            if (!isPlayerTurn) return;
            StartInput();
        }

        private void OnMouseUp()
        {
            if (!isPlayerTurn) return;
            EndInput();
        }

        protected override void OnClick()
        {
            heroController.Attack();
        }

        protected override void OnHold()
        {
            //Show a popup
            heroController.ShowInfo();
        }
    }
}
