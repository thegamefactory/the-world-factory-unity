﻿using System.Collections.Generic;
using System;

namespace TWF
{
    /// <summary>
    /// An utility class to iterate over map positions.
    /// Useful wrapper over MapPositionsEnumberables.
    /// </summary>
    public static class WorldPositionsEnumberables
    {
        /// <summary>
        /// Get a GameStatePositions encapsulating the map and an enumerable to iterate over all the positions.
        /// </summary>
        /// <param name="map">The map to traverse.</param>
        /// <return>Get a GameStatePositions encapsulating the game state and an enumerable to iterate over all the tiles positions.</return>
        public static WorldPositions<T> ToAllPositions<T>(this IMapView<T> map)
        {
            return map.ToMapPositions(map.GetAllPositions());
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
        public static WorldPositions<T> ToPositions<T>(this IMapView<T> map, int x1, int y1, int x2, int y2)
        {
            return map.ToMapPositions(map.GetPositions(x1, y1, x2, y2));
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

        private static WorldPositions<T> ToMapPositions<T>(this IMapView<T> map, IEnumerable<Vector> positions)
        {
            return new WorldPositions<T>(map, positions);
        }
    }
}
