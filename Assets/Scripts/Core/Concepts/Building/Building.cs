namespace TWF
{
    /// <summary>
    /// Instance of a building on the map.
    /// </summary>
    public class Building
    {
        public int Variant { get; }

        public Building(int variant)
        {
            Variant = variant;
        }
    }
}
