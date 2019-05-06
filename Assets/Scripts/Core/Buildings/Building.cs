using TWF.Map;

namespace TWF.Building
{
    public class Building : IBuilding
    {
        public Tile.TileZone Zone { get; }
        public int Seed { get; }

        public Building(Tile.TileZone zone, int seed)
        {
            Zone = zone;
            Seed = seed;
        }
    }
}
