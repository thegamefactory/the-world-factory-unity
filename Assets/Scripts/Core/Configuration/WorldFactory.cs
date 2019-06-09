namespace TWF
{
    using System;
    using System.Diagnostics.Contracts;

    public class WorldFactory
    {
        public Vector Size { get; set; } = new Vector(1, 1);

        public ITerrainGenerator TerrainGenerator { get; set; } = new UniformMapGenerator(Terrains.Land);

        public WorldRulesFactory WorldConfigFactory { get; set; } = new WorldRulesFactory();

        public Random Random { get; set; } = new Random();

        /// <summary>
        /// Return a list of tuples (IAgent, float), where float is the period in seconds at which the agent should be executed.
        /// </summary>
        public World Create()
        {
            Contract.Requires(this.Size.X > 0, "size.X must be positive");
            Contract.Requires(this.Size.Y > 0, "size.Y must be positive");

            WorldRules worldConfig = this.WorldConfigFactory.Create(this.Random);

            var terrainMap = this.TerrainGenerator.GenerateTerrainMap(worldConfig, this.Size);
            var zoneMap = new ArrayMap<int>(this.Size, worldConfig.Zones[Zones.EMPTY]);
            var buildingMap = new ArrayMap<Building>(this.Size, null);

            Maps maps = new Maps(this.Size);
            terrainMap.RegisterTerrain(maps);
            zoneMap.RegisterZone(maps);
            buildingMap.RegisterBuilding(maps);

            return new World(maps, worldConfig, new Ticker());
        }
    }
}
