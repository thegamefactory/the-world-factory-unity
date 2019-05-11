using TWF.State.Tile;
using TWF.State;

namespace TWF.Statistics
{
    public class DirectMapStatistics : IMapStatistics
    {
        IGameStateView gameStateView;

        public DirectMapStatistics(IGameStateView gameStateView)
        {
            this.gameStateView = gameStateView;
        }

        public int GetBuildingCount(TileZone zone)
        {
            return 0;
        }

        public int GetCount(TileZone zone)
        {
            throw new System.NotImplementedException();
        }

        public int GetUnoccupiedCount(TileZone zone)
        {
            throw new System.NotImplementedException();
        }
    }
}
