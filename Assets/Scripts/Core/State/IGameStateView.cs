using System.Collections.Generic;
using TWF.State.Tile;

namespace TWF.State
{
    public interface IGameStateView
    {
        /// <summary>
        /// <summary>
        /// Return the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        Vector GetSize();

        /// <summary>
        /// Get an enumerable to iterate over all the tiles.
        /// </summary>
        /// <return>An enumerable to iterate over all the tiles.</return>
        IEnumerable<(Vector, ITileView)> GetTiles();

        /// <summary>
        /// Get the tile at the given position using an absolute scale.
        /// </summary>
        /// <return>The tile at the given position.</return>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        ITileView GetTile(int x, int y);

        /// <summary>
        /// Get the tile at the given position using an absolute scale.
        /// </summary>
        /// <return>The tile at the given position.</return>
        /// <param name="position">The position.</param>
        ITileView GetTile(Vector position);
    }
}
