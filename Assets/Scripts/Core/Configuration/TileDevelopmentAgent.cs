namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The configuration of the tile development rules.
    /// </summary>
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

            var buildingResource = new BuildingResourceVoter(CreatePathFinder(worldRules, roadGraph));

            CombinedTileDevelopmentVoter combinedTileDevelopmentVoter = new CombinedTileDevelopmentVoter(emptyLocation, stochastic, buildingResource);

            RootTileDevelopmentVoter rootTileDevelopmentVoter = new RootTileDevelopmentVoter(combinedTileDevelopmentVoter, new ZoneBuildingModels());

            worldRules.OnNewWorldListener += w => rootTileDevelopmentVoter.OnNewWorld(w);

            var zoneDeveloper = new TileDeveloper(rootTileDevelopmentVoter);
            worldRules.Agents[zoneDeveloper.Name] = new ScheduledAgent(zoneDeveloper, 1.0f);
        }

        private static IPathFinder<Vector> CreatePathFinder(WorldRules worldRules, RoadGraph roadGraph)
        {
            ManatthanHeuristicProvider manatthanHeuristicProvider = new ManatthanHeuristicProvider();

            int maxExplorationSpace = 1024;

            var pathFinder = new RoadConnectedPathFinder(
                new AStarPathFinder<Vector>(roadGraph, manatthanHeuristicProvider, maxExplorationSpace));

            worldRules.OnNewWorldListener += pathFinder.OnNewWorld;

            return pathFinder;
        }
    }
}