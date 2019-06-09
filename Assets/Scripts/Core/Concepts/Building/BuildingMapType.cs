namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Utilities to define and fetch the Building map.
    /// </summary>
    public static partial class MapTypes
    {
        public static readonly string BUILDING = "building";

        public static IMapView<Building> GetBuildingMapView(this IWorldView worldView)
        {
            Contract.Requires(worldView != null);
            return worldView.GetMapView<Building>(MapTypes.BUILDING);
        }

        public static IMap<Building> GetBuildingMap(this World world)
        {
            Contract.Requires(world != null);
            return world.GetMap<Building>(MapTypes.BUILDING);
        }

        public static void RegisterBuilding(this IMap<Building> map, Maps maps)
        {
            Contract.Requires(maps != null);
            maps.RegisterMap(MapTypes.BUILDING, map);
        }
    }
}
