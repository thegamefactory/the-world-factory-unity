using TWF.Graphics;
using UnityEngine;

namespace TWF.Input
{
    class ZonerBuilder
    {
        private readonly IReadOnlyEntities Zones;
        private readonly IReadOnlyTypedComponents<Color?> ZoneColor;

        public ZonerBuilder(IWorldView worldView)
        {
            Zones = worldView.Rules.Zones;
            ZoneColor = Zones.GetTypedComponents<Color?>(ZoneLayer.COMPONENT);
        }

        public Tool BuildZoner(string zone, string prefix, string brush)
        {
            Color? color = ZoneColor.GetComponent(Zones[zone]);
            if (color.HasValue)
            {
                color = new Color(color.Value.r, color.Value.g, color.Value.b, color.Value.a / 2);
            }
            return new Tool(prefix + '_' + zone, ToolBehaviors.ZONER, zone, brush, color);
        }
    }
}
