namespace TWF
{
    public static class Terrains
    {
        public static string REGISTRY = "terrain";
        public static string LAND = "land";
        public static string WATER = "water";

        public static Registry DefaultTerrains()
        {
            Registry terrains = new Registry(REGISTRY);
            terrains.Register(LAND);
            terrains.Register(WATER);
            return terrains;
        }
    }
}
