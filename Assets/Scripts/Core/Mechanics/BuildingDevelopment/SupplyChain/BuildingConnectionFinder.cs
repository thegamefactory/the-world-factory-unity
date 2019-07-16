namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The connection finder is a critical mechanic of the supply chain.
    /// Each building needs connection to locations that satisfy its resources production.
    /// Excess production must find a consumer and excess consumption must find a producer.
    ///
    /// To satisfy the resource production (or consumption, treated equivalently), the connection finder does:
    /// 1) picks a random tile on the map that is able to provide the resource
    /// 2) checks the connectivity between that tile and the candidate tile
    ///
    /// FindConnections returns an Enumerator. Each generated value is a pair.
    /// The first element of the pair is the resourceProduction of the building, and the corresponding value is a vector representing the connection.
    /// If the value is null, it means that no connection was found.
    /// There can be multiple pairs generating with the same resourceProduction, because the production can require many connections to be satisfied.
    /// In case of multiple connections, it is possible that the last value is null, which means that not all the production could be satisfied.
    /// </summary>
    public class BuildingConnectionFinder
    {
        private readonly IPathFinder<Vector> pathFinder;
        private readonly RandomResourceProvider randomResourceProvider;
        private IReadOnlyTypedComponents<BuildingResourceProduction[]> buidlingResourceProductions;
        private Path<Vector> path;

        public BuildingConnectionFinder(IPathFinder<Vector> pathFinder)
        {
            this.pathFinder = pathFinder;
            this.randomResourceProvider = new RandomResourceProvider();
        }

        public void OnNewWorld(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            this.randomResourceProvider.OnNewWorld(worldView);
            this.buidlingResourceProductions = worldView.Rules.BuildingModels
                .GetTypedComponents<BuildingResourceProduction[]>(BuildingModels.BuildingModelResourceProductionComponent);

            this.path = worldView.CreatePath();
        }

        public IEnumerable<(BuildingResourceProduction, Vector?)> FindConnections(Vector pos, int buildingModel)
        {
            BuildingResourceProduction[] resourceProductions = this.buidlingResourceProductions[buildingModel];

            foreach (var resourceProduction in resourceProductions)
            {
                if (resourceProduction.IsConsumer())
                {
                    Vector? candidateProvider = this.randomResourceProvider.GetRandomProvider(resourceProduction.ResourceId);

                    if (!candidateProvider.HasValue)
                    {
                        yield return (resourceProduction, null);
                    }

                    if (!this.pathFinder.FindPath(pos, candidateProvider.Value, resourceProduction.MaxDistance, ref this.path))
                    {
                        yield return (resourceProduction, null);
                    }
                    else
                    {
                        yield return (resourceProduction, candidateProvider.Value);
                    }
                }
            }
        }
    }
}
