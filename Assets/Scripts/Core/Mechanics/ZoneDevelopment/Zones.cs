namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A zone component to define the development behavior..
    /// A zone will be developed by the ZoneDeveloper agents to create buildings on it if the voters is favorable to it.
    /// </summary>
    public partial class Zones
    {
        public static readonly string DevelopmentVoter = "development_voter";

        public static void RegisterDevelopmentVoter(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            NamedEntities zones = worldRules.Zones;
            TypedComponents<CombinedDevelopmentVoter> developable = new TypedComponents<CombinedDevelopmentVoter>(
                DevelopmentVoter, () => new CombinedDevelopmentVoter());

            EmptyLocationVoter emptyLocation = EmptyLocationVoter.GetInstance();

            // these zones will develop (at the condition that there is no building on the zone)
            developable.GetComponent(zones[Residential]).RegisterVoter(emptyLocation);
            developable.GetComponent(zones[Farmland]).RegisterVoter(emptyLocation);

            zones.Extend(developable);

            worldRules.OnNewWorldListener += world =>
            {
                foreach (CombinedDevelopmentVoter combinedDevelopmentVoter in developable.GetMatchingComponents(cdv => cdv.VotersCount > 0))
                {
                    combinedDevelopmentVoter.OnNewWorld(world);
                }
            };
        }
    }
}