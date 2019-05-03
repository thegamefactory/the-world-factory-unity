using UnityEngine;
using TWF;
using TWF.Map;

public class TileMapper : MonoBehaviour
{
    public Color emptyTile;
    public Color residentialTile;
    public Color buildingTile;

    public Color GetTileColor(IEntity entity, Tile tile)
    {
        if (entity is Building)
        {
            return buildingTile;
        }

        switch (tile.Type)
        {
            case Tile.Zone.EMPTY:
                return emptyTile;
            case Tile.Zone.RESIDENTIAL:
                return residentialTile;
            default:
                return Color.magenta;
        }
    }
}
