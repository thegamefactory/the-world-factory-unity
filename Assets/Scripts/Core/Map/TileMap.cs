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
                    tiles[x, y] = new Tile(Tile.TileZone.EMPTY);
                }
            }
        }

        public Vector GetSize()
        {
            return new Vector(tiles.GetLength(0), tiles.GetLength(1));
        }

        public IEnumerable<(Vector, Tile)> GetTiles()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    yield return (new Vector(x, y), tiles[x, y]);
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        public Tile GetTile(Vector position)
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
            return GetTile(GetPosition(x, y));
        }

        public Vector GetPosition(float x, float y)
        {
            return new Vector((int)(tiles.GetLength(0) * x), (int)(tiles.GetLength(1) * y));
        }

        public void SetTileZone(Tile.TileZone zone, Vector position)
        {
            tiles[position.X, position.Y] = new Tile(zone);
        }
    }
}
