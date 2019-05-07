using System.Collections.Generic;
using TWF.Map;

namespace TWF.Building
{
    /// <summary>
    /// A building represents a constructed instance on the map.
    /// </summary>
    public interface IBuilding : IEntity
    {
        TileZone Zone { get; }
        int Seed { get; }
        int GetCapacity(UsageType usageType);
        int GetOccupation(UsageType usageType);
        ICollection<UsageType> GetUsageTypes();
    }
}
