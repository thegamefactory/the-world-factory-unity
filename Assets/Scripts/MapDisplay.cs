using TWF;
using UnityEngine;
using UnityEngine.Assertions;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;

    private Color[] cachedColorMap;
    private readonly Texture2D texture2D;

    public void Update()
    {
        Draw(Root.WorldView, GetComponent<TileMapper>());
    }

    public void Draw(IWorldView worldView, TileMapper tileMapper)
    {
        Vector size = worldView.Size;
        int width = size.X;
        int height = size.Y;
        Assert.IsTrue(width > 0);
        Assert.IsTrue(height > 0);

        Color[] colorMap = GetColorMap(width, height);

        IMapView<Building> buildingMap = worldView.GetBuildingMapView();
        IMapView<int> zoneMap = worldView.GetZoneMapView();
        IMapView<int> terrainMap = worldView.GetTerrainMapView();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                colorMap[y * width + x] = tileMapper.GetTileColor(buildingMap, terrainMap, zoneMap, x, y);
            }
        }

        Texture2D texture = GetTexture(width, height);
        texture.SetPixels(colorMap);
        texture.Apply();
        textureRender.sharedMaterial.mainTexture = texture;
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
            texture = new Texture2D(width, height)
            {
                filterMode = FilterMode.Point
            };
            textureRender.transform.localScale = new Vector3(width, height, 1);
        }
        return texture;
    }
}
