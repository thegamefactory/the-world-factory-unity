using TWF;

public static class Root
{
    public static GameService GameService { get; set; } = new GameService();

    public static IWorldView WorldView => GameService.GetWorldView();
}
