namespace TWF.Map
{
    /// <summary>
    /// The smallest discrete position on the map.
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// The zone of the tile, can be defined by the player and used by agents to evaluate against rule and define which buildings to create.
        /// </summary>
        public enum TileZone
        {
            EMPTY,
            RESIDENTIAL,
            ROAD
        }

        /// <summary>
        /// The terrain of the tile.
        /// </summary>
        public enum TileTerrain
        {
            WATER,
            LAND
        }

        public TileZone Zone { get; private set; }
        public TileTerrain Terrain { get; private set; }

        public Tile(TileZone zone, TileTerrain terrain)
        {
            Zone = zone;
            Terrain = terrain;
        }
    }
}