using TWF;
using UnityEngine;
using UnityEngine.Assertions;

namespace TWF.Graphics
{
    public class MapDisplay : MonoBehaviour
    {
        public Renderer TextureRender;
        public string[] ActiveTileLayers;
        public Color EmptyColor;

        private ITileLayer[] cachedTileLayers = new ITileLayer[0];
        private Color[] cachedColorMap;
        private readonly Texture2D texture2D;

        public void Update()
        {
            Draw(Root.WorldView);
        }

        public void Draw(IWorldView worldView)
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

            Vector v;
            int i = 0;
            ITileLayer[] tileLayers = GetTileLayers();

            for (v.Y = 0; v.Y < height; v.Y++)
            {
                for (v.X = 0; v.X < width; v.X++)
                {
                    Color color = EmptyColor;
                    color.a = 0.0f;

                    foreach (ITileLayer tileLayer in tileLayers)
                    {
                        Color? newColor = tileLayer.GetColor(v);
                        if (newColor.HasValue)
                        {
                            ColorUtils.Superpose(ref color, color, newColor.Value);
                            if (1.0f == color.a)
                            {
                                break;
                            }
                        }
                    }
                    colorMap[i++] = color;
                }
            }

            Texture2D texture = GetTexture(width, height);
            texture.SetPixels(colorMap);
            texture.Apply();
            TextureRender.sharedMaterial.mainTexture = texture;
        }

        private ITileLayer[] GetTileLayers()
        {
            bool invalidCache = false;
            if (cachedTileLayers.Length == ActiveTileLayers.Length)
            {
                for (int i = 0; i < ActiveTileLayers.Length; ++i)
                {
                    if (cachedTileLayers[i].Name != ActiveTileLayers[i])
                    {
                        invalidCache = true;
                    }
                }
            }
            else
            {
                invalidCache = true;
            }

            if (invalidCache)
            {
                ITileLayer[] tileLayers = new ITileLayer[ActiveTileLayers.Length];
                for (int i = 0; i < ActiveTileLayers.Length; ++i)
                {
                    tileLayers[i] = Root.GameService.GraphicConfig.TileLayers[ActiveTileLayers[i]];
                }
                cachedTileLayers = tileLayers;
            }
            return cachedTileLayers;
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
            Texture2D texture = TextureRender.sharedMaterial.mainTexture as Texture2D;
            if (null == texture || texture.width != width || texture.height != height)
            {
                texture = new Texture2D(width, height)
                {
                    filterMode = FilterMode.Point
                };
                TextureRender.transform.localScale = new Vector3(width, height, 1);
            }
            return texture;
        }
    }
}