using TWF.State.Entity;

namespace TWF.State.Tile
{
    /// <summary>
    /// The smallest discrete element composing the map.
    /// </summary>
    public class Tile : ITileView
    {
        public TileZone Zone { get; set; }
        public TileTerrain Terrain { get; set; }
        public IEntity Entity { get; set; }

        public Tile(TileZone zone, TileTerrain terrain, IEntity entity)
        {
            Zone = zone;
            Terrain = terrain;
            Entity = entity;
        }

        public Tile(TileZone zone, TileTerrain terrain)
        {
            Zone = zone;
            Terrain = terrain;
            Entity = null;
        }
    }
}