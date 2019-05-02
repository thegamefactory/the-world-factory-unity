using UnityEngine;
using TWF.Map;

public class TileMapper : MonoBehaviour
{
    public Color emptyTile;
    public Color residentialTile;

    public Color GetTileColor(Tile tile)
    {
        switch(tile.Type)
        {
            case Tile.TileType.EMPTY:
                return emptyTile;
            case Tile.TileType.RESIDENTIAL:
                return residentialTile;
            default:
                return Color.magenta;
        }
    }
}
