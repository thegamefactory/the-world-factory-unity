using UnityEngine;
using TWF;

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

    public Color GetTileColor(IMapView<Building> buildingMap, IMapView<TWF.Terrain> terrainMap, IMapView<Zone> zoneMap, int x, int y)
    {
        Building building = buildingMap[x, y];
        Zone zone = zoneMap[x, y];

        if (null != building)
        {
            switch (zone)
            {
                case Zone.RESIDENTIAL:
                    return building.Variant % 2 == 0 ? building1Tile : building2Tile;
                case Zone.FARMLAND:
                    return fieldTile;
                default:
                    return errorTile;
            }
        }

        TWF.Terrain terrain = terrainMap[x, y];
        if (TWF.Terrain.WATER == terrain)
        {
            return waterTile;
        }

        switch (zone)
        {
            case Zone.EMPTY:
                return emptyTile;
            case Zone.RESIDENTIAL:
                return residentialTile;
            case Zone.FARMLAND:
                return farmlandTile;
            case Zone.ROAD:
                return roadTile;
            default:
                return errorTile;
        }
    }
}
