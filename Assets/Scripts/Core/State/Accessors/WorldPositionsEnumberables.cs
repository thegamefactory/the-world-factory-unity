using System.Collections.Generic;
using TWF.State.Map;
using System;

namespace TWF.State.Accessors
{
    /// <summary>
    /// An utility class to iterate over game state map positions.
    /// Useful wrapper over MapPositionsEnumberables.
    /// </summary>
    public static class WorldPositionsEnumberables
    {
        /// <summary>
        /// Get a GameStatePositions encapsulating the game state and an enumerable to iterate over all the tiles positions.
        /// </summary>
        /// <param name="gameStateView">The map to traverse.</param>
        /// <return>Get a GameStatePositions encapsulating the game state and an enumerable to iterate over all the tiles positions.</return>
        public static WorldPositions ToAllPositions(this IWorldView gameStateView)
        {
            return gameStateView.ToGamePositions(gameStateView.GetAllPositions());
        }

        /// <summary>
        /// Get an enumerable to iterate over all the tiles positions.
        /// </summary>
        /// <param name="gameStateView">The map to traverse.</param>
        /// <return>An enumerable to iterate over all the positions.</return>
        public static IEnumerable<Vector> GetAllPositions(this IWorldView gameStateView)
        {
            return MapPositionsEnumerables.GetPositions(0, 0, gameStateView.SizeX - 1, gameStateView.SizeY - 1);
        }

        /// <summary>
        /// Get a GameStatePositions encapsulating the game state and an enumerable to iterate over the tiles positions in the rectangle defined by the given coordinates.
        /// Corners are included.
        /// Coordinates outside of the map boundaries will be cropped.
        /// An invalid rectangle results in an empty result.
        /// </summary>
        /// <param name="x1">X position of the first corner.</param>
        /// <param name="y1">Y position of the first corner.</param>
        /// <param name="x2">X position of the second corner.</param>
        /// <param name="y2">Y position of the second corner.</param>
        /// <return>An enumerable to iterate over the tile positions.</return>
        public static WorldPositions ToPositions(this IWorldView gameStateView, int x1, int y1, int x2, int y2)
        {
            return gameStateView.ToGamePositions(gameStateView.GetPositions(x1, y1, x2, y2));
        }

        /// <summary>
        /// Get an enumerable to iterate over the tiles positions in the rectangle defined by the given coordinates.
        /// Corners are included.
        /// Coordinates outside of the map boundaries will be cropped.
        /// An invalid rectangle results in an empty result.
        /// </summary>
        /// <param name="x1">X position of the first corner.</param>
        /// <param name="y1">Y position of the first corner.</param>
        /// <param name="x2">X position of the second corner.</param>
        /// <param name="y2">Y position of the second corner.</param>
        /// <return>An enumerable to iterate over the tile positions.</return>
        public static IEnumerable<Vector> GetPositions(this IWorldView gameStateView, int x1, int y1, int x2, int y2)
        {
            int xMin = Math.Max(Math.Min(x1, x2), 0);
            int xMax = Math.Min(Math.Max(x1, x2), gameStateView.SizeX - 1);
            int yMin = Math.Max(Math.Min(y1, y2), 0);
            int yMax = Math.Min(Math.Max(y1, y2), gameStateView.SizeY - 1);
            return MapPositionsEnumerables.GetPositions(xMin, yMin, xMax, yMax);
        }

        private static WorldPositions ToGamePositions(this IWorldView gameStateView, IEnumerable<Vector> positions)
        {
            return new WorldPositions(gameStateView, positions);
        }
    }
}
