using System;
using TWF;

public static class Root
{
    public static GameService GameService { get; set; } = new GameService(
        WorldFactory.Create(
            new Vector(1, 1),
            new UniformMapGenerator(Terrain.LAND),
            new Random()
            ));

    public static IWorldView WorldView
    {
        get
        {
            return GameService.GetWorldView();
        }
    }
}
