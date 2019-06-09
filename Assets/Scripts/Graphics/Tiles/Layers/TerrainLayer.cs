namespace TWF.Graphics
{
    using UnityEngine;

    internal delegate Color TerrainColorProvider(int zoneId);

    public class TerrainLayer : ITileLayer
    {
        public static string COMPONENT = "terrain_color";

        private readonly IMapView<int> terrainMap;
        private readonly TerrainColorProvider terrainColorProvider;

        public TerrainLayer(IWorldView worldView)
        {
            this.terrainMap = worldView.GetTerrainMapView();

            var terrainColor = worldView.Rules.Terrains.GetTypedComponents<Color>(COMPONENT);
            this.terrainColorProvider = (t) => terrainColor.GetComponent(t);
        }

        public string Name => COMPONENT;

        public Color? GetColor(Vector pos)
        {
            return this.terrainColorProvider(this.terrainMap[pos]);
        }
    }
}
