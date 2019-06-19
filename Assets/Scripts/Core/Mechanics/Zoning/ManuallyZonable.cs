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
            var manuallyZonable = new TypedComponents<bool>(ManuallyZonable, () => false);

            manuallyZonable.SetComponent(zones[Residential], true);
            manuallyZonable.SetComponent(zones[Farmland], true);
            manuallyZonable.SetComponent(zones[Road], true);
            zones.Extend(manuallyZonable);
        }
    }
}
