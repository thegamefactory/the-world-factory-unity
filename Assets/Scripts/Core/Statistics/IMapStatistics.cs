using TWF.Map;

namespace TWF.Statistics
{
    public interface ITileMapStatistics
    {
        int GetCount(TileZone zone);
        int GetBuildingCount(TileZone zone);
        int GetUnoccupiedCount(TileZone zone);
    }
}
