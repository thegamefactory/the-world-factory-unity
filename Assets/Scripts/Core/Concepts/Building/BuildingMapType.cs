namespace TWF
{
    /// <summary>
    /// Utilities to define and fetch the Building map.
    /// </summary>
    public static partial class MapTypes
    {
        public static string BUILDING = "building";

        public static IMapView<Building> GetBuildingMapView(this IWorldView worldView)
        {
            return worldView.GetMapView<Building>(MapTypes.BUILDING);
        }

        public static IMap<Building> GetBuildingMap(this World world)
        {
            return world.GetMap<Building>(MapTypes.BUILDING);
        }

        public static void RegisterBuilding(this IMap<Building> map, Maps maps)
        {
            maps.RegisterMap(MapTypes.BUILDING, map);
        }
    }
}
