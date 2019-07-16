namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// This agent is constructing buildings.
    /// For each tile, it queries the buildingConstructorVoter for which building should be constructed on the tile.
    /// When the voter reply is different than no model, the corresponding building is created.
    /// </summary>
    public class BuildingConstructor : IAgent
    {
        private readonly BuildingConstructorVoter buildingConstructorVoter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingConstructor"/> class.
        /// </summary>
        public BuildingConstructor(BuildingConstructorVoter rootTileDevelopmentVoter)
        {
            this.buildingConstructorVoter = rootTileDevelopmentVoter;
        }

        public string Name => "Constructor";

        public Action<World> Execute(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            List<(Vector, int)> posBuildingModelToBuild = worldView.GetZoneMapView()
                .GetAllPositions()
                .Select(p => (p, this.buildingConstructorVoter.Vote(p)))
                .Where(posBuildingModelTuple => posBuildingModelTuple.Item2 != BuildingModels.NoModel)
                .ToList();

            return (world) =>
            {
                IMap<int> buildingMap = world.GetBuildingMap();
                IMap<int> zoneMap = world.GetZoneMap();
                AnonymousEntities buildings = world.Buildings;
                TypedComponents<int> buildingVariant = buildings.GetMutableTypedComponents<int>(BuildingVariants.Component);
                TypedComponents<int> buildingModels = buildings.GetMutableTypedComponents<int>(Buildings.BuildingModelComponent);

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
