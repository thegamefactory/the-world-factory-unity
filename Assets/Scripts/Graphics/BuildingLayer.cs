using UnityEngine;

namespace TWF.Graphics
{
    internal delegate Color BuildingColorProvider(int zoneId, int buildingVariant);

    internal class BuildingLayer : ITileLayer
    {
        private readonly IMapView<Building> buildingMap;
        private readonly IMapView<int> zoneMap;
        private readonly BuildingColorProvider buildingColorProvider;

        public BuildingLayer(IMapView<Building> buildingMap, IMapView<int> zoneMap, BuildingColorProvider buildingColorProvider)
        {
            this.buildingMap = buildingMap;
            this.zoneMap = zoneMap;
            this.buildingColorProvider = buildingColorProvider;
        }

        public Color? GetColor(Vector pos)
        {
            Building building = buildingMap[pos];
            if (null != building)
            {
                return buildingColorProvider(zoneMap[pos], building.Variant);
            }
            else
            {
                return null;
            }
        }
    }
}
