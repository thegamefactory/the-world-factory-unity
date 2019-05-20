using TWF.Map.Tile;
using TWF.Map.Building;

namespace TWF.Statistics
{
    public interface IMapStatistics
    {
        int GetCapacity(UsageType usageType);
        int GetOccupation(UsageType usageType);
    }
}
