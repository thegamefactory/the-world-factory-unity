namespace TWF
{
    /// <summary>
    /// Utilities to define and fetch the Zone map.
    /// </summary>
    public static partial class MapTypes
    {
        public static string ZONE = "zone";

        public static IMapView<int> GetZoneMapView(this IWorldView worldView)
        {
            return worldView.GetMapView<int>(MapTypes.ZONE);
        }

        public static IMap<int> GetZoneMap(this World world)
        {
            return world.GetMap<int>(MapTypes.ZONE);
        }

        public static void RegisterZone(this IMap<int> map, Maps maps)
        {
            maps.RegisterMap(MapTypes.ZONE, map);
        }
    }
}
