namespace TWF
{
    /// <summary>
    /// A tile generator which creates an uniform map of the terrain type given at construction.
    /// </summary>
    public class UniformMapGenerator : ITerrainGenerator
    {
        private Terrain terrain;

        /// <param name="terrain">The type of terrain that this generator create.</param>
        public UniformMapGenerator(Terrain terrain)
        {
            this.terrain = terrain;
        }

        public IMap<Terrain> Generate(Vector size)
        {
            Terrain[,] tiles = new Terrain[size.X, size.Y];
            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    tiles[x, y] = terrain;
                }
            }
            return new ArrayMap<Terrain>(tiles);
        }
    }
}
