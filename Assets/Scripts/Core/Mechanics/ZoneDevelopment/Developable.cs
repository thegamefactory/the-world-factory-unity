namespace TWF
{
    public partial class Zones
    {
        public static string DEVELOPABLE = "deveoplable";
        public static void DefaultDevelopableComponent(WorldConfig worldConfig)
        {
            Registry zones = worldConfig.Zones;
            MarkerComponent developable = new MarkerComponent(DEVELOPABLE);

            developable.MarkEntity(zones[RESIDENTIAL]);
            developable.MarkEntity(zones[FARMLAND]);
            zones.Extend(developable);
        }
    }
}
