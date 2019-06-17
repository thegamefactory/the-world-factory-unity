namespace TWF.Graphics
{
    using UnityEngine;

    public delegate Color BuildingColor(int buildingVariant);

    public delegate Color BuildingColorProvider(int zoneId, int buildingVariant);

    public class BuildingLayer : ITileLayer
    {
        public static readonly string Component = "building_color";

        private readonly IMapView<int> buildingMap;
        private readonly IReadOnlyTypedComponents<int> buildingVariants;
        private readonly IMapView<int> zoneMap;
        private readonly BuildingColorProvider buildingColorProvider;

        public BuildingLayer(IWorldView worldView)
        {
            this.buildingMap = worldView.GetBuildingMapView();
            this.buildingVariants = worldView.Buildings.GetTypedComponents<int>(BuildingVariants.Component);
            this.zoneMap = worldView.GetZoneMapView();

            var buildingColor = worldView.Rules.Zones.GetTypedComponents<BuildingColor>(Component);
            this.buildingColorProvider = (zoneId, buildingId) => buildingColor.GetComponent(zoneId).Invoke(buildingId);
        }

        public string Name => Component;

        public Color? GetColor(Vector pos)
        {
            int building = this.buildingMap[pos];
            if (building != MapTypes.NoBuilding)
            {
                return this.buildingColorProvider(this.zoneMap[pos], this.buildingVariants.GetComponent(building));
            }
            else
            {
                return null;
            }
        }
    }
}
