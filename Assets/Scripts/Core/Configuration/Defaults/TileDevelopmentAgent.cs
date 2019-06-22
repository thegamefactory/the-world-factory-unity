namespace TWF
{
    using System;
    using System.Diagnostics.Contracts;

    public static class TileDevelopmentAgent
    {
        public static readonly string ComponentName = "development_voter";

        public static void ConfigureTileDevelopment(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            EmptyLocationVoter emptyLocation = EmptyLocationVoter.GetInstance();
            StochasticVoter stochastic = new StochasticVoter(0.01, worldRules.Random);
            CombinedTileDevelopmentVoter combinedTileDevelopmentVoter = new CombinedTileDevelopmentVoter(emptyLocation, stochastic);

            RootTileDevelopmentVoter rootTileDevelopmentVoter = new RootTileDevelopmentVoter(combinedTileDevelopmentVoter, new ZoneBuildingModels());

            worldRules.OnNewWorldListener += w => rootTileDevelopmentVoter.OnNewWorld(w);

            var zoneDeveloper = new TileDeveloper(rootTileDevelopmentVoter, worldRules.Random);
            worldRules.Agents[zoneDeveloper.Name] = new ScheduledAgent(zoneDeveloper, 1.0f);
        }
    }
}