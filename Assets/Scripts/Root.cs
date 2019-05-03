using TWF;
using TWF.Map;

public static class Root
{
    public static GameService GameService { get; set; } = GameFactory.Create(
        new Vector(1, 1),
        new TWF.Terrain.UniformMapGenerator(Tile.TileTerrain.LAND),
        new System.Random());

    public static IGameState GameState
    {
        get
        {
            return GameService;
        }
    }
}
