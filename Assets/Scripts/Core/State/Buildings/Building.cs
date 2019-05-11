using System.Collections.Generic;
using TWF.State.Tile;

namespace TWF.State.Building
{
    /// <summary>
    /// Concrete building implementation.
    /// </summary>
    public class Building : IBuilding
    {
        public TileZone Zone { get; }
        public int RenderingSeed { get; }
        private IDictionary<UsageType, Usage> usage;

        public Building(TileZone zone, int seed, IDictionary<UsageType, Usage> usage)
        {
            Zone = zone;
            RenderingSeed = seed;
            this.usage = usage;
        }

        public int GetCapacity(UsageType usageType)
        {
            if (usage.ContainsKey(usageType))
            {
                return usage[usageType].Capacity;
            }
            else
            {
                return 0;
            }
        }

        public int GetOccupation(UsageType usageType)
        {
            if (usage.ContainsKey(usageType))
            {
                return usage[usageType].Occupation;
            }
            else
            {
                return 0;
            }
        }

        public ICollection<UsageType> GetUsageTypes()
        {
            return usage.Keys;
        }
    }
}
