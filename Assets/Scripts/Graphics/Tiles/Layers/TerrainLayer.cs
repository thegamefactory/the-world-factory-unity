using UnityEngine;

namespace TWF.Graphics
{
    internal delegate Color TerrainColorProvider(int zoneId);
    public class TerrainLayer : ITileLayer
    {
        public static string COMPONENT = "terrainColor";

        private IMapView<int> terrainMap;
        private TerrainColorProvider terrainColorProvider;

        public void OnNewWorld(IWorldView worldView)
        {
            this.terrainMap = worldView.GetTerrainMapView();

            var terrainColor = worldView.Terrains.GetTypedComponents<Color>(COMPONENT);
            this.terrainColorProvider = (t) => terrainColor.GetComponent(t);
        }

        public string Name => COMPONENT;

        public Color? GetColor(Vector pos)
        {
            return terrainColorProvider(terrainMap[pos]);
        }
    }
}
