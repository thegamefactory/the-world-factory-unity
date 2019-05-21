namespace TWF
{
    /// <summary>
    /// Utilities to define and fetch the Zone map.
    /// </summary>
    public static partial class MapTypes
    {
        public static MapType Zone { get; } = new MapType("Zone");

        public static IMapView<Zone> GetZoneMapView(this IWorldView worldView)
        {
            return worldView.GetMapView<Zone>(MapTypes.Zone);
        }

        public static IMap<Zone> GetZoneMap(this World world)
        {
            return world.GetMap<Zone>(MapTypes.Zone);
        }

        public static void Register(this IMap<Zone> map, Maps maps)
        {
            maps.RegisterMap(MapTypes.Zone, map);
        }
    }
}
