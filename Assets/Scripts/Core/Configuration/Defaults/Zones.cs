namespace TWF
{
    using System.Diagnostics.Contracts;

    public static class Zones
    {
        public static readonly string EntitiesName = "zone";
        public static readonly string Empty = "empty";

        public static readonly string Commercial = "commercial";
        public static readonly string Farmland = "farmland";
        public static readonly string Residential = "residential";
        public static readonly string Road = "road";

        public static void RegisterDefaults(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            NamedEntities zones = worldRules.Zones;
            zones.Register(Empty);
            zones.Register(Commercial);
            zones.Register(Farmland);
            zones.Register(Residential);
            zones.Register(Road);
        }
    }
}
