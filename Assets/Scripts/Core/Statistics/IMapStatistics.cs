using TWF.State.Tile;
using TWF.State.Building;

namespace TWF.Statistics
{
    public interface IMapStatistics
    {
        int GetCapacity(UsageType usageType);
        int GetOccupation(UsageType usageType);
    }
}
