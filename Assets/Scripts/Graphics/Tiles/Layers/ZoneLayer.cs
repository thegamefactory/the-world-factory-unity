﻿using UnityEngine;

namespace TWF.Graphics
{
    internal delegate Color? ZoneColorProvider(int zoneId);
    public class ZoneLayer : ITileLayer
    {
        private IMapView<int> zoneMap;
        private ZoneColorProvider zoneColorProvider;

        public static string COMPONENT = "zone_color";

        public ZoneLayer(IWorldView worldView)
        {
            this.zoneMap = worldView.GetZoneMapView();

            var zoneColor = worldView.Rules.Zones.GetTypedComponents<Color?>(COMPONENT);
            this.zoneColorProvider = (z) => zoneColor.GetComponent(z);
        }

        public string Name => COMPONENT;

        public Color? GetColor(Vector pos)
        {
            return zoneColorProvider(zoneMap[pos]);
        }
    }
}
