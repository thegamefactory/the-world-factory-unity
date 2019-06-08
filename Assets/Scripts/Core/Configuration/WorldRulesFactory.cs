using System;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// Factory design pattern for WorldRules.
    /// An extension point is provided with WorldRulesCustomizer delegates that can be register and allow to either redefine or create new rules.
    /// </summary>
    public class WorldRulesFactory
    {
        public WorldRulesCustomizer WorldRulesCustomizer { get; set; }

        public WorldRulesFactory()
        {
            WorldRulesCustomizer = DefaultCustomizer();
        }

        public static WorldRulesCustomizer DefaultCustomizer()
        {
            WorldRulesCustomizer defaultDevelopableComponent = Zones.DefaultDevelopableComponent;
            WorldRulesCustomizer defaultManuallyZonableComponent = Zones.DefaultManuallyZonableComponent;
            WorldRulesCustomizer defaultZonableTerrainsComponent = Zones.DefaultZonableTerrainComponent;
            return defaultDevelopableComponent + defaultManuallyZonableComponent + defaultZonableTerrainsComponent;
        }

        public WorldRules Create(Random random)
        {
            Entities zones = Zones.DefaultZones();
            Entities terrains = Terrains.DefaultTerrains();

            WorldRules wr = new WorldRules(
                zones,
                terrains,
                Agents.AllAgents(random),
                new Dictionary<string, Func<string, IToolBehavior>>() { [ToolBehaviors.ZONER] = ToolBehaviors.Zoners(zones) },
                ToolBrushes.AllToolBrushes);

            WorldRulesCustomizer(wr);

            return wr;
        }
    }
}
