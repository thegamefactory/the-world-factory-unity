using TWF.Map;

namespace TWF.Building
{
    public interface IBuilding : IEntity
    {
        Tile.TileZone Zone { get; }
        int Seed { get; }
    }
}
