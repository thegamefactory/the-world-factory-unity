using UnityEngine;

namespace TWF.Terrain
{
    public class NoiseGenerator : INoiseGenerator
    {
        private float scale;

        public NoiseGenerator(float scale)
        {
            this.scale = scale;
        }

        public void Generate(float[,] noiseMap)
        {
            int width = noiseMap.GetLength(0);
            int height = noiseMap.GetLength(1);
            for (int x = 0; x < noiseMap.GetLength(0); ++x)
            {
                for (int y = 0; y < noiseMap.GetLength(1); ++y)
                {
                    float sampleX = (float)(x) / width * scale;
                    float sampleY = (float)(y) / height * scale;
                    noiseMap[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
                }
            }
        }
    }
}
