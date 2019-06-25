namespace TWF
{
    /// <summary>
    /// A generic interface for a read-only view of a map containing elements of type T.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the map.</typeparam>
    public interface IMapView<T> : IGenericMap
    {
#pragma warning disable CA1043 // Use Integral Or String Argument For Indexers
        /// <summary>
        /// Returns the T object corresponding to the given position, or null if the map is empty there.
        /// </summary>
        /// <returns>The T object corresponding to the given position, or null if the map is empty there.</returns>
        /// <param name="position">position.</param>
        T this[Vector position]
        {
            get;
        }
#pragma warning restore CA1043 // Use Integral Or String Argument For Indexers

        /// <summary>
        /// Registers a listener that will be invoked every time the map content changes.
        /// </summary>
        /// <param name="updateListener">The listener to register.</param>
        void RegisterListener(MapUpdateListener<T> updateListener);
    }
}
