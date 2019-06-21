namespace TWF
{
    using System.Diagnostics.Contracts;

    public static class ZoneDevelopmentVoter
    {
        public static readonly string ComponentName = "development_voter";

        public static void RegisterZoneComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            NamedEntities zones = worldRules.Zones;
            TypedComponents<CombinedZoneDevelopmentVoter> developable = new TypedComponents<CombinedZoneDevelopmentVoter>(
                ComponentName, () => new CombinedZoneDevelopmentVoter());

            EmptyLocationVoter emptyLocation = EmptyLocationVoter.GetInstance();
            StochasticVoter stochastic = new StochasticVoter(0.01, worldRules.Random);

            // these zones will develop (at the condition that there is no building on the zone)
            developable[zones[Zones.Commercial]].RegisterVoters(emptyLocation, stochastic);
            developable[zones[Zones.Farmland]].RegisterVoters(emptyLocation, stochastic);
            developable[zones[Zones.Residential]].RegisterVoters(emptyLocation, stochastic);

            zones.Extend(developable);

            worldRules.OnNewWorldListener += world =>
            {
                foreach (CombinedZoneDevelopmentVoter combinedDevelopmentVoter in developable.GetMatchingComponents(cdv => cdv.VotersCount > 0))
                {
                    combinedDevelopmentVoter.OnNewWorld(world);
                }
            };
        }
    }
}