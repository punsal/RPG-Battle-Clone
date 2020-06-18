namespace State
{
    public class GameStateModel
    {
        private static GameState State;

        public GameStateModel()
        {
            State = GameState.GameStart;
        }
        
        public static GameState GetGameState() => State;
        public static void SetGameState(GameState state) => State = state;
    }
}