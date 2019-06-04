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

    // hack - This method is just to get something working.
    public Color GetTileColor(IMapView<Building> buildingMap, IMapView<int> terrainMap, IMapView<int> zoneMap, int x, int y)
    {
        /*Building building = buildingMap[x, y];
        Zone zone = zoneMap[x, y];

        if (null != building)
        {
            switch (zone.Name)
            {
                case "Residential":
                    return building.Variant % 2 == 0 ? building1Tile : building2Tile;
                case "Farmland":
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

        switch (zone.Name)
        {
            case "Empty":
                return emptyTile;
            case "Residential":
                return residentialTile;
            case "Farmland":
                return farmlandTile;
            case "Road":
                return roadTile;
            default:
                return errorTile;
        }*/
        return Color.white;
    }
}
