namespace TWF
{
    public static partial class Zones
    {
        public static string MANUALLY_ZONABLE = "manuallyZonable";

        public static void DefaultManuallyZonableComponent(WorldConfig worldConfig)
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
