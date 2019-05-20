using System.Collections.Generic;
using System.Linq;
using TWF.Map.Building;
using TWF.Map.Tile;

namespace TWF.Map.Accessors
{
    /// <summary>
    /// An utility class that extends GamePositions to add a bunch of functions that map positions to useful constructs.
    /// </summary>
    public static class WorldPositionsMappers
    {
        public static IEnumerable<(Vector, ITileView)> ToTilePositionTuples(this WorldPositions gamePositions)
        {
            return gamePositions.Positions
               .Select(gamePositions.GetTilePositionTuple);
        }

        public static IEnumerable<ITileView> ToTiles(this WorldPositions gamePositions)
        {
            return gamePositions.Positions
               .Select(gamePositions.GetTile);
        }

        public static IEnumerable<IBuilding> ToBuildings(this WorldPositions gamePositions)
        {
            return gamePositions.ToTiles()
               .Select(t => t.Building)
               .Where(b => null != b);
        }
    }
}
