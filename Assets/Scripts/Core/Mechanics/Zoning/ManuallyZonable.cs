namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A zone component to mark a zone as manually zonable.
    /// Such zones can be defined by the user using tools.
    /// </summary>
    public static class ManuallyZonable
    {
        public static readonly string ComponentName = "manually_zonable";

        public static void RegisterZoneComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);
            NamedEntities zones = worldRules.Zones;
            var manuallyZonable = new TypedComponents<bool>(ComponentName, () => false);

            manuallyZonable[zones[Zones.Commercial]] = true;
            manuallyZonable[zones[Zones.Farmland]] = true;
            manuallyZonable[zones[Zones.Residential]] = true;
            manuallyZonable[zones[Zones.Road]] = true;
            zones.Extend(manuallyZonable);
        }
    }
}
