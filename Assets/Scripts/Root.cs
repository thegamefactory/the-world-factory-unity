using TWF;
using TWF.Map;
using TWF.Terrain;

public static class Root
{
    public static GameService GameService { get; set; } = GameServiceFactory.Create(
        GameFactory.Create(
            new Vector(1, 1),
            new UniformMapGenerator(TileTerrain.LAND),
            new System.Random()
            ),
        new System.Random());

    public static IGameStateView GameState
    {
        get
        {
            return GameService;
        }
    }
}
