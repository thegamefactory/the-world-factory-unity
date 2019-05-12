using System.Collections.Generic;
using TWF.State.Tile;

namespace TWF.State.Accessors
{
    /// <summary>
    /// A tuple game state / positions.
    /// Having the game state embedded alongside positions enables to map the positions to constructs that are specific to the state.
    /// </summary>
    public class GamePositions
    {
        public GamePositions(IGameStateView gameStateView, IEnumerable<Vector> positions)
        {
            GameStateView = gameStateView;
            Positions = positions;
        }

        public IGameStateView GameStateView { get; }
        public IEnumerable<Vector> Positions { get; }

        public ITileView GetTile(Vector position)
        {
            return GameStateView.GetTile(position);
        }

        public (Vector, ITileView) GetTilePositionTuple(Vector position)
        {
            return (position, GameStateView.GetTile(position));
        }
    }
}
