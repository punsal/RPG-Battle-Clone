using EventArguments;
using UnityEngine;
using Utility.System.Publisher_Subscriber_System;

namespace Data.Game
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Data/Game Data", order = 0)]
    public class GameData : ScriptableObject
    {
        [Header("Game State")]
        [SerializeField] private GameEventType currentGameEventType;
        public GameEventType CurrentGameEventType => currentGameEventType;
        
        [Header("Game Counter")]
        public int gameLoopCount;
        
        [Header("Battle")]
        public string battleResult = "Who Won?";
        
        public int InitialAvailableHeroModelCount => 3 + gameLoopCount / 5;

        public void SetGameEvent(GameEventType gameEventType)
        {
            currentGameEventType = gameEventType;
            PublisherSubscriber.Publish(gameEventType);
        }

        public void StartBattleEvent()
        {
            SetGameEvent(GameEventType.StartBattle);
        }

        public void SelectHeroesEvent()
        {
            SetGameEvent(GameEventType.SelectHero);
        }
    }
}