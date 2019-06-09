namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A zone component to define legal terrains corresponding to each zone. By default, the only legal terrain is LAND.
    /// </summary>
    public static partial class Zones
    {
        public static readonly string ZonableTerrains = "zonable_terrains";

        public static void DefaultZonableTerrainComponent(WorldRules worldConfig)
        {
            Contract.Requires(worldConfig != null);

            var zones = worldConfig.Zones;
            var terrains = worldConfig.Terrains;
            ZonableOnlyOn zonableOnlyOnLand = new ZonableOnlyOn(terrains[Terrains.Land]);
            TypedComponents<IZonableTerrain> zonableTerrains = new TypedComponents<IZonableTerrain>(ZonableTerrains, () => zonableOnlyOnLand);
            zonableTerrains.AttachComponent(zones[TWF.Zones.EMPTY], new AlwaysZonable());
            zones.Extend(zonableTerrains);
        }
    }
}
