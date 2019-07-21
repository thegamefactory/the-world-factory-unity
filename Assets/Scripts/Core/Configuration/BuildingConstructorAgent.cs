namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The configuration of the tile development rules.
    /// </summary>
    public static class BuildingConstructorAgent
    {
        public static readonly string ComponentName = "development_voter";

        public static void ConfigureBuildingConstructorAgent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            EmptyLocationVoter emptyLocation = EmptyLocationVoter.GetInstance();
            StochasticVoter stochastic = new StochasticVoter(worldRules.Random);
            worldRules.ConfigProvider.RegisterConfigUpdateListener<double>("tiledev.rate", rate =>
            {
                Contract.Requires(rate >= 0.0 && rate <= 1.0);
                stochastic.DevelopmentRate = rate;
            });

            var buildingTransportLinkFinder = new BuildingTransportLinkFinder();
            worldRules.OnNewWorldListener += buildingTransportLinkFinder.OnNewWorld;
            worldRules.ConfigProvider.RegisterConfigUpdateListener<int>("transport_location.max_distance", maxDistance =>
            {
                Contract.Requires(maxDistance > 0);
                buildingTransportLinkFinder.MaxDistance = maxDistance;
            });

            var buildingConnectionFinder = new BuildingConnectionFinder(CreatePathFinder(), new RandomResourceProvider(buildingTransportLinkFinder));
            worldRules.OnNewWorldListener += buildingConnectionFinder.OnNewWorld;


            var buildingResource = new BuildingResourceVoter(buildingConnectionFinder, buildingTransportLinkFinder);

            CombinedBuildingDevelopmentVoter combinedTileDevelopmentVoter = new CombinedBuildingDevelopmentVoter(emptyLocation, stochastic, buildingResource);

            BuildingConstructorVoter buildingConstructionVoter = new BuildingConstructorVoter(combinedTileDevelopmentVoter, new ZoneBuildingModels());

            worldRules.OnNewWorldListener += w => buildingConstructionVoter.OnNewWorld(w);

            var zoneDeveloper = new BuildingConstructor(buildingConstructionVoter);
            worldRules.Agents[zoneDeveloper.Name] = new ScheduledAgent(zoneDeveloper, 1.0f);
        }

        private static IPathFinder<Vector> CreatePathFinder()
        {
            ManatthanHeuristicProvider manatthanHeuristicProvider = new ManatthanHeuristicProvider();

            int maxExplorationSpace = 1024;
            var pathFinder = new AStarPathFinder<Vector>(manatthanHeuristicProvider, maxExplorationSpace);
            return pathFinder;
        }
    }
}