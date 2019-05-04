using System.Collections.Generic;

namespace TWF.Map
{
    public interface IImmutableTileMap
    {
        /// <summary>
        /// Return the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        Vector GetSize();

        /// <summary>
        /// Get an enumerable to iterate over all the tiles.
        /// </summary>
        /// <return>An enumerable to iterate over all the tiles.</return>
        IEnumerable<(Vector, Tile)> GetTiles();

        /// <summary>
        /// Get the tile at the given position using an absolute scale.
        /// </summary>
        /// <return>The tile at the given position.</return>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        Tile GetTile(int x, int y);

        /// <summary>
        /// Get the tile at the given position using an absolute scale.
        /// </summary>
        /// <return>The tile at the given position.</return>
        /// <param name="position">The position.</param>
        Tile GetTile(Vector position);

        /// <summary>
        /// Gets the tile at a continous position using a normalized scale.
        /// </summary>
        /// <returns>The tile at provided position.</returns>
        /// <param name="x">The x coordinate between 0 and 1.</param>
        /// <param name="y">The y coordinate between 0 and 1.</param>
        Tile GetTile(float x, float y);

        /// <summary>
        /// Converts the position given on a normalized scale onto an absolute position.
        /// </summary>
        /// <return>The absolute position.</return>
        /// <param name="x">The x coordinate between 0 and 1.</param>
        /// <param name="y">The y coordinate between 0 and 1.</param>
        Vector GetPosition(float x, float y);
    }
}
