namespace TWF
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// World buildings are defined in a registry of Entities.
    /// The building map type allows to perform geographical lookups of location to building id.
    /// Each building is related to a particular model, this is a Component of the building Entities.
    /// Other Components can be attached to buildings, such as a grahpical variant which corresponds to a different graphical rendering.
    /// The effect of the building on the gameplay is however completely defined in the building model.
    /// Building model itself is another registry Entities to which components can be attached to, to provide specific behavior.
    ///
    /// Let's provide an example which ties everything together.
    ///
    /// The map at location (x, y) will reference the building B, which is a number between 0 and "number of buildings in the current world".
    /// B has a model BM, a number corresponding to the building model "house".
    /// B has also a graphical component BG, a number corresponding to the variant "yellow house with red roof"
    /// BM itself has many components that define the effect of a house on the gameplay, such as its population, its elecriticy consomption, etc.
    /// </summary>
    public static class BuildingModels
    {
        public static readonly string EntitiesName = "building_models";
        public static readonly string Farm = "farm";
        public static readonly string House = "house";

        public static readonly string Component = "building_models";
        public static readonly int NoModel = -1;

        public static void RegisterDefaults(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            NamedEntities buildingModels = worldRules.BuildingModels;
            buildingModels.Register(Farm);
            buildingModels.Register(House);
        }

        public static void RegisterBuilidingModelComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            TypedComponents<int> typedComponents = new TypedComponents<int>(Component, () => NoModel);

            worldRules.BuildingComponents.Add(typedComponents.Name, typedComponents);
        }
    }
}
