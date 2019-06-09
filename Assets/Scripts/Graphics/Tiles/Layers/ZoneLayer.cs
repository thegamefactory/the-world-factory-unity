namespace TWF.Graphics
{
    using UnityEngine;

    internal delegate Color? ZoneColorProvider(int zoneId);

    public class ZoneLayer : ITileLayer
    {
        public static readonly string Component = "zone_color";

        private readonly IMapView<int> zoneMap;
        private readonly ZoneColorProvider zoneColorProvider;

        public ZoneLayer(IWorldView worldView)
        {
            this.zoneMap = worldView.GetZoneMapView();

            var zoneColor = worldView.Rules.Zones.GetTypedComponents<Color?>(Component);
            this.zoneColorProvider = (z) => zoneColor.GetComponent(z);
        }

        public string Name => Component;

        public Color? GetColor(Vector pos)
        {
            return this.zoneColorProvider(this.zoneMap[pos]);
        }
    }
}
