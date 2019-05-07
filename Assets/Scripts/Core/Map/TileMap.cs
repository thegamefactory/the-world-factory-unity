using System.Collections.Generic;

namespace TWF.Map
{
    /// <summary>
    /// A bidimensional array of tiles representing the game map.
    /// </summary>
    public class TileMap : IImmutableTileMap
    {
        Tile[,] tiles;

        /// <summary>
        /// A constructor that initializes the map with the given tiles.
        /// </summary>
        public TileMap(Tile[,] tiles)
        {
            this.tiles = tiles;
        }

        /// <summary>
        /// Return the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        public Vector GetSize()
        {
            return new Vector(tiles.GetLength(0), tiles.GetLength(1));
        }

        /// <summary>
        /// Get an enumerable to iterate over all the tiles.
        /// </summary>
        /// <return>An enumerable to iterate over all the tiles.</return>
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

        /// <summary>
        /// Get the tile at the given position using an absolute scale.
        /// </summary>
        /// <return>The tile at the given position.</return>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

        /// <summary>
        /// Get the tile at the given position using an absolute scale.
        /// </summary>
        /// <return>The tile at the given position.</return>
        /// <param name="position">The position.</param>
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

        /// <summary>
        /// Converts the position given on a normalized scale onto an absolute position.
        /// </summary>
        /// <return>The absolute position.</return>
        /// <param name="x">The x coordinate between 0 and 1.</param>
        /// <param name="y">The y coordinate between 0 and 1.</param>
        public Vector GetPosition(float x, float y)
        {
            return new Vector((int)(tiles.GetLength(0) * x), (int)(tiles.GetLength(1) * y));
        }

        /// <summary>
        /// Sets the zone of the tile at the given position to the given zone.
        /// </summary>
        /// <return>The absolute position.</return>
        /// <param name="zone">The new tile zone.</param>
        /// <param name="position">The position of the tile.</param>
        public void SetTileZone(TileZone zone, Vector position)
        {
            SetTileZone(zone, position.X, position.Y);
        }

        /// <summary>
        /// Sets the zone of the tile at the given position to the given zone.
        /// </summary>
        /// <return>The absolute position.</return>
        /// <param name="zone">The new tile zone.</param>
        /// <param name="x">The x coordinate of the tile.</param>
        /// <param name="y">The x coordinate of the tile.</param>
        public void SetTileZone(TileZone zone, int x, int y)
        {
            tiles[x, y] = new Tile(zone, tiles[x, y].Terrain);
        }
    }
}
