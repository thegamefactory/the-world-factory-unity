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

            // these zones will develop (at the condition that there is no building on the zone)
            developable.GetComponent(zones[Zones.Commercial]).RegisterVoter(emptyLocation);
            developable.GetComponent(zones[Zones.Farmland]).RegisterVoter(emptyLocation);
            developable.GetComponent(zones[Zones.Residential]).RegisterVoter(emptyLocation);

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