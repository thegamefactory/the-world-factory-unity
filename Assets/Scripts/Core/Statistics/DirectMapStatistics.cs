using TWF.State;
using TWF.State.Map;
using System;
using System.Linq;
using TWF.State.Building;
using TWF.State.Accessors;

namespace TWF.Statistics
{
    public class DirectMapStatistics : IMapStatistics
    {
        IGameStateView gameStateView;

        public DirectMapStatistics(IGameStateView gameStateView)
        {
            this.gameStateView = gameStateView;
        }

        public int GetCapacity(UsageType usageType)
        {
            return MapBuidlings(b => b.GetCapacity(usageType));
        }

        public int GetOccupation(UsageType usageType)
        {
            return MapBuidlings(b => b.GetOccupation(usageType));
        }

        public int MapBuidlings(Func<IBuilding, int> mapper)
        {
            return gameStateView.ToAllPositions()
                .ToBuildings()
                .Select(b => b.GetOccupation(UsageType.HOUSING))
                .Sum();
        }
    }
}
