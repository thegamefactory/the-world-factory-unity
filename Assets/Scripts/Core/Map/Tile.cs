namespace TWF.Map
{
    /// <summary>
    /// The smallest discrete position on the map.
    /// </summary>
    public class Tile
    {
        public TileZone Zone { get; private set; }
        public TileTerrain Terrain { get; private set; }

        public Tile(TileZone zone, TileTerrain terrain)
        {
            Zone = zone;
            Terrain = terrain;
        }
    }
}