namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A tile generator which creates an uniform map of the terrain type given at construction.
    /// </summary>
    public class UniformMapGenerator : ITerrainGenerator
    {
        private readonly string terrain;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniformMapGenerator"/> class.
        /// </summary>
        /// <param name="terrain">The type of terrain that this generator create.</param>
        public UniformMapGenerator(string terrain)
        {
            this.terrain = terrain;
        }

        public IMap<int> GenerateTerrainMap(IWorldRules worldConfig, Vector size)
        {
            Contract.Requires(worldConfig != null);

            int terrainId = worldConfig.Terrains[this.terrain];
            int[,] tiles = new int[size.X, size.Y];
            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    tiles[x, y] = terrainId;
                }
            }

            return new ArrayMap<int>(tiles);
        }
    }
}
