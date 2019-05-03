using UnityEngine;

namespace TWF.Terrain
{
    /// <summary>
    /// An implementation of the noise generator using Perlin noise.
    /// </summary>
    public class PerlinNoiseGenerator : INoiseGenerator
    {
        private float noisePeriod;
        private float offsetX;
        private float offsetY;

        /// <param name="noisePeriod">
        /// The noise period. 
        /// The smaller the number, the higher the noise frequency.
        /// A period of 1 means that the noise repeats itself at the end of the map.
        /// </param>
        /// <param name="offsetX">
        /// The noise offset along the x axis.
        /// Used mainly to avoid introduce a parameter that enables to avoid repetition between different runs.
        /// Should be between 0 and 1, effects will repeat with a period of 1.
        /// </param>
        /// <param name="offsetY">
        /// The noise offset along the Y axis.
        /// Used mainly to avoid introduce a parameter that enables to avoid repetition between different runs.
        /// Should be between 0 and 1, effects will repeat with a period of 1.
        /// </param>
        public PerlinNoiseGenerator(float noisePeriod, float offsetX, float offsetY)
        {
            this.noisePeriod = noisePeriod;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public void Generate(float[,] noiseMap)
        {
            int width = noiseMap.GetLength(0);
            int height = noiseMap.GetLength(1);

            for (int x = 0; x < noiseMap.GetLength(0); ++x)
            {
                for (int y = 0; y < noiseMap.GetLength(1); ++y)
                {
                    float sampleX = (float)(x) / width * noisePeriod + offsetX;
                    float sampleY = (float)(y) / height * noisePeriod + offsetY;
                    noiseMap[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
                }
            }
        }
    }
}
