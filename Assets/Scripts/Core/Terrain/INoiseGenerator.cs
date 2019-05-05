namespace TWF.Terrain
{
    /// <summary>
    /// An interface to generate random noise on a given float bidimensional array.
    /// The generated noise should be comprised between 0 and 1 (some values might slightly exceed 1).
    /// </summary>
    public interface INoiseGenerator
    {
        void Generate(float[,] noiseMap);
    }
}
