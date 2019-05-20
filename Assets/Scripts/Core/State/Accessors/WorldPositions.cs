using System.Collections.Generic;
using TWF.State.Tile;

namespace TWF.State.Accessors
{
    /// <summary>
    /// A tuple world view / positions.
    /// Having the world view embedded alongside positions enables to map the positions to constructs that are specific to the state.
    /// </summary>
    public class WorldPositions
    {
        public WorldPositions(IWorldView worldView, IEnumerable<Vector> positions)
        {
            WorldView = worldView;
            Positions = positions;
        }

        public IWorldView WorldView { get; }
        public IEnumerable<Vector> Positions { get; }

        public ITileView GetTile(Vector position)
        {
            return WorldView.GetTile(position);
        }

        public (Vector, ITileView) GetTilePositionTuple(Vector position)
        {
            return (position, WorldView.GetTile(position));
        }
    }
}
