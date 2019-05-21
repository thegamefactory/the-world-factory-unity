using System;

namespace TWF
{
    public static class WorldFactory
    {
        /// <summary>
        /// Return a list of tuples (IAgent, float), where float is the period in seconds at which the agent should be executed.
        /// </summary>
        public static World Create(Vector size, ITerrainGenerator terrainGenerator, Random random)
        {
            var terrainMap = terrainGenerator.Generate(size);
            var zoneMap = new ArrayMap<Zone>(size, Zone.EMPTY);
            var buildingMap = new ArrayMap<Building>(size, null);

            var maps = new Maps(size);
            terrainMap.Register(maps);
            zoneMap.Register(maps);
            buildingMap.Register(maps);

            return new World(maps, new Ticker());
        }
    }
}
