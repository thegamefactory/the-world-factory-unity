namespace TWF
{
    /// <summary>
    /// A generic interface for a read-write map containing elements of type T.
    /// </summary>
    public interface IMap<T> : IMapView<T>
    {
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
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        new T this[int x, int y]
        {
            get; set;
        }

        /// <summary>
        /// Sets the content of the map at the given position. Overrides content if not empty.
        /// </summary>
        /// <param name="content">The content to set.</param>
        /// <param name="position">Position.</param>
        void SetContent(T content, Vector position);

        /// <summary>
        /// Sets the content of the map at the given position. Overrides content if not empty.
        /// </summary>
        /// <param name="position">Position.</param>
        new T this[Vector position]
        {
            get; set;
        }
    }
}
