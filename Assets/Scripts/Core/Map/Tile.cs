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
            RESIDENTIAL
        }

        public TileZone Zone { get; private set; }

        public Tile(TileZone zone)
        {
            this.Zone = zone;
        }
    }
}