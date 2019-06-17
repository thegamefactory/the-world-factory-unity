namespace TWF
{
    public interface IWorldView
    {
        /// <summary>
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

        /// <summary>
        /// Gets the world rules.
        /// </summary>
        /// <return>The world rules.</return>
        IWorldRules Rules { get; }

        /// <summary>
        /// Gets the world buildings.
        /// </summary>
        /// <return>The world buildings.</return>
        IReadOnlyEntities Buildings { get; }

        /// <summary>
        /// Gets the map view corresponding to the given type.
        /// </summary>
        /// <return>The map view corresponding to the given type.</return>
        /// <param name="mapType">The identifier of the map.</param>
        /// <typeparam name="T">The type of the returned map view.</param>
        IMapView<T> GetMapView<T>(string mapType);
    }
}
