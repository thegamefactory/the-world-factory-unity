using UnityEngine;

public class HeightMapDisplay : MonoBehaviour
{
    public Renderer textureRender;

    public void Draw(float[,] altitudeMap)
    {
        int width = altitudeMap.GetLength(0);
        int height = altitudeMap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // colorMap[y * width + x] = Color.blue;
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, altitudeMap[x, y]);
            }
        }

        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(colorMap);
        texture.Apply();

        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(width, height, 1);
    }
}
