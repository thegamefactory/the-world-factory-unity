using UnityEngine;

namespace TWF.Graphics
{
    internal delegate Color? ZoneColorProvider(int zoneId);
    public class ZoneLayer : ITileLayer
    {
        private IMapView<int> zoneMap;
        private ZoneColorProvider zoneColorProvider;

        public static string COMPONENT = "zoneColor";

        public ZoneLayer(IWorldView worldView)
        {
            this.zoneMap = worldView.GetZoneMapView();

            var zoneColor = worldView.Zones.GetTypedComponents<Color?>(COMPONENT);
            this.zoneColorProvider = (z) => zoneColor.GetComponent(z);
        }

        public string Name => COMPONENT;

        public Color? GetColor(Vector pos)
        {
            return zoneColorProvider(zoneMap[pos]);
        }
    }
}
