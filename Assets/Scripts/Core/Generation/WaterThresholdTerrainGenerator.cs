namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A tile generator which considers all the noise below a certain threshold as water and the rest land.
    /// </summary>
    public class WaterThresholdTerrainGenerator : ITerrainGenerator
    {
        private readonly INoiseGenerator noiseGenerator;
        private readonly float waterThreshold;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaterThresholdTerrainGenerator"/> class.
        /// </summary>
        /// <param name="noiseGenerator">The noise generator.</param>
        /// <param name="waterThreshold">
        /// The threshold under which the noise is considered as water.
        /// Note that this is not equivalent to proportion as the noise is not uniformly distributed.
        /// </param>
        public WaterThresholdTerrainGenerator(INoiseGenerator noiseGenerator, float waterThreshold)
        {
            this.noiseGenerator = noiseGenerator;
            this.waterThreshold = waterThreshold;
        }

        public IMap<int> GenerateTerrainMap(IWorldRules worldConfig, Vector size)
        {
            Contract.Requires(worldConfig != null);

            int land = worldConfig.Terrains[Terrains.Land];
            int water = worldConfig.Terrains[Terrains.Water];

            float[,] noiseMap = new float[size.X, size.Y];
            this.noiseGenerator.Generate(noiseMap);
            int[,] tiles = new int[size.X, size.Y];
            for (int x = 0; x < size.X; ++x)
            {
                for (int y = 0; y < size.Y; ++y)
                {
                    tiles[x, y] = noiseMap[x, y] > this.waterThreshold ? land : water;
                }
            }

            return new ArrayMap<int>(tiles);
        }
    }
}
