namespace TWF
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An utility class that extends GamePositions to add a bunch of functions that map positions to useful constructs.
    /// </summary>
    public static class WorldPositionsMappers
    {
        public static IEnumerable<(Vector, T)> ToPositionTuples<T>(this WorldPositions<T> gamePositions)
        {
            return gamePositions.Positions
               .Select(gamePositions.GetMapContentPositionTuple);
        }

        public static IEnumerable<T> ToContent<T>(this WorldPositions<T> gamePositions)
        {
            return gamePositions.Positions
               .Select(gamePositions.GetMapContent);
        }
    }
}
