using System.Collections.Generic;
using TWF.State.Tile;
using TWF.State.Map;

namespace TWF.State
{
    public interface IGameStateView
    {
        /// <summary>
        /// <summary>
        /// Return the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        Vector Size { get; }

        /// <summary>
        /// Returns the size of the map on the X axis.
        /// </summary>
        /// <return>The size of the map on the X axis.</return>
        int SizeX { get; }

        /// <summary>
        /// Returns the size of the map on the Y axis.
        /// </summary>
        /// <return>The size of the map on the Y axis.</return>
        int SizeY { get; }

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
