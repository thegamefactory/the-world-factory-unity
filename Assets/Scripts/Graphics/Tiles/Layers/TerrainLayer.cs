namespace TWF.Graphics
{
    using UnityEngine;

    internal delegate Color TerrainColorProvider(int zoneId);

    public class TerrainLayer : ITileLayer
    {
        public static readonly string Component = "terrain_color";

        private readonly IMapView<int> terrainMap;
        private readonly TerrainColorProvider terrainColorProvider;

        public TerrainLayer(IWorldView worldView)
        {
            this.terrainMap = worldView.GetTerrainMapView();

            var terrainColor = worldView.Rules.Terrains.GetTypedComponents<Color>(Component);
            this.terrainColorProvider = (t) => terrainColor.GetComponent(t);
        }

        public string Name => Component;

        public Color? GetColor(Vector pos)
        {
            return this.terrainColorProvider(this.terrainMap[pos]);
        }
    }
}
