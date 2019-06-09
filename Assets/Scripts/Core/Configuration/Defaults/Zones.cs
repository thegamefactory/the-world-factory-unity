namespace TWF
{
    public static partial class Zones
    {
        public static readonly string ENTITIES = "zone";
        public static readonly string EMPTY = "empty";
        public static readonly string Residential = "residential";
        public static readonly string Farmland = "farmland";
        public static readonly string Road = "road";

        public static Entities DefaultZones()
        {
            Entities zones = new Entities(ENTITIES);
            zones.Register(EMPTY);
            zones.Register(Residential);
            zones.Register(Farmland);
            zones.Register(Road);
            return zones;
        }
    }
}
