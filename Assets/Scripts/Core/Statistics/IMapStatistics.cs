using TWF.State.Tile;

namespace TWF.Statistics
{
    public interface IMapStatistics
    {
        int GetCount(TileZone zone);
        int GetBuildingCount(TileZone zone);
        int GetUnoccupiedCount(TileZone zone);
    }
}
