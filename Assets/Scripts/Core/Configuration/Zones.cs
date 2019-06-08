namespace TWF
{
    /// <summary>
    /// A definition of all zones.
    /// </summary>
    public static partial class Zones
    {
        public static string ENTITIES_NAME = "zone";
        public static string EMPTY = "empty";
        public static string RESIDENTIAL = "residential";
        public static string FARMLAND = "farmland";
        public static string ROAD = "road";

        public static Entities DefaultZones()
        {
            Entities zones = new Entities(ENTITIES_NAME);
            zones.Register(EMPTY);
            zones.Register(RESIDENTIAL);
            zones.Register(FARMLAND);
            zones.Register(ROAD);
            return zones;
        }
    }
}
