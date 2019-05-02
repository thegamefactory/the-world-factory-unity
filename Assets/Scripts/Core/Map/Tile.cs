namespace TWF.Map
{
    public class Tile
    {
        public enum TileType
        {
            EMPTY,
            RESIDENTIAL
        }

        public TileType Type { get; internal set; } = TileType.EMPTY;
    }
}