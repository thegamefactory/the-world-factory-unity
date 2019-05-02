using System.Collections.Generic;

namespace TWF.Map
{
    public class TileMap
    {
        Tile[,] tiles;

        public TileMap(int width, int height)
        {
            tiles = new Tile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new Tile();
                }
            }
        }

        public IEnumerable<(Position, Tile)> GetTiles()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    yield return (new Position(x, y), tiles[x, y]);
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public Tile GetTile(Position position)
        {
            return tiles[position.X, position.Y];
        }

        /// <summary>
        /// Gets the tile at a continous position using a normalized scale.
        /// </summary>
        /// <returns>The tile at provided position.</returns>
        /// <param name="x">The x coordinate between 0 and 1.</param>
        /// <param name="y">The y coordinate between 0 and 1.</param>
        public Tile GetTile(float x, float y)
        {
            return tiles[(int)(tiles.GetLength(0) * x), (int)(tiles.GetLength(1) * y)];
        }
    }
}
