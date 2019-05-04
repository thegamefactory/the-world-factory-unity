using UnityEngine;
using TWF;
using TWF.Map;

public class TileMapper : MonoBehaviour
{
    public Color emptyTile;
    public Color residentialTile;
    public Color building1Tile;
    public Color building2Tile;
    public Color waterTile;

    public Color GetTileColor(IEntity entity, Tile tile)
    {
        if (entity is Building)
        {
            Building building = entity as Building;
            return building.Seed % 2 == 0 ? building1Tile : building2Tile;
        }

        if (Tile.TileTerrain.WATER == tile.Terrain)
        {
            return waterTile;
        }

        switch (tile.Zone)
        {
            case Tile.TileZone.EMPTY:
                return emptyTile;
            case Tile.TileZone.RESIDENTIAL:
                return residentialTile;
            default:
                return Color.magenta;
        }
    }
}
