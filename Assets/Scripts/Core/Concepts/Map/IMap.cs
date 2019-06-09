namespace TWF
{
    /// <summary>
    /// A generic interface for a read-write map containing elements of type T.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the map.</typeparam>
    public interface IMap<T> : IMapView<T>
    {
        /// <summary>
        /// Sets the content of the map at the given position. Overrides content if not empty.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        new T this[int x, int y]
        {
            get; set;
        }

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

        /// <summary>
        /// Sets the content of the map at the given position. Overrides content if not empty.
        /// </summary>
        /// <param name="content">The content to set.</param>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        void SetContent(T content, int x, int y);

        /// <summary>
        /// Sets the content of the map at the given position. Overrides content if not empty.
        /// </summary>
        /// <param name="content">The content to set.</param>
        /// <param name="position">Position.</param>
        void SetContent(T content, Vector position);
    }
}
