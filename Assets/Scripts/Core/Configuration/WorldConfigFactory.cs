using System;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// Factory design pattern for WorldConfig
    /// </summary>
    public class WorldConfigFactory
    {
        public WorldConfigCustomizer WorldConfigCustomizer { get; set; }

        public WorldConfigFactory()
        {
            WorldConfigCustomizer = DefaultCustomizer();
        }

        public static WorldConfigCustomizer DefaultCustomizer()
        {
            WorldConfigCustomizer defaultDevelopableComponent = Zones.DefaultDevelopableComponent;
            WorldConfigCustomizer defaultManuallyZonableComponent = Zones.DefaultManuallyZonableComponent;
            WorldConfigCustomizer defaultZonableTerrainsComponent = Zones.DefaultZonableTerrainComponent;
            return defaultDevelopableComponent + defaultManuallyZonableComponent + defaultZonableTerrainsComponent;
        }

        public WorldConfig CreateDefaultWorldConfig(Random random)
        {
            Registry zones = Zones.DefaultZones();
            Registry terrains = Terrains.DefaultTerrains();

            WorldConfig wc = new WorldConfig(
                zones,
                terrains,
                Agents.AllAgents(random),
                new Dictionary<string, Func<string, IToolBehavior>>() { [ToolBehaviors.ZONER] = ToolBehaviors.Zoners(zones) },
                ToolBrushes.AllToolBrushes);

            WorldConfigCustomizer(wc);

            return wc;
        }
    }
}
