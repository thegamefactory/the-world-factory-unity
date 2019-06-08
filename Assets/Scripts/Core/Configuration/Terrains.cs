namespace TWF
{
    public static class Terrains
    {
        public static string ENTITIES_NAME = "terrain";
        public static string LAND = "land";
        public static string WATER = "water";

        public static Entities DefaultTerrains()
        {
            Entities terrains = new Entities(ENTITIES_NAME);
            terrains.Register(LAND);
            terrains.Register(WATER);
            return terrains;
        }
    }
}
