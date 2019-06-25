namespace TWF
{
    /// <summary>
    /// A generic interface for a read-write map containing elements of type T.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the map.</typeparam>
    public interface IMap<T> : IMapView<T>
    {
#pragma warning disable CA1043 // Use Integral Or String Argument For Indexers
        /// <summary>
        /// Sets the content of the map at the given position. Overrides content if not empty.
        /// </summary>
        /// <param name="position">Position.</param>
        new T this[Vector position]
        {
            get; set;
        }
#pragma warning restore CA1043 // Use Integral Or String Argument For Indexers
    }
}
