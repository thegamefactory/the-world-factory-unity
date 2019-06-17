namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A zone component to mark a zone as manually zonable.
    /// Such zones can be defined by the user using tools.
    /// </summary>
    public static partial class Zones
    {
        public static readonly string ManuallyZonable = "manually_zonable";

        public static void RegisterManuallyZonableComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);
            NamedEntities zones = worldRules.Zones;
            MarkerComponents manuallyZonable = new MarkerComponents(ManuallyZonable);

            manuallyZonable.MarkEntity(zones[Residential]);
            manuallyZonable.MarkEntity(zones[Farmland]);
            manuallyZonable.MarkEntity(zones[Road]);
            zones.Extend(manuallyZonable);
        }
    }
}
