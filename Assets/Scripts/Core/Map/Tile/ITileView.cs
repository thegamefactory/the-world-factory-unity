using TWF.Map.Building;

namespace TWF.Map.Tile
{
    /// <summary>
    /// A read-only view of a Tile.
    /// </summary>
    public interface ITileView
    {
        /// <summary>
        /// The type of zone of the tile. Will be Empty if not zoned.
        /// </summary>
        TileZone Zone { get; }

        /// <summary>
        /// The type of terrain of the tile.
        /// </summary>
        TileTerrain Terrain { get; }

        /// <summary>
        /// The building corresponding to the given tile. Can be null. Multiple tiles can share the same building.
        /// </summary>
        IBuilding Building { get; }
    }
}
