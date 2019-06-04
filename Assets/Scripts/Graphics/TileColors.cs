using UnityEngine;

namespace TWF
{
    public class TileColors : MonoBehaviour
    {
        public Color EmptyTile;
        public Color ResidentialTile;
        public Color FarmlandTile;
        public Color FieldTile;
        public Color Building1Tile;
        public Color Building2Tile;
        public Color WaterTile;
        public Color RoadTile;

        public static string ZONE_COLOR = "tileColor";
        public static string BUILDING_COLOR = "buildingColor";

        public Color errorTile;

        public void Start()
        {
            Root.WorldConfigFactory.WorldConfigCustomizer += RegisterTileColors;
        }

        private void RegisterTileColors(WorldConfig worldConfig)
        {
            Registry zones = worldConfig.Zones;

            TypedComponent<Color> zoneColor = new TypedComponent<Color>(ZONE_COLOR, () => errorTile);
            zoneColor.AttachComponent(zones[Zones.EMPTY], EmptyTile);
            zoneColor.AttachComponent(zones[Zones.FARMLAND], FarmlandTile);
            zoneColor.AttachComponent(zones[Zones.RESIDENTIAL], ResidentialTile);
            zoneColor.AttachComponent(zones[Zones.ROAD], RoadTile);
            zones.Extend(zoneColor);

            TypedComponent<BuildingColor> tileColor = new TypedComponent<BuildingColor>(BUILDING_COLOR, () => (v) => errorTile);
            tileColor.AttachComponent(zones[Zones.FARMLAND], new BuildingColor((v) => FieldTile));
            tileColor.AttachComponent(zones[Zones.RESIDENTIAL], new BuildingColor((v) => v % 2 == 0 ? Building1Tile : Building2Tile));
            zones.Extend(tileColor);
        }
    }
}
