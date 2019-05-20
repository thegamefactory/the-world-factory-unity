using TWF;
using TWF.Map.Tile;
using TWF.Generation;

public static class Root
{
    public static GameService GameService { get; set; } = GameServiceFactory.Create(
        WorldFactory.Create(
            new Vector(1, 1),
            new UniformMapGenerator(TileTerrain.LAND),
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
