namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// This customizer attaches a graphic variant component to buildings.
    /// </summary>
    public static class BuildingVariants
    {
        public static readonly string Component = "variant";

        public static void RegisterBuildingVariantComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            TypedComponents<int> typedComponents = new TypedComponents<int>(Component, worldRules.Random.Next);

            worldRules.BuildingComponents.Add(typedComponents.Name, typedComponents);
        }
    }
}
