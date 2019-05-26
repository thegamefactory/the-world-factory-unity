namespace TWF
{
    public interface IWorldView : IWorldConfig
    {
        /// <summary>
        /// <summary>
        /// Return the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        Vector Size { get; }

        /// <summary>
        /// Returns the size of the map on the X axis.
        /// </summary>
        /// <return>The size of the map on the X axis.</return>
        int SizeX { get; }

        /// <summary>
        /// Returns the size of the map on the Y axis.
        /// </summary>
        /// <return>The size of the map on the Y axis.</return>
        int SizeY { get; }

        /// <summary>
        /// Get the map view corresponding to the given type.
        /// </summary>
        /// <return>The map view corresponding to the given type.</return>
        /// <param name="mapType">The type of the map.</param>
        IMapView<T> GetMapView<T>(MapType mapType);
    }
}
