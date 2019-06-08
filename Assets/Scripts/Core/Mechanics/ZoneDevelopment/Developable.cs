namespace TWF
{
    /// <summary>
    /// A zone component to mark a zone as developable.
    /// A developable zone will be processed by the ZoneDeveloper agents to create buildings on it.
    /// </summary>
    public partial class Zones
    {
        public static string DEVELOPABLE = "deveoplable";
        public static void DefaultDevelopableComponent(WorldRules worldConfig)
        {
            Entities zones = worldConfig.Zones;
            MarkerComponent developable = new MarkerComponent(DEVELOPABLE);

            developable.MarkEntity(zones[RESIDENTIAL]);
            developable.MarkEntity(zones[FARMLAND]);
            zones.Extend(developable);
        }
    }
}
