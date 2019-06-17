namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This customizer attaches a graphic variant component to buildings.
    /// </summary>
    public static class BuildingVariants
    {
        public static readonly string Component = "variant";

        public static void DefaultBuildingVariantComponent(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            TypedComponents<int> typedComponents = new TypedComponents<int>(Component, worldRules.Random.Next);

            worldRules.BuildingComponents.Add(typedComponents.Name, typedComponents);
        }
    }
}
