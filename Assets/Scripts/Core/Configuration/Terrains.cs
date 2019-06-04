namespace TWF
{
    public static class Terrains
    {
        public static string LAND = "land";
        public static string WATER = "water";

        public static Registry DefaultTerrains()
        {
            Registry terrains = new Registry();
            terrains.Register(LAND);
            terrains.Register(WATER);
            return terrains;
        }
    }
}
