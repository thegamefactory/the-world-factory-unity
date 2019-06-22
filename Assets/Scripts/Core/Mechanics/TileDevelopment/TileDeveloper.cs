namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// This agent is creating buildings on tiles.
    /// </summary>
    public class TileDeveloper : IAgent
    {
        private readonly RootTileDevelopmentVoter rootTileDevelopmentVoter;
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="TileDeveloper"/> class.
        /// </summary>
        /// <param name="random">Provides random value</param>
        public TileDeveloper(RootTileDevelopmentVoter rootTileDevelopmentVoter, Random random)
        {
            this.rootTileDevelopmentVoter = rootTileDevelopmentVoter;
            this.random = random;
        }

        public string Name => "Constructor";

        public Action<World> Execute(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            List<(Vector, int)> posBuildingModelToBuild = worldView.GetZoneMapView()
                .GetAllPositions()
                .Select(p => (p, this.rootTileDevelopmentVoter.Vote(p)))
                .Where(posBuildingModelTuple => posBuildingModelTuple.Item2 != BuildingModels.NoModel)
                .ToList();

            return (world) =>
            {
                IMap<int> buildingMap = world.GetBuildingMap();
                IMap<int> zoneMap = world.GetZoneMap();
                AnonymousEntities buildings = world.Buildings;
                TypedComponents<int> buildingVariant = buildings.GetMutableTypedComponents<int>(BuildingVariants.Component);
                TypedComponents<int> buildingModels = buildings.GetMutableTypedComponents<int>(BuildingModels.BuildingBuildingModelComponent);

                foreach (var posBuildingModelTuple in posBuildingModelToBuild
                    .Where((posBuildingModelTuple) => buildingMap[posBuildingModelTuple.Item1] == MapTypes.NoBuilding))
                {
                    int buildingId = buildings.Register();
                    buildingMap[posBuildingModelTuple.Item1] = buildingId;
                    buildingModels[buildingId] = posBuildingModelTuple.Item2;
                }
            };
        }
    }
}
