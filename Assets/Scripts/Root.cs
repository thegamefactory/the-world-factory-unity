using System;
using TWF;

public static class Root
{
    public static Vector Size { get; set; } = new Vector(1, 1);

    public static ITerrainGenerator TerrainGenerator { get; set; } = new UniformMapGenerator(Terrains.LAND);

    public static WorldConfigFactory WorldConfigFactory { get; set; } = new WorldConfigFactory();

    public static Random Random { get; set; } = new Random();

    public static GameService GameService { get; set; } = new GameService(
    WorldFactory.Create(
        Size,
        new UniformMapGenerator(Terrains.LAND),
        new WorldConfigFactory(),
        new Random()
        ));

    public static IWorldView WorldView => GameService.GetWorldView();

    public static void Recreate()
    {
        GameService = new GameService(
            WorldFactory.Create(
                Size,
                TerrainGenerator,
                WorldConfigFactory,
                Random
                ));
    }
}
