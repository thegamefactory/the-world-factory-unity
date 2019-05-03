using TWF;

public static class Root
{
    public static GameService GameService { get; set; } = GameFactory.Create(1, 1);

    public static IGameState GameState
    {
        get
        {
            return GameService;
        }
    }
}
