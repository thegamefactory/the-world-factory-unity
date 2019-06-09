namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// An utility class to iterate over maps positions.
    /// </summary>
    public static class MapPositionsEnumerables
    {
        /// <summary>
        /// Get an enumerable to iterate over all the tiles positions.
        /// </summary>
        /// <param name="map">The map to traverse.</param>
        /// <return>An enumerable to iterate over all the positions.</return>
        public static IEnumerable<Vector> GetAllPositions(this IGenericMap map)
        {
            Contract.Requires(map != null);
            return UnsafeGetPositions(0, 0, map.SizeX - 1, map.SizeY - 1);
        }

        /// <summary>
        /// Get an enumerable to iterate over the tiles positions in the rectangle defined by the given coordinates.
        /// Corners are included.
        /// Coordinates outside of the map boundaries will be cropped.
        /// An invalid rectangle results in an empty result.
        /// </summary>
        /// <param name="map">The map from which the coordinates are sourced.</param>
        /// <param name="x1">X position of the first corner.</param>
        /// <param name="y1">Y position of the first corner.</param>
        /// <param name="x2">X position of the second corner.</param>
        /// <param name="y2">Y position of the second corner.</param>
        /// <return>An enumerable to iterate over the tile positions.</return>
        public static IEnumerable<Vector> GetPositions(this IGenericMap map, int x1, int y1, int x2, int y2)
        {
            Contract.Requires(map != null);

            int xMin = Math.Max(Math.Min(x1, x2), 0);
            int xMax = Math.Min(Math.Max(x1, x2), map.SizeX - 1);
            int yMin = Math.Max(Math.Min(y1, y2), 0);
            int yMax = Math.Min(Math.Max(y1, y2), map.SizeY - 1);
            return UnsafeGetPositions(xMin, yMin, xMax, yMax);
        }

        /// <summary>
        /// Get an enumerable to iterate over the tiles positions in the rectangle defined by the given coordinates.
        /// Corners are included.
        /// Coordinates outside of the map boundaries will be cropped.
        /// An invalid rectangle results in an empty result.
        /// </summary>
        /// <param name="firstCorner">Position of the first corner.</param>
        /// <param name="secondCorner">Position of the second corner.</param>
        /// <return>An enumerable to iterate over the tile positions.</return>
        public static IEnumerable<Vector> GetPositions(Vector firstCorner, Vector secondCorner)
        {
            return GetPositions(firstCorner.X, firstCorner.Y, secondCorner.X, secondCorner.Y);
        }

        /// <summary>
        /// Get an enumerable to iterate over the tiles positions in the rectangle defined by the given coordinates.
        /// Since no map is supplied, the coordinates are not validated.
        /// </summary>
        /// <param name="x1">X position of the first corner.</param>
        /// <param name="y1">Y position of the first corner.</param>
        /// <param name="x2">X position of the second corner.</param>
        /// <param name="y2">Y position of the second corner.</param>
        /// <return>An enumerable to iterate over the tile positions.</return>
        public static IEnumerable<Vector> GetPositions(int x1, int y1, int x2, int y2)
        {
            int xMin = Math.Min(x1, x2);
            int xMax = Math.Max(x1, x2);
            int yMin = Math.Min(y1, y2);
            int yMax = Math.Max(y1, y2);
            return UnsafeGetPositions(xMin, yMin, xMax, yMax);
        }

        private static IEnumerable<Vector> UnsafeGetPositions(int x1, int y1, int x2, int y2)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    yield return new Vector(x, y);
                }
            }
        }
    }
}
