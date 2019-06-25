namespace TWF
{
    using System.Diagnostics.Contracts;

    public static class Zones
    {
        public static readonly string EntitiesName = "zone";
        public static readonly string Empty = "empty";

        public static readonly string Commercial = "commercial";
        public static readonly string Farmland = "farmland";
        public static readonly string Residential = "residential";
        public static readonly string Road = "road";

        public static readonly string ManuallyZonable = "manually_zonable";
        public static readonly string ZonableTerrains = "zonable_terrains";
        public static readonly string DefaultBuildingModel = "default_building_model";

        public static void RegisterDefaults(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            NamedEntities zones = worldRules.Zones;
            zones.Register(Empty);
            zones.Register(Commercial);
            zones.Register(Farmland);
            zones.Register(Residential);
            zones.Register(Road);
        }

        public static void RegisterManuallyZonableComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);
            NamedEntities zones = worldRules.Zones;
            var manuallyZonable = new TypedComponents<bool>(ManuallyZonable, () => false);

            manuallyZonable[zones[Zones.Commercial]] = true;
            manuallyZonable[zones[Zones.Farmland]] = true;
            manuallyZonable[zones[Zones.Residential]] = true;
            manuallyZonable[zones[Zones.Road]] = true;
            zones.Extend(manuallyZonable);
        }

        /// <summary>
        /// A zone component to define legal terrains corresponding to each zone. By default, the only legal terrain is Land.
        /// </summary>
        public static void RegisterZonableTerrainsComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var zones = worldRules.Zones;
            var terrains = worldRules.Terrains;
            ZonableOnlyOn zonableOnlyOnLand = new ZonableOnlyOn(terrains[Terrains.Land]);
            TypedComponents<IZonableTerrain> zonableTerrains = new TypedComponents<IZonableTerrain>(ZonableTerrains, () => zonableOnlyOnLand);
            zonableTerrains[zones[Zones.Empty]] = new AlwaysZonable();
            zones.Extend(zonableTerrains);
        }

        /// <summary>
        /// The default building model for each zone. This is used as estimate when computing how empty zones will develop.
        /// </summary>
        public static void RegisterZonesDefaultBuildingModel(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var zones = worldRules.Zones;
            var buildingModels = worldRules.BuildingModels;

            TypedComponents<int> defaultBuildingModels = new TypedComponents<int>(DefaultBuildingModel, () => BuildingModels.NoModel);
            defaultBuildingModels[zones[Zones.Farmland]] = buildingModels[BuildingModels.Farm];
            defaultBuildingModels[zones[Zones.Residential]] = buildingModels[BuildingModels.House];
            defaultBuildingModels[zones[Zones.Commercial]] = buildingModels[BuildingModels.ConvenienceStore];

            zones.Extend(defaultBuildingModels);
        }
    }
}
