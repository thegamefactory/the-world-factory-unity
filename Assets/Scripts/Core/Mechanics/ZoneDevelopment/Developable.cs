namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A zone component to mark a zone as developable.
    /// A developable zone will be processed by the ZoneDeveloper agents to create buildings on it.
    /// </summary>
    public partial class Zones
    {
        public static readonly string DEVELOPABLE = "deveoplable";

        public static void DefaultDevelopableComponent(WorldRules worldConfig)
        {
            Contract.Requires(worldConfig != null);

            Entities zones = worldConfig.Zones;
            MarkerComponents developable = new MarkerComponents(DEVELOPABLE);

            developable.MarkEntity(zones[Residential]);
            developable.MarkEntity(zones[Farmland]);
            zones.Extend(developable);
        }
    }
}
