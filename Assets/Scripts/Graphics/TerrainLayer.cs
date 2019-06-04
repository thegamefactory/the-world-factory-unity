using UnityEngine;

namespace TWF.Graphics
{
    internal delegate Color TerrainColorProvider(int zoneId);
    class TerrainLayer : ITileLayer
    {
        private readonly IMapView<int> terrainMap;
        private readonly TerrainColorProvider terrainColorProvider;
        public TerrainLayer(IMapView<int> terrainMap, TerrainColorProvider terrainColorProvider)
        {
            this.terrainMap = terrainMap;
            this.terrainColorProvider = terrainColorProvider;
        }
        public Color? GetColor(Vector pos)
        {
            return terrainColorProvider(terrainMap[pos]);
        }
    }
}
