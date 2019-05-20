using TWF.Map.Building;

namespace TWF.Map.Tile
{
    /// <summary>
    /// The smallest discrete element composing the map.
    /// </summary>
    public class Tile : ITileView
    {
        public TileZone Zone { get; set; }
        public TileTerrain Terrain { get; set; }
        public IBuilding Building { get; set; }

        public Tile(TileZone zone, TileTerrain terrain, IBuilding building)
        {
            Zone = zone;
            Terrain = terrain;
            Building = building;
        }

        public Tile(TileZone zone, TileTerrain terrain)
        {
            Zone = zone;
            Terrain = terrain;
            Building = null;
        }
    }
}