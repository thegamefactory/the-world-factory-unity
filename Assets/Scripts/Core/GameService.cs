namespace TWF
{
    public class GameService
    {
        Map map;

        public GameService()
        {
            map = new Map(1, 1);
        }

        public void InitMap(int width, int height)
        {
            map = new Map(width, height);
        }

        public Tile GetTile(int x, int y)
        {
            return map.GetTile(x, y);
        }

        public void SetTileType(int x, int y, Tile.TileType type)
        {
            map.GetTile(x, y).Type = type;
        }
    }
}
