namespace TWF.Graphics
{
    using UnityEngine;

    public class TileLayersComponent : MonoBehaviour
    {
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
        public Color LandTile;
        public Color WaterTile;

        public Color CommercialTile;
        public Color FarmlandTile;
        public Color ResidentialTile;

        public Color FieldTile;

        public Color Building1Tile;
        public Color Building2Tile;

        public Color RoadTile;

        public Color ErrorTile;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

        public void Awake()
        {
            Root.GameService.WorldFactory.WorldRulesFactory.WorldRulesCustomizer += this.ConfigureTileColors;
            var worldView = Root.WorldView;
            var tileLayers = Root.GameService.GraphicConfig.TileLayers;
            tileLayers.RegisterLayerProvider(wv => new ZoneLayer(wv));
            tileLayers.RegisterLayerProvider(wv => new BuildingLayer(wv));
            tileLayers.RegisterLayerProvider(wv => new TerrainLayer(wv));

            var inputController = GameObject.Find("Input").GetComponent<TWF.Input.InputController>();
            tileLayers.RegisterLayerProvider(wv => new ToolPreviewLayer(
                inputController.GetToolPreviewOutcomeMapProvider(),
                inputController.GetToolSuccessColorProvider(),
                this.ErrorTile));
        }

        private void ConfigureTileColors(WorldRules worldConfig)
        {
            var terrains = worldConfig.Terrains;
            var zones = worldConfig.Zones;

            TypedComponents<Color> terrainColor = new TypedComponents<Color>(TerrainLayer.Component, () => this.ErrorTile);
            terrainColor[terrains[Terrains.Land]] = this.LandTile;
            terrainColor[terrains[Terrains.Water]] = this.WaterTile;
            terrains.Extend(terrainColor);

            TypedComponents<Color?> zoneColor = new TypedComponents<Color?>(ZoneLayer.Component, () => null);
            zoneColor[zones[Zones.Commercial]] = this.CommercialTile;
            zoneColor[zones[Zones.Farmland]] = this.FarmlandTile;
            zoneColor[zones[Zones.Residential]] = this.ResidentialTile;
            zoneColor[zones[Zones.Road]] = this.RoadTile;
            zones.Extend(zoneColor);

            TypedComponents<BuildingColor> tileColor = new TypedComponents<BuildingColor>(BuildingLayer.Component, () => (v) => this.ErrorTile);
            tileColor[zones[Zones.Farmland]] = new BuildingColor((v) => this.FieldTile);
            tileColor[zones[Zones.Commercial]] = new BuildingColor((v) => this.Building1Tile);
            tileColor[zones[Zones.Residential]] = new BuildingColor((v) => this.Building2Tile);
            zones.Extend(tileColor);
        }
    }
}
