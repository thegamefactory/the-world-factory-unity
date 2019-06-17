namespace TWF
{
    public static partial class Zones
    {
        public static readonly string Entities = "zone";
        public static readonly string Empty = "empty";
        public static readonly string Residential = "residential";
        public static readonly string Farmland = "farmland";
        public static readonly string Road = "road";

        public static NamedEntities DefaultZones()
        {
            NamedEntities zones = new NamedEntities(Entities);
            zones.Register(Empty);
            zones.Register(Residential);
            zones.Register(Farmland);
            zones.Register(Road);
            return zones;
        }
    }
}
