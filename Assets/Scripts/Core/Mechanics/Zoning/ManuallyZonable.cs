namespace TWF
{
    /// <summary>
    /// A zone component to mark a zone as manually zonable.
    /// Such zones can be defined by the user using tools.
    /// </summary>
    public static partial class Zones
    {
        public static string MANUALLY_ZONABLE = "manuallyZonable";

        public static void DefaultManuallyZonableComponent(WorldRules worldConfig)
        {
            Entities zones = worldConfig.Zones;
            MarkerComponent manuallyZonable = new MarkerComponent(MANUALLY_ZONABLE);

            manuallyZonable.MarkEntity(zones[RESIDENTIAL]);
            manuallyZonable.MarkEntity(zones[FARMLAND]);
            manuallyZonable.MarkEntity(zones[ROAD]);
            zones.Extend(manuallyZonable);
        }
    }
}
