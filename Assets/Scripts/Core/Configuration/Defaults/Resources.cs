namespace TWF
{
    using System.Diagnostics.Contracts;

#pragma warning disable CA1724 // Type names should not match namespaces
    public static class Resources
#pragma warning restore CA1724 // Type names should not match namespaces
    {
        public static readonly string EntitiesName = "resource";
        public static readonly string Population = "population";
        public static readonly string Food = "food";

        public static void RegisterDefaults(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            NamedEntities resources = worldRules.Resources;
            resources.Register(Population);
            resources.Register(Food);
        }
    }
}
