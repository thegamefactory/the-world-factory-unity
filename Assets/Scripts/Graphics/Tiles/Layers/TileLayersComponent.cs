using UnityEngine;

namespace TWF.Graphics
{
    public class TileLayersComponent : MonoBehaviour
    {
        public Color LandTile;
        public Color ResidentialTile;
        public Color FarmlandTile;
        public Color FieldTile;
        public Color Building1Tile;
        public Color Building2Tile;
        public Color WaterTile;
        public Color RoadTile;

        public Color ErrorTile;

        public void Awake()
        {
            Root.GameService.WorldFactory.WorldConfigFactory.WorldRulesCustomizer += ConfigureTileColors;
            var worldView = Root.WorldView;
            var tileLayers = Root.GameService.GraphicConfig.TileLayers;
            tileLayers.RegisterLayerProvider(wv => new ZoneLayer(wv));
            tileLayers.RegisterLayerProvider(wv => new BuildingLayer(wv));
            tileLayers.RegisterLayerProvider(wv => new TerrainLayer(wv));

            var inputController = GameObject.Find("Input").GetComponent<Input.InputController>();
            tileLayers.RegisterLayerProvider(wv => new ToolPreviewLayer(
                inputController.GetToolPreviewOutcomeMapProvider(),
                inputController.GetToolSuccessColorProvider(),
                ErrorTile
                ));
        }

        private void ConfigureTileColors(WorldRules worldConfig)
        {
            Entities terrains = worldConfig.Terrains;
            Entities zones = worldConfig.Zones;

            TypedComponent<Color> terrainColor = new TypedComponent<Color>(TerrainLayer.COMPONENT, () => ErrorTile);
            terrainColor.AttachComponent(terrains[Terrains.LAND], LandTile);
            terrainColor.AttachComponent(terrains[Terrains.WATER], WaterTile);
            terrains.Extend(terrainColor);

            TypedComponent<Color?> zoneColor = new TypedComponent<Color?>(ZoneLayer.COMPONENT, () => null);
            zoneColor.AttachComponent(zones[Zones.FARMLAND], FarmlandTile);
            zoneColor.AttachComponent(zones[Zones.RESIDENTIAL], ResidentialTile);
            zoneColor.AttachComponent(zones[Zones.ROAD], RoadTile);
            zones.Extend(zoneColor);

            TypedComponent<BuildingColor> tileColor = new TypedComponent<BuildingColor>(BuildingLayer.COMPONENT, () => (v) => ErrorTile);
            tileColor.AttachComponent(zones[Zones.FARMLAND], new BuildingColor((v) => FieldTile));
            tileColor.AttachComponent(zones[Zones.RESIDENTIAL], new BuildingColor((v) => v % 2 == 0 ? Building1Tile : Building2Tile));
            zones.Extend(tileColor);
        }
    }
}
