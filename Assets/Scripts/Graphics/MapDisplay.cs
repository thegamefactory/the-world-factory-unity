namespace TWF.Graphics
{
    using System.Diagnostics.Contracts;
    using TWF;
    using UnityEngine;
    using UnityEngine.Assertions;

    public class MapDisplay : MonoBehaviour
    {
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
        public Renderer TextureRender;
        public string[] ActiveTileLayers;
        public Color EmptyColor;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

        private ITileLayer[] cachedTileLayers = System.Array.Empty<ITileLayer>();
        private Color[] cachedColorMap;

        public void Update()
        {
            this.Draw(Root.WorldView);
        }

        public void Draw(IWorldView worldView)
        {
            Contract.Requires(worldView != null);

            Vector size = worldView.Size;
            int width = size.X;
            int height = size.Y;
            Assert.IsTrue(width > 0);
            Assert.IsTrue(height > 0);

            Color[] colorMap = this.GetColorMap(width, height);

            Vector v;
            int i = 0;
            ITileLayer[] tileLayers = this.GetTileLayers();

            for (v.Y = 0; v.Y < height; v.Y++)
            {
                for (v.X = 0; v.X < width; v.X++)
                {
                    Color color = this.EmptyColor;
                    color.a = 0.0f;

                    foreach (ITileLayer tileLayer in tileLayers)
                    {
                        Color? newColor = tileLayer.GetColor(v);
                        if (newColor.HasValue)
                        {
                            ColorUtils.Superpose(ref color, color, newColor.Value);
                            if (color.a == 1.0f)
                            {
                                break;
                            }
                        }
                    }

                    colorMap[i++] = color;
                }
            }

            Texture2D texture = this.GetTexture(width, height);
            texture.SetPixels(colorMap);
            texture.Apply();
            this.TextureRender.sharedMaterial.mainTexture = texture;
        }

        private ITileLayer[] GetTileLayers()
        {
            bool invalidCache = false;
            if (this.cachedTileLayers.Length == this.ActiveTileLayers.Length)
            {
                for (int i = 0; i < this.ActiveTileLayers.Length; ++i)
                {
                    if (this.cachedTileLayers[i].Name != this.ActiveTileLayers[i])
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
                ITileLayer[] tileLayers = new ITileLayer[this.ActiveTileLayers.Length];
                for (int i = 0; i < this.ActiveTileLayers.Length; ++i)
                {
                    tileLayers[i] = Root.GameService.GraphicConfig.TileLayers[this.ActiveTileLayers[i]];
                }

                this.cachedTileLayers = tileLayers;
            }

            return this.cachedTileLayers;
        }

        private Color[] GetColorMap(int width, int height)
        {
            if (this.cachedColorMap == null || this.cachedColorMap.Length != width * height)
            {
                this.cachedColorMap = new Color[width * height];
            }

            return this.cachedColorMap;
        }

        private Texture2D GetTexture(int width, int height)
        {
            Texture2D texture = this.TextureRender.sharedMaterial.mainTexture as Texture2D;
            if (texture == null || texture.width != width || texture.height != height)
            {
                texture = new Texture2D(width, height)
                {
                    filterMode = FilterMode.Point,
                };
                this.TextureRender.transform.localScale = new Vector3(width, height, 1);
            }

            return texture;
        }
    }
}