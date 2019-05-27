namespace TWF
{
    /// <summary>
    /// Utilities to define and fetch the Terrain map.
    /// </summary>
    public static partial class MapTypes
    {
        public static MapType Terrain { get; } = new MapType("Terrain");

        public static IMapView<Terrain> GetTerrainMapView(this IWorldView worldView)
        {
            return worldView.GetMapView<Terrain>(MapTypes.Terrain);
        }

        public static IMap<Terrain> GetTerrainMap(this World world)
        {
            return world.GetMap<Terrain>(MapTypes.Terrain);
        }

        public static void Register(this IMap<Terrain> map, Maps maps)
        {
            maps.RegisterMap(MapTypes.Terrain, map);
        }
    }
}
