using UnityEngine;
using TWF.Map.Tile;
using TWF.Map.Building;

public class TileMapper : MonoBehaviour
{
    public Color emptyTile;
    public Color residentialTile;
    public Color farmlandTile;
    public Color fieldTile;
    public Color building1Tile;
    public Color building2Tile;
    public Color waterTile;
    public Color roadTile;

    public Color errorTile;

    public Color GetTileColor(ITileView tile)
    {
        IBuilding building = tile.Building;
        if (null != building)
        {

            switch (tile.Zone)
            {
                case TileZone.RESIDENTIAL:
                    return building.RenderingSeed % 2 == 0 ? building1Tile : building2Tile;
                case TileZone.FARMLAND:
                    return fieldTile;
                default:
                    return errorTile;
            }
        }

        if (TileTerrain.WATER == tile.Terrain)
        {
            return waterTile;
        }

        switch (tile.Zone)
        {
            case TileZone.EMPTY:
                return emptyTile;
            case TileZone.RESIDENTIAL:
                return residentialTile;
            case TileZone.FARMLAND:
                return farmlandTile;
            case TileZone.ROAD:
                return roadTile;
            default:
                return errorTile;
        }
    }
}
