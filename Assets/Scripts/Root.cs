using TWF;

public static class Root
{
    public static GameService GameService { get; set; } = GameServiceFactory.Create(
        WorldFactory.Create(
            new Vector(1, 1),
            new UniformMapGenerator(Terrain.LAND),
            new System.Random()
            ),
        new System.Random());

    public static IWorldView WorldView
    {
        get
        {
            return GameService;
        }
    }
}
