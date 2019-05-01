public class Map
{
    Tile[,] tiles;

    public Map(int width, int height)
    {
        tiles = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile();
            }
        }
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[x, y];
    }
}
