using TWF;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;

    private Color[] cachedColorMap;
    private Texture2D texture2D;

    public void Draw(int width, int height)
    {
        IGameState gameState = Root.GameState;
        TileMapper tileMapper = GetComponent<TileMapper>();
        Color[] colorMap = GetColorMap(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                colorMap[y * width + x] = tileMapper.GetTileColor(gameState.GetEntity(x, y), gameState.GetTile(x, y));
            }
        }

        Texture2D texture = GetTexture(width, height);
        texture.SetPixels(colorMap);
        texture.Apply();
    }

    private Color[] GetColorMap(int width, int height)
    {
        if (null == cachedColorMap || cachedColorMap.Length != width * height)
        {
            cachedColorMap = new Color[width * height];
        }
        return cachedColorMap;
    }

    private Texture2D GetTexture(int width, int height)
    {
        Texture2D texture = textureRender.sharedMaterial.mainTexture as Texture2D;
        if (null == texture || texture.width != width || texture.height != height)
        {
            texture = new Texture2D(width, height);
            texture.filterMode = FilterMode.Point;
            textureRender.transform.localScale = new Vector3(width, height, 1);
        }
        return texture;
    }
}
