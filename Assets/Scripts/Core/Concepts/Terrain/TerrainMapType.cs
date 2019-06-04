namespace TWF
{
    /// <summary>
    /// Utilities to define and fetch the Terrain map.
    /// </summary>
    public static partial class MapTypes
    {
        public static string TERRAIN = "terrain";

        public static IMapView<int> GetTerrainMapView(this IWorldView worldView)
        {
            return worldView.GetMapView<int>(MapTypes.TERRAIN);
        }

        public static IMap<int> GetTerrainMap(this World world)
        {
            return world.GetMap<int>(MapTypes.TERRAIN);
        }

        public static void RegisterTerrain(this IMap<int> map, Maps maps)
        {
            maps.RegisterMap(MapTypes.TERRAIN, map);
        }
    }
}
