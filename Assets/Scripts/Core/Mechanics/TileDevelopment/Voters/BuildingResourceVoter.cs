namespace TWF
{
    using System.Diagnostics.Contracts;

    public class BuildingResourceVoter : ITileDevelopmentVoter
    {
        private readonly IPathFinder<Vector> pathFinder;
        private readonly RandomResourceProvider randomResourceProvider;
        private IReadOnlyTypedComponents<BuildingResourceProduction[]> buidlingResourceProductions;
        private Path<Vector> path;

        public BuildingResourceVoter(IPathFinder<Vector> pathFinder)
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

        public double Vote(Vector pos, int buildingModel)
        {
            BuildingResourceProduction[] resourceProductions = this.buidlingResourceProductions[buildingModel];

            foreach (var resourceProduction in resourceProductions)
            {
                if (resourceProduction.IsConsumer())
                {
                    Vector? candidateProvider = this.randomResourceProvider.GetRandomProvider(resourceProduction.ResourceId);

                    if (!candidateProvider.HasValue)
                    {
                        return 0;
                    }

                    if (!this.pathFinder.FindPath(pos, candidateProvider.Value, resourceProduction.MaxDistance, ref this.path))
                    {
                        return 0;
                    }
                }
            }

            return 1;
        }
    }
}
