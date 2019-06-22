namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Utilities to define and fetch the Terrain map.
    /// </summary>
    public static partial class MapTypes
    {
        public static readonly string Terrain = "terrain";

        public static IMapView<int> GetTerrainMapView(this IWorldView worldView)
        {
            Contract.Requires(worldView != null);
            return worldView.GetMapView<int>(MapTypes.Terrain);
        }

        public static IMap<int> GetTerrainMap(this World world)
        {
            Contract.Requires(world != null);
            return world.GetMap<int>(MapTypes.Terrain);
        }

        public static void RegisterTerrain(this IMap<int> map, Maps maps)
        {
            Contract.Requires(maps != null);
            maps.RegisterMap(MapTypes.Terrain, map);
        }
    }
}
