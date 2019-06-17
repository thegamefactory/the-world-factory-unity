namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Utilities to define and fetch the Building map.
    /// </summary>
    public static partial class MapTypes
    {
        public static readonly string Building = "building";
        public static readonly int NoBuilding = -1;

        public static IMapView<int> GetBuildingMapView(this IWorldView worldView)
        {
            Contract.Requires(worldView != null);
            return worldView.GetMapView<int>(MapTypes.Building);
        }

        public static IMap<int> GetBuildingMap(this World world)
        {
            Contract.Requires(world != null);
            return world.GetMap<int>(MapTypes.Building);
        }

        public static void RegisterBuilding(this IMap<int> map, Maps maps)
        {
            Contract.Requires(maps != null);
            maps.RegisterMap(MapTypes.Building, map);
        }
    }
}
