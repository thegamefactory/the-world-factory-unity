namespace TWF
{
    /// <summary>
    /// An interface to generate maps of a given size.
    /// </summary>
    public interface ITerrainGenerator
    {
        IMap<int> GenerateTerrainMap(IWorldConfig worldConfig, Vector size);
    }
}
