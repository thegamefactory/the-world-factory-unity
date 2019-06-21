namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A zone component to define legal terrains corresponding to each zone. By default, the only legal terrain is LAND.
    /// </summary>
    public static class ZonableTerrains
    {
        public static readonly string ComponentName = "zonable_terrains";

        public static void RegisterZoneComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var zones = worldRules.Zones;
            var terrains = worldRules.Terrains;
            ZonableOnlyOn zonableOnlyOnLand = new ZonableOnlyOn(terrains[Terrains.Land]);
            TypedComponents<IZonableTerrain> zonableTerrains = new TypedComponents<IZonableTerrain>(ComponentName, () => zonableOnlyOnLand);
            zonableTerrains[zones[Zones.Empty]] = new AlwaysZonable();
            zones.Extend(zonableTerrains);
        }
    }
}
