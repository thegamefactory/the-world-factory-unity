using System.Collections.Generic;
using TWF.Map.Entity;
using TWF.Map.Tile;

namespace TWF.Map.Building
{
    /// <summary>
    /// A building represents a constructed instance on the map.
    /// </summary>
    public interface IBuilding : IEntity
    {
        /// <summary>
        /// The zone on which the building is physically situated.
        /// </summary>
        TileZone Zone { get; }

        /// <summary>
        /// A number which is used to procedurally generate the rendering aspect of the building.
        /// </summary>
        int RenderingSeed { get; }

        /// <summary>
        /// The capacity of the building on the given usage <paramref name="usageType"/>.
        /// For ex: capacity in population = 10.
        /// Will return 0 if the building doesn't define the usage type.
        /// </summary>
        int GetCapacity(UsageType usageType);

        /// <summary>
        /// The absolute occupation of the building on the given usage <paramref name="usageType"/>.
        /// For ex: occupation in population = 8.
        /// Will return 0 if the building doesn't define the usage type.
        /// </summary>
        int GetOccupation(UsageType usageType);

        /// <summary>
        /// The usage types defined for this building.
        /// </summary>
        ICollection<UsageType> GetUsageTypes();
    }
}
