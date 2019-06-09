namespace TWF
{
    public class AlwaysZonable : IZonableTerrain
    {
        public bool IsZonable(int terrainId)
        {
            return true;
        }
    }
}
