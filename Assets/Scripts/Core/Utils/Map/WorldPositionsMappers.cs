namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// An utility class that extends GamePositions to add a bunch of functions that map positions to useful constructs.
    /// </summary>
    public static class WorldPositionsMappers
    {
        public static IEnumerable<(Vector, T)> ToPositionTuples<T>(this WorldPositions<T> worldPositions)
        {
            Contract.Requires(worldPositions != null);

            return worldPositions.Positions
               .Select(worldPositions.GetMapContentPositionTuple);
        }

        public static IEnumerable<T> ToContent<T>(this WorldPositions<T> worldPositions)
        {
            Contract.Requires(worldPositions != null);

            return worldPositions.Positions
               .Select(worldPositions.GetMapContent);
        }
    }
}
