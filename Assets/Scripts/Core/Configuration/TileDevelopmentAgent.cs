namespace TWF
{
    using System.Diagnostics.Contracts;

    public static class TileDevelopmentAgent
    {
        public static readonly string ComponentName = "development_voter";

        public static void ConfigureTileDevelopment(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            EmptyLocationVoter emptyLocation = EmptyLocationVoter.GetInstance();
            StochasticVoter stochastic = new StochasticVoter(worldRules.Random);
            worldRules.ConfigProvider.RegisterConfigUpdateListener<double>("tiledev.rate", rate =>
            {
                Contract.Requires(rate >= 0.0 && rate <= 1.0);
                stochastic.DevelopmentRate = rate;
            });

            RoadGraph roadGraph = new RoadGraph();
            worldRules.OnNewWorldListener += roadGraph.OnNewWorld;

            ManatthanHeuristicProvider manatthanHeuristicProvider = new ManatthanHeuristicProvider();

            int maxExplorationSpace = 1024;

            BuildingResourceVoter buildingResource = new BuildingResourceVoter(
                new AStarPathFinder<Vector>(roadGraph, manatthanHeuristicProvider, maxExplorationSpace));

            CombinedTileDevelopmentVoter combinedTileDevelopmentVoter = new CombinedTileDevelopmentVoter(emptyLocation, stochastic, buildingResource);

            RootTileDevelopmentVoter rootTileDevelopmentVoter = new RootTileDevelopmentVoter(combinedTileDevelopmentVoter, new ZoneBuildingModels());

            worldRules.OnNewWorldListener += w => rootTileDevelopmentVoter.OnNewWorld(w);

            var zoneDeveloper = new TileDeveloper(rootTileDevelopmentVoter);
            worldRules.Agents[zoneDeveloper.Name] = new ScheduledAgent(zoneDeveloper, 1.0f);
        }
    }
}