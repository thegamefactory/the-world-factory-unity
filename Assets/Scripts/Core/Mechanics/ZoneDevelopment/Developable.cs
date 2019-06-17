namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A zone component to mark a zone as developable.
    /// A developable zone will be processed by the ZoneDeveloper agents to create buildings on it.
    /// </summary>
    public partial class Zones
    {
        public static readonly string Developable = "deveoplable";

        public static void RegisterDevelopableComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            NamedEntities zones = worldRules.Zones;
            MarkerComponents developable = new MarkerComponents(Developable);

            developable.MarkEntity(zones[Residential]);
            developable.MarkEntity(zones[Farmland]);
            zones.Extend(developable);
        }
    }
}
