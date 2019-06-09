namespace TWF
{
    /// <summary>
    /// A generic interface for a read-only view of a map containing elements of type T.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the map.</typeparam>
    public interface IMapView<out T> : IGenericMap
    {
        /// <summary>
        /// Returns the T object corresponding to the given position, or null if the map is empty there.
        /// </summary>
        /// <returns>The T object corresponding to the given position, or null if the map is empty there.</returns>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        T this[int x, int y]
        {
            get;
        }

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
        /// Returns the T object corresponding to the given position, or null if the map is empty there.
        /// </summary>
        /// <returns>The T object corresponding to the given position, or null if the map is empty there.</returns>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        T GetContent(int x, int y);

        /// <summary>
        /// Returns the T object corresponding to the given position, or null if the map is empty there.
        /// </summary>
        /// <returns>The T object corresponding to the given position, or null if the map is empty there.</returns>
        /// <param name="position">position.</param>
        T GetContent(Vector position);
    }
}
