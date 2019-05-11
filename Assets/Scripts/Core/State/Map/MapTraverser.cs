using System.Collections.Generic;
using System;

namespace TWF.State.Map
{
    /// <summary>
    /// An utility class to traverse Maps.
    /// </summary>
    public static class MapTraverser
    {
        /// <summary>
        /// Get an enumerable to iterate over all the map elements.
        /// </summary>
        /// <return>An enumerable to iterate over all the map elements.</return>
        public static IEnumerable<(Vector, T)> GetElements<T>(IMap<T> map)
        {
            return UnsafeGetElementsPositions(map, 0, 0, map.GetSizeX() - 1, map.GetSizeY() - 1);
        }

        /// <summary>
        /// Get an enumerable to iterate over the tiles in the rectangle defined by the given coordinates.
        /// Corners are included.
        /// Coordinates outside of the map boundaries will be cropped.
        /// An invalid rectangle results in an empty result.
        /// </summary>
        /// <param name="firstCorner">Position of the first corner.</param>
        /// <param name="secondCorner">Position of the second corner.</param>
        public static IEnumerable<(Vector, T)> GetElements<T>(IMap<T> map, Vector firstCorner, Vector secondCorner)
        {
            return GetElements(map, firstCorner.X, firstCorner.Y, secondCorner.X, secondCorner.Y);
        }

        /// <summary>
        /// Get an enumerable to iterate over the tiles in the rectangle defined by the given coordinates.
        /// Corners are included.
        /// Coordinates outside of the map boundaries will be cropped.
        /// An invalid rectangle results in an empty result.
        /// </summary>
        /// <param name="x1">X position of the first corner.</param>
        /// <param name="y1">Y position of the first corner.</param>
        /// <param name="x2">X position of the second corner.</param>
        /// <param name="y2">Y position of the second corner.</param>
        /// <return>An enumerable to iterate over the tiles.</return>
        public static IEnumerable<(Vector, T)> GetElements<T>(IMap<T> map, int x1, int y1, int x2, int y2)
        {
            int xMin = Math.Max(Math.Min(x1, x2), 0);
            int xMax = Math.Min(Math.Max(x1, x2), map.GetSizeX() - 1);
            int yMin = Math.Max(Math.Min(y1, y2), 0);
            int yMax = Math.Min(Math.Max(y1, y2), map.GetSizeY() - 1);
            return UnsafeGetElementsPositions(map, xMin, yMin, xMax, yMax);
        }

        private static IEnumerable<(Vector, T)> UnsafeGetElementsPositions<T>(IMap<T> map, int x1, int y1, int x2, int y2)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    yield return (new Vector(x, y), map.GetElement(x, y));
                }
            }
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
        /// <return>An enumerable to iterate over the tiles.</return>
        /// TODO: refactor GetElements/GetPositions to avoid duplication
        public static IEnumerable<Vector> GetPositions<T>(IMap<T> map, int x1, int y1, int x2, int y2)
        {
            int xMin = Math.Max(Math.Min(x1, x2), 0);
            int xMax = Math.Min(Math.Max(x1, x2), map.GetSizeX() - 1);
            int yMin = Math.Max(Math.Min(y1, y2), 0);
            int yMax = Math.Min(Math.Max(y1, y2), map.GetSizeY() - 1);
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
        /// TODO: refactor GetElements/GetPositions to avoid duplication
        public static IEnumerable<Vector> GetPositions(Vector firstCorner, Vector secondCorner)
        {
            return GetPositions(firstCorner.X, firstCorner.Y, secondCorner.X, secondCorner.Y);
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
        /// <return>An enumerable to iterate over the tiles.</return>
        /// TODO: refactor GetElements/GetPositions to avoid duplication
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
