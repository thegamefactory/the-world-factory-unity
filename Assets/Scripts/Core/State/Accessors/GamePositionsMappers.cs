using System.Collections.Generic;
using System.Linq;
using TWF.State.Building;
using TWF.State.Tile;

namespace TWF.State.Accessors
{
    /// <summary>
    /// An utility class that extends GamePositions to add a bunch of functions that map positions to useful constructs.
    /// </summary>
    public static class GamePositionsMappers
    {
        public static IEnumerable<(Vector, ITileView)> ToTilePositionTuples(this GamePositions gamePositions)
        {
            return gamePositions.Positions
               .Select(gamePositions.GetTilePositionTuple);
        }

        public static IEnumerable<ITileView> ToTiles(this GamePositions gamePositions)
        {
            return gamePositions.Positions
               .Select(gamePositions.GetTile);
        }

        public static IEnumerable<IBuilding> ToBuildings(this GamePositions gamePositions)
        {
            return gamePositions.ToTiles()
               .Select(t => t.Entity as IBuilding)
               .Where(b => null != b);
        }
    }
}
