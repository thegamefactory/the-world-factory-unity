namespace TWF.State.Map
{
    /// <summary>
    /// A concrete map implementation based on a bidimensional array.
    /// </summary>
    public class ArrayMap<T> : IMap<T>
    {
        T[,] contentMap;

        public ArrayMap(Vector size)
        {
            contentMap = new T[size.X, size.Y];
        }

        public ArrayMap(int sizeX, int sizeY)
        {
            contentMap = new T[sizeX, sizeY];
        }

        public ArrayMap(T[,] contentMap)
        {
            this.contentMap = contentMap;
        }

        /// <inheritdoc/>
        public T GetElement(int x, int y)
        {
            return contentMap[x, y];
        }

        /// <inheritdoc/>
        public T GetElement(Vector position)
        {
            return contentMap[position.X, position.Y];
        }

        /// <inheritdoc/>
        public Vector Size { get => new Vector(contentMap.GetLength(0), contentMap.GetLength(1)); }

        /// <inheritdoc/>
        public int SizeX { get => contentMap.GetLength(0); }

        /// <inheritdoc/>
        public int SizeY { get => contentMap.GetLength(0); }

        /// <inheritdoc/>
        public void SetElement(T content, int x, int y)
        {
            contentMap[x, y] = content;
        }

        /// <inheritdoc/>
        public void SetElement(T content, Vector position)
        {
            contentMap[position.X, position.Y] = content;
        }
    }
}
