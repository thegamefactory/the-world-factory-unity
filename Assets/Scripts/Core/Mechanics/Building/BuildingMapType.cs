namespace TWF
{
    /// <summary>
    /// Utilities to define and fetch the Building map.
    /// </summary>
    public static partial class MapTypes
    {
        public static MapType Building { get; } = new MapType("Building");

        public static IMapView<Building> GetBuildingMapView(this IWorldView worldView)
        {
            return worldView.GetMapView<Building>(MapTypes.Building);
        }

        public static IMap<Building> GetBuildingMap(this World world)
        {
            return world.GetMap<Building>(MapTypes.Building);
        }

        public static void Register(this IMap<Building> map, Maps maps)
        {
            maps.RegisterMap(MapTypes.Building, map);
        }
    }
}
