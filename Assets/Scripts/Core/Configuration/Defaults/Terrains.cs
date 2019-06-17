namespace TWF
{
    public static class Terrains
    {
        public static readonly string EntitiesName = "terrain";
        public static readonly string Land = "land";
        public static readonly string Water = "water";

        public static NamedEntities DefaultTerrains()
        {
            NamedEntities terrains = new NamedEntities(EntitiesName);
            terrains.Register(Land);
            terrains.Register(Water);
            return terrains;
        }
    }
}
