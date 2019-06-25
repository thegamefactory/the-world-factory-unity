namespace TWF
{
    public interface IGenericMap
    {
        /// <summary>
        /// Gets the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        Vector Size { get; }

        /// <summary>
        /// Gets the size of the map on the X axis.
        /// </summary>
        /// <return>The size of the map on the X axis.</return>
        int SizeX { get; }

        /// <summary>
        /// Gets the size of the map on the Y axis.
        /// </summary>
        /// <return>The size of the map on the Y axis.</return>
        int SizeY { get; }
    }
}
