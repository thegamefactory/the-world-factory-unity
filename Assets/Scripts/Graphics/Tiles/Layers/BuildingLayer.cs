using UnityEngine;

namespace TWF.Graphics
{
    public delegate Color BuildingColor(int buildingVariant);
    public delegate Color BuildingColorProvider(int zoneId, int buildingVariant);

    public class BuildingLayer : ITileLayer
    {
        private IMapView<Building> buildingMap;
        private IMapView<int> zoneMap;
        private BuildingColorProvider buildingColorProvider;

        public static string COMPONENT = "buildingColor";

        public void OnNewWorld(IWorldView worldView)
        {
            buildingMap = worldView.GetBuildingMapView();
            zoneMap = worldView.GetZoneMapView();

            var buildingColor = worldView.Zones.GetTypedComponentRegistry<BuildingColor>(COMPONENT);
            buildingColorProvider = (zoneId, buildingId) => buildingColor.GetComponent(zoneId).Invoke(buildingId);
        }

        public string Name => COMPONENT;

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
