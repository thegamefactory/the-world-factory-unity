namespace TWF
{
    /// <summary>
    /// An interface to generate maps of a given size.
    /// </summary>
    public interface ITerrainGenerator
    {
        IMap<Terrain> Generate(Vector size);
    }
}
