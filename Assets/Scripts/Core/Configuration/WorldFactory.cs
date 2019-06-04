using System;

namespace TWF
{
    public static class WorldFactory
    {
        /// <summary>
        /// Return a list of tuples (IAgent, float), where float is the period in seconds at which the agent should be executed.
        /// </summary>
        public static World Create(Vector size, ITerrainGenerator terrainGenerator, WorldConfigFactory worldConfigFactory, Random random)
        {
            TwfDebug.Assert(size.X > 0, "size.X must be positive");
            TwfDebug.Assert(size.Y > 0, "size.Y must be positive");

            WorldConfig worldConfig = worldConfigFactory.CreateDefaultWorldConfig(random);

            var terrainMap = terrainGenerator.GenerateTerrainMap(worldConfig, size);
            var zoneMap = new ArrayMap<int>(size, worldConfig.Zones[Zones.EMPTY]);
            var buildingMap = new ArrayMap<Building>(size, null);

            Maps maps = new Maps(size);
            terrainMap.RegisterTerrain(maps);
            zoneMap.RegisterZone(maps);
            buildingMap.RegisterBuilding(maps);

            return new World(maps, worldConfig, new Ticker());
        }
    }
}
