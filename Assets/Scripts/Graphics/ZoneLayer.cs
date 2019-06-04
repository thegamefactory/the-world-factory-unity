using UnityEngine;

namespace TWF.Graphics
{
    internal delegate Color? ZoneColorProvider(int zoneId);
    class ZoneLayer : ITileLayer
    {
        private readonly IMapView<int> zoneMap;
        private readonly ZoneColorProvider zoneColorProvider;
        public ZoneLayer(IMapView<int> zoneMap, ZoneColorProvider zoneColorProvider)
        {
            this.zoneMap = zoneMap;
            this.zoneColorProvider = zoneColorProvider;
        }
        public Color? GetColor(Vector pos)
        {
            return zoneColorProvider(zoneMap[pos]);
        }
    }
}
