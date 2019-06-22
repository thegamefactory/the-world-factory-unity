namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Utilities to define and fetch the Zone map.
    /// </summary>
    public static partial class MapTypes
    {
        public static readonly string Zone = "zone";

        public static IMapView<int> GetZoneMapView(this IWorldView worldView)
        {
            Contract.Requires(worldView != null);
            return worldView.GetMapView<int>(MapTypes.Zone);
        }

        public static IMap<int> GetZoneMap(this World world)
        {
            Contract.Requires(world != null);
            return world.GetMap<int>(MapTypes.Zone);
        }

        public static void RegisterZone(this IMap<int> map, Maps maps)
        {
            Contract.Requires(maps != null);
            maps.RegisterMap(MapTypes.Zone, map);
        }
    }
}
