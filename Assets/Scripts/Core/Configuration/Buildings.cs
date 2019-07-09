namespace TWF
{
    using System.Diagnostics.Contracts;

    public static class Buildings
    {
        public static readonly string BuildingModelComponent = "building_model";

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
    }
}
