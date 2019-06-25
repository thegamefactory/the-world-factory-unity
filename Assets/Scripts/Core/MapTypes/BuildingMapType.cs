namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The building map maps each position to a building id, or -1 if there's no building at this location.
    /// Note: it maps to a *building id*, not a *building model id*.
    /// The building entity will have a building model component to access the building model.
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
