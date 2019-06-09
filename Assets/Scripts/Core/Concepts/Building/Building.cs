namespace TWF
{
    /// <summary>
    /// Instance of a building on the map.
    /// </summary>
    public class Building
    {
        public Building(int variant)
        {
            this.Variant = variant;
        }

        public int Variant { get; }
    }
}
