namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A zone component to define legal terrains corresponding to each zone. By default, the only legal terrain is LAND.
    /// </summary>
    public static partial class Zones
    {
        public static readonly string ZonableTerrains = "zonable_terrains";

        public static void RegisterZonableTerrainComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var zones = worldRules.Zones;
            var terrains = worldRules.Terrains;
            ZonableOnlyOn zonableOnlyOnLand = new ZonableOnlyOn(terrains[Terrains.Land]);
            TypedComponents<IZonableTerrain> zonableTerrains = new TypedComponents<IZonableTerrain>(ZonableTerrains, () => zonableOnlyOnLand);
            zonableTerrains.AttachComponent(zones[TWF.Zones.Empty], new AlwaysZonable());
            zones.Extend(zonableTerrains);
        }
    }
}
