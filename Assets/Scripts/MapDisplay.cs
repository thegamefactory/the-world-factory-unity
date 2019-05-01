using TWF;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;

    void Update()
    {

    }

    public void Draw(int width, int height)
    {
        GameService gameService = Root.GetInstance<GameService>();
        TileMapper tileMapper = FindObjectOfType<TileMapper>();
        Color[] colorMap = new Color[width * height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                colorMap[y * width + x] = tileMapper.GetTileColor(gameService.GetTile(x, y));
            }
        }

        Texture2D texture = textureRender.sharedMaterial.mainTexture as Texture2D;
        if (null == texture || texture.width != width || texture.height != height)
        {
            texture = new Texture2D(width, height);
        }

        texture.filterMode = FilterMode.Point;
        texture.SetPixels(colorMap);
        texture.Apply();

        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(width, height, 1);
    }
}
