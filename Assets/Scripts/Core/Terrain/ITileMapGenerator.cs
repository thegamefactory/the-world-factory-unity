using TWF.Map;

namespace TWF.Terrain
{
    /// <summary>
    /// An interface to generate maps of a given size.
    /// </summary>
    public interface ITileMapGenerator
    {
        IMap<Tile> Generate(Vector size);
    }
}
