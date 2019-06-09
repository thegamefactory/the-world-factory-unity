using UnityEngine;

namespace TWF.Graphics
{
    internal delegate Color TerrainColorProvider(int zoneId);
    public class TerrainLayer : ITileLayer
    {
        public static string COMPONENT = "terrain_color";

        private IMapView<int> terrainMap;
        private TerrainColorProvider terrainColorProvider;

        public TerrainLayer(IWorldView worldView)
        {
            this.terrainMap = worldView.GetTerrainMapView();

            var terrainColor = worldView.Rules.Terrains.GetTypedComponents<Color>(COMPONENT);
            this.terrainColorProvider = (t) => terrainColor.GetComponent(t);
        }

        public string Name => COMPONENT;

        public Color? GetColor(Vector pos)
        {
            return terrainColorProvider(terrainMap[pos]);
        }
    }
}
