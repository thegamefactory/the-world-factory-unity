namespace TWF
{
    using System.Diagnostics.Contracts;

    public static class Buildings
    {
        public static readonly string BuildingModelComponent = "building_model";

        public static void RegisterBuildingModelComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var buildingModelComponent = new TypedComponents<int>(BuildingModelComponent, () => BuildingModels.NoModel);

            worldRules.BuildingComponents.Add(buildingModelComponent.Name, buildingModelComponent);
        }
    }
}
