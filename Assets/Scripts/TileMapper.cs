using UnityEngine;
using TWF;
using TWF.Map;

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

    public Color GetTileColor(IEntity entity, Tile tile)
    {
        if (entity is Building)
        {

            switch (tile.Zone)
            {
                case Tile.TileZone.RESIDENTIAL:
                    Building building = entity as Building;
                    return building.Seed % 2 == 0 ? building1Tile : building2Tile;
                case Tile.TileZone.FARMLAND:
                    return fieldTile;
                default:
                    return errorTile;
            }
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
            case Tile.TileZone.FARMLAND:
                return farmlandTile;
            case Tile.TileZone.ROAD:
                return roadTile;
            default:
                return errorTile;
        }
    }
}
