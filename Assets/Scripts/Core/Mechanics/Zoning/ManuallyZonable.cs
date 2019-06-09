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

        public static void DefaultManuallyZonableComponent(WorldRules worldConfig)
        {
            Contract.Requires(worldConfig != null);
            Entities zones = worldConfig.Zones;
            MarkerComponents manuallyZonable = new MarkerComponents(ManuallyZonable);

            manuallyZonable.MarkEntity(zones[Residential]);
            manuallyZonable.MarkEntity(zones[Farmland]);
            manuallyZonable.MarkEntity(zones[Road]);
            zones.Extend(manuallyZonable);
        }
    }
}
