namespace TWF
{
    using System.Diagnostics.Contracts;

    public static class Buildings
    {
        public static readonly string BuildingModelComponent = "building_model";
        public static readonly string ConnectionsComponent = "connections";

        /// <summary>
        /// The building model of each building, such a house, or a shop.
        /// Much of the building attributes are defined in the model.
        /// This is an implementation of the flywheel design pattern.
        /// </summary>
        public static void RegisterBuildingModelComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var buildingModelComponent = new TypedComponents<int>(BuildingModelComponent, () => BuildingModels.NoModel);

            worldRules.BuildingComponents.Add(buildingModelComponent.Name, buildingModelComponent);
        }

        /// <summary>
        /// The connections of each building.
        /// Building connections are used to satisfy the building model resource production constraints.
        /// For example, a building with a model "house" consuming "food" will need to be connected to a building with a model producing "food" such as a "farm".
        /// There can be multiple connections for the same resource, until the constraint is satisfied, considering that resource productions have associated quantities.
        /// </summary>
        public static void RegisterBuildingConnections(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var connectionsComponent = new TypedComponents<Vector[]>(ConnectionsComponent, () => System.Array.Empty<Vector>());

            worldRules.BuildingComponents.Add(connectionsComponent.Name, connectionsComponent);
        }
    }
}
