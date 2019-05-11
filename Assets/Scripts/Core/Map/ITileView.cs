namespace TWF.Map
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
        /// The entity corresponding to the given tile. Can be null. Multiple tiles can share the same entity.
        /// </summary>
        IEntity Entity { get; }
    }
}
