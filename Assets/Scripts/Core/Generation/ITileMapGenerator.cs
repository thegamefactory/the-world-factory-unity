using TWF.Map;
using TWF.Map.Tile;

namespace TWF.Generation
{
    /// <summary>
    /// An interface to generate maps of a given size.
    /// </summary>
    public interface ITileMapGenerator
    {
        IMap<Tile> Generate(Vector size);
    }
}
