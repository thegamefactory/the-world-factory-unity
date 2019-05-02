using TWF.Map;

namespace TWF
{
    public class GameService
    {
        TileMap map;
        EntityMap entityMap;

        public GameService()
        {
            map = new TileMap(1, 1);
            entityMap = new EntityMap(1, 1);
        }

        public void InitMap(int width, int height)
        {
            map = new TileMap(width, height);
            entityMap = new EntityMap(width, height);
        }

        public IEntity GetEntity(int x, int y)
        {
            return entityMap.GetEntity(x, y);
        }

        public Tile GetTile(int x, int y)
        {
            return map.GetTile(x, y);
        }

        public Tile GetTile(float x, float y)
        {
            return map.GetTile(x, y);
        }

        public void SetTileType(int x, int y, Tile.TileType type)
        {
            map.GetTile(x, y).Type = type;
        }

        public void SetTileType(float x, float y, Tile.TileType type)
        {
            map.GetTile(x, y).Type = type;
        }
    }
}
