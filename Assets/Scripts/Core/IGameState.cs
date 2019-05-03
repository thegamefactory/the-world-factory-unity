using TWF.Map;
using System.Collections.Generic;

namespace TWF
{
    public interface IGameState
    {
        Vector GetTileMapSize();
        IEntity GetEntity(int x, int y);
        Tile GetTile(int x, int y);
        Tile GetTile(float x, float y);
        IEnumerable<(Vector, Tile)> GetTiles();
    }
}
