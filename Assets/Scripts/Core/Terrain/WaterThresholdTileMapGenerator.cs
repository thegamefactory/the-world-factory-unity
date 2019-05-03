using System;
using TWF.Map;

namespace TWF.Terrain
{
    /// <summary>
    /// A tile generator which considers all the noise below a certain threshold as water and the rest land.
    /// </summary>
    public class WaterThresholdTileMapGenerator : ITileMapGenerator
    {
        private INoiseGenerator noiseGenerator;
        private float waterThreshold;

        /// <param name="noiseGenerator">The noise generator.</param>
        /// <param name="waterThreshold">
        /// The threshold under which the noise is considered as water.
        /// Note that this is not equivalent to proportion as the noise is not uniformly distributed.
        /// </param>
        public WaterThresholdTileMapGenerator(INoiseGenerator noiseGenerator, float waterThreshold)
        {
            this.noiseGenerator = noiseGenerator;
            this.waterThreshold = waterThreshold;
        }

        public TileMap Generate(Vector size)
        {
            float[,] noiseMap = new float[size.X, size.Y];
            noiseGenerator.Generate(noiseMap);
            Tile[,] tiles = new Tile[size.X, size.Y];
            for (int x = 0; x < size.X; ++x)
            {
                for (int y = 0; y < size.Y; ++y)
                {
                    tiles[x, y] = CreateTile(noiseMap[x, y]);
                }
            }
            return new TileMap(tiles);
        }

        Tile CreateTile(float noiseValue)
        {
            if (noiseValue < waterThreshold)
            {
                return new Tile(Tile.TileZone.EMPTY, Tile.TileTerrain.WATER);
            }
            else
            {
                return new Tile(Tile.TileZone.EMPTY, Tile.TileTerrain.LAND);
            }
        }
    }
}
