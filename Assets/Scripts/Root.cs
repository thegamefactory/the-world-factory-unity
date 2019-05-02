using TWF;

public static class Root
{
    public static GameService GameService { get; set; } = GameServiceFactory.Create(1, 1);
}
