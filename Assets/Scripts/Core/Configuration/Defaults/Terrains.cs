namespace TWF
{
    using System.Diagnostics.Contracts;

    public static class Terrains
    {
        public static readonly string EntitiesName = "terrain";
        public static readonly string Land = "land";
        public static readonly string Water = "water";

        public static void RegisterDefaults(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            NamedEntities terrains = worldRules.Terrains;
            terrains.Register(Land);
            terrains.Register(Water);
        }
    }
}
