namespace TWF.Map.Tile
{
    /// <summary>
    /// The zone of the tile, can be defined by the player and used by agents to evaluate against rule and define which buildings to create.
    /// </summary>
    public enum TileZone
    {
        EMPTY,
        FARMLAND,
        RESIDENTIAL,
        ROAD
    }
}
