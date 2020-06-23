namespace EventArguments
{
    public enum GameEventType
    {
        StartGame,
        SelectHero,
        StartBattle,
        //Turn-based
        PlayerTurn,
        PlayerFinishedTurn,
        EnemyTurn,
        EnemyFinishedTurn,
        //Battle Result
        PlayerWon,
        PlayerDefeated,
        EndBattle,
    }
}
