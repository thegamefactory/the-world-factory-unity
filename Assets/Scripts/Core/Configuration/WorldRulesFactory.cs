namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Factory design pattern for WorldRules.
    /// An extension point is provided with WorldRulesCustomizer delegates that can be register and allow to either redefine or create new rules.
    /// </summary>
    public class WorldRulesFactory
    {
        public WorldRulesFactory()
        {
            this.WorldRulesCustomizer = DefaultCustomizer();
        }

        public WorldRulesCustomizer WorldRulesCustomizer { get; set; }

        public static WorldRulesCustomizer DefaultCustomizer()
        {
            WorldRulesCustomizer wrc = Zones.DefaultDevelopableComponent;
            wrc += Zones.DefaultManuallyZonableComponent;
            wrc += Zones.DefaultZonableTerrainComponent;
            wrc += BuildingModels.DefaultBuildingModelComponent;
            wrc += BuildingVariants.DefaultBuildingVariantComponent;
            return wrc;
        }

        public WorldRules Create(Random random)
        {
            var zones = Zones.DefaultZones();
            var terrains = Terrains.DefaultTerrains();
            var buildingModels = BuildingModels.DefaultBuildingModels();

            WorldRules wr = new WorldRules(
                random,
                zones,
                terrains,
                buildingModels,
                Agents.AllAgents(random),
                new Dictionary<string, Func<string, IToolBehavior>>() { [ToolBehaviors.ZONER] = ToolBehaviors.Zoners(zones) },
                ToolBrushes.AllToolBrushes);

            this.WorldRulesCustomizer(wr);

            return wr;
        }
    }
}
