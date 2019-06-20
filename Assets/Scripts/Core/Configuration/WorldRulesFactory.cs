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
            WorldRulesCustomizer wrc = Zones.RegisterDefaults;
            wrc += Terrains.RegisterDefaults;
            wrc += BuildingModels.RegisterDefaults;
            wrc += ZoneDevelopmentVoter.RegisterZoneComponent;
            wrc += ManuallyZonable.RegisterZoneComponent;
            wrc += ZonableTerrains.RegisterZoneComponent;
            wrc += BuildingModels.RegisterBuilidingModelComponent;
            wrc += BuildingVariants.RegisterBuildingComponent;
            wrc += Agents.RegisterDefaults;
            wrc += ToolBehaviors.RegisterDefaults;
            wrc += ToolBrushes.RegisterDefaults;
            return wrc;
        }

        public WorldRules Create(Random random)
        {
            WorldRules wr = new WorldRules(random);
            this.WorldRulesCustomizer(wr);
            return wr;
        }
    }
}
