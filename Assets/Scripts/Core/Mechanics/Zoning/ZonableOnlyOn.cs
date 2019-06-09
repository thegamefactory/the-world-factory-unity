namespace TWF
{
    public class ZonableOnlyOn : IZonableTerrain
    {
        private readonly int zonableTerrainId;

        public ZonableOnlyOn(int zonableTerrainId)
        {
            this.zonableTerrainId = zonableTerrainId;
        }

        public bool IsZonable(int terrainId)
        {
            return terrainId == this.zonableTerrainId;
        }
    }
}
