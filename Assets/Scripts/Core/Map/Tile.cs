namespace TWF.Map
{
    public class Tile
    {
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