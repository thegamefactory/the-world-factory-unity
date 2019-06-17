namespace TWF.Input
{
    using TWF.Graphics;
    using UnityEngine;

    internal class ZonerBuilder
    {
        private readonly IReadOnlyNamedEntities zones;
        private readonly IReadOnlyTypedComponents<Color?> zoneColor;

        public ZonerBuilder(IWorldView worldView)
        {
            this.zones = worldView.Rules.Zones;
            this.zoneColor = this.zones.GetTypedComponents<Color?>(ZoneLayer.Component);
        }

        public Tool BuildZoner(string zone, string prefix, string brush)
        {
            Color? color = this.zoneColor.GetComponent(this.zones[zone]);
            if (color.HasValue)
            {
                color = new Color(color.Value.r, color.Value.g, color.Value.b, color.Value.a / 2);
            }

            return new Tool(prefix + '_' + zone, ToolBehaviors.ZONER, zone, brush, color);
        }
    }
}
