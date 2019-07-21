namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    public static class Buildings
    {
        public static readonly string BuildingModelComponent = "building_model";
        public static readonly string LocationComponent = "location";
        public static readonly string TransportLinkComponent = "transport_link";
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
        /// The location of each building.
        /// </summary>
        public static void RegisterBuildingLocation(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var connectionsComponent = new TypedComponents<Vector>(LocationComponent, () => new Vector(-1, -1));

            worldRules.BuildingComponents.Add(connectionsComponent.Name, connectionsComponent);
        }

        /// <summary>
        /// The transport link of each building.
        /// The transport link is the position on the map where all path finding related to the building starts and ends.
        /// </summary>
        public static void RegisterBuildingTransportLink(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var connectionsComponent = new TypedComponents<Vector?>(TransportLinkComponent, () => null);

            worldRules.BuildingComponents.Add(connectionsComponent.Name, connectionsComponent);
        }

        /// <summary>
        /// The connections of each building.
        /// The connection refers to a building id.
        /// Connections are bi-directional, in other words, if a building A has a connection to a building B, then building B must have a connection to building A.
        /// Building connections are used to satisfy the building supply chain.
        /// For example, a building with a model "house" consuming "food" will need to be connected to a building with a model producing "food" such as a "farm".
        /// There can be multiple connections for the same resource, until the constraint is satisfied, considering that resource productions have associated quantities.
        /// </summary>
        public static void RegisterBuildingConnections(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var connectionsComponent = new TypedComponents<ISet<int>>(ConnectionsComponent, () => new HashSet<int>());

            worldRules.BuildingComponents.Add(connectionsComponent.Name, connectionsComponent);
        }
    }
}
