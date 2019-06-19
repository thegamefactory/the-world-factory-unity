namespace TWF.Graphics
{
    using UnityEngine;

    public class TileLayersComponent : MonoBehaviour
    {
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
        public Color LandTile;
        public Color ResidentialTile;
        public Color FarmlandTile;
        public Color FieldTile;
        public Color Building1Tile;
        public Color Building2Tile;
        public Color WaterTile;
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
            terrainColor.SetComponent(terrains[Terrains.Land], this.LandTile);
            terrainColor.SetComponent(terrains[Terrains.Water], this.WaterTile);
            terrains.Extend(terrainColor);

            TypedComponents<Color?> zoneColor = new TypedComponents<Color?>(ZoneLayer.Component, () => null);
            zoneColor.SetComponent(zones[Zones.Farmland], this.FarmlandTile);
            zoneColor.SetComponent(zones[Zones.Residential], this.ResidentialTile);
            zoneColor.SetComponent(zones[Zones.Road], this.RoadTile);
            zones.Extend(zoneColor);

            TypedComponents<BuildingColor> tileColor = new TypedComponents<BuildingColor>(BuildingLayer.Component, () => (v) => this.ErrorTile);
            tileColor.SetComponent(zones[Zones.Farmland], new BuildingColor((v) => this.FieldTile));
            tileColor.SetComponent(zones[Zones.Residential], new BuildingColor((v) => v % 2 == 0 ? this.Building1Tile : this.Building2Tile));
            zones.Extend(tileColor);
        }
    }
}
