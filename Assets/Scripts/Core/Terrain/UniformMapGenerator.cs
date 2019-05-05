using System;
using TWF.Map;

namespace TWF.Terrain
{
    /// <summary>
    /// A tile generator which creates an uniform map of the terrain type given at construction.
    /// </summary>
    public class UniformMapGenerator : ITileMapGenerator
    {
        private Tile.TileTerrain terrain;

        /// <param name="terrain">The type of terrain that this generator create.</param>
        public UniformMapGenerator(Tile.TileTerrain terrain)
        {
            this.terrain = terrain;
        }

        public TileMap Generate(Vector size)
        {
            Tile[,] tiles = new Tile[size.X, size.Y];
            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    tiles[x, y] = new Tile(Tile.TileZone.EMPTY, terrain);
                }
            }
            return new TileMap(tiles);
        }
    }
}
