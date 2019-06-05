using System;

namespace TWF
{
    public class WorldFactory
    {
        public Vector Size { get; set; } = new Vector(1, 1);

        public ITerrainGenerator TerrainGenerator { get; set; } = new UniformMapGenerator(Terrains.LAND);

        public WorldConfigFactory WorldConfigFactory { get; set; } = new WorldConfigFactory();

        public Random Random { get; set; } = new Random();

        /// <summary>
        /// Return a list of tuples (IAgent, float), where float is the period in seconds at which the agent should be executed.
        /// </summary>
        public World Create()
        {
            TwfDebug.Assert(Size.X > 0, "size.X must be positive");
            TwfDebug.Assert(Size.Y > 0, "size.Y must be positive");

            WorldConfig worldConfig = WorldConfigFactory.CreateDefaultWorldConfig(Random);

            var terrainMap = TerrainGenerator.GenerateTerrainMap(worldConfig, Size);
            var zoneMap = new ArrayMap<int>(Size, worldConfig.Zones[Zones.EMPTY]);
            var buildingMap = new ArrayMap<Building>(Size, null);

            Maps maps = new Maps(Size);
            terrainMap.RegisterTerrain(maps);
            zoneMap.RegisterZone(maps);
            buildingMap.RegisterBuilding(maps);

            return new World(maps, worldConfig, new Ticker());
        }
    }
}
