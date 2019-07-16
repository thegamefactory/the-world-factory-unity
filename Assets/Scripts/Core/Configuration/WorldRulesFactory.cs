namespace TWF
{
    using System;
    using System.Diagnostics.Contracts;

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

        public IConfigProvider ConfigProvider { get; set; }

        public static WorldRulesCustomizer DefaultCustomizer()
        {
            WorldRulesCustomizer wrc = Zones.RegisterDefaults;
            wrc += Terrains.RegisterDefaults;
            wrc += Resources.RegisterDefaults;
            wrc += BuildingModels.RegisterDefaults;
            wrc += Zones.RegisterManuallyZonableComponent;
            wrc += Zones.RegisterZonableTerrainsComponent;
            wrc += Zones.RegisterZonesDefaultBuildingModel;
            wrc += BuildingModels.RegisterBuildingModelZoneComponent;
            wrc += BuildingModels.RegisterBuilidingModelResourceProductionComponent;
            wrc += Buildings.RegisterBuildingModelComponent;
            wrc += Buildings.RegisterBuildingConnections;
            wrc += TileDevelopmentAgent.ConfigureTileDevelopment;
            wrc += BuildingVariants.RegisterBuildingComponent;
            wrc += ToolBehaviors.RegisterDefaults;
            wrc += ToolBrushes.RegisterDefaults;
            return wrc;
        }

        public WorldRules Create(Random random)
        {
            Contract.Requires(this.ConfigProvider != null);
            Contract.Requires(this.WorldRulesCustomizer != null);

            WorldRules wr = new WorldRules(random)
            {
                ConfigProvider = this.ConfigProvider,
            };
            this.WorldRulesCustomizer(wr);
            return wr;
        }
    }
}
