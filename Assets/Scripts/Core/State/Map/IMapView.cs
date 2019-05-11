namespace TWF.State.Map
{
    /// <summary>
    /// A generic interface for a read-only view of a map containing elements of type T.
    /// </summary>
    public interface IMapView<T>
    {
        /// <summary>
        /// Returns the T object corresponding to the given position, or null if the map is empty there.
        /// </summary>
        /// <returns>The T object corresponding to the given position, or null if the map is empty there.</returns>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        T GetElement(int x, int y);

        /// <summary>
        /// Returns the T object corresponding to the given position, or null if the map is empty there.
        /// </summary>
        /// <returns>The T object corresponding to the given position, or null if the map is empty there.</returns>
        /// <param name="position">position.</param>
        T GetElement(Vector position);

        /// <summary>
        /// Returns the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        Vector GetSize();

        /// <summary>
        /// Returns the size of the map on the X axis.
        /// </summary>
        /// <return>The size of the map on the X axis.</return>
        int GetSizeX();

        /// <summary>
        /// Returns the size of the map on the Y axis.
        /// </summary>
        /// <return>The size of the map on the Y axis.</return>
        int GetSizeY();
    }
}
