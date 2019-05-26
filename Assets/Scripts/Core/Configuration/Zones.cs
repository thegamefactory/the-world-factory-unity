using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// A definition of all zones.
    /// </summary>
    public static class Zones
    {
        public static Dictionary<string, Zone> AllZones { get; } = new Dictionary<string, Zone>();

        public static Zone Empty = RegisterZone("Empty", ManuallyZonable.Instance);
        public static Zone Residential = RegisterZone("Residential", ManuallyZonable.Instance, Developable.Instance);
        public static Zone Farmland = RegisterZone("Farmland", ManuallyZonable.Instance, Developable.Instance);
        public static Zone Road = RegisterZone("Road", ManuallyZonable.Instance);

        private static Zone RegisterZone(string name, params ITrait[] zoneTraits)
        {
            Zone z = new Zone(name, zoneTraits);
            AllZones.Add(z.Name, z);
            return z;
        }
    }
}
