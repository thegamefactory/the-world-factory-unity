namespace TWF
{
    /// <summary>
    /// A concrete map implementation based on a bidimensional array.
    /// </summary>
    public class ArrayMap<T> : IMap<T>
    {
        T[,] contentMap;

        public ArrayMap(Vector size) : this(size.X, size.Y)
        {
        }

        public ArrayMap(int sizeX, int sizeY)
        {
            contentMap = new T[sizeX, sizeY];
        }

        public ArrayMap(T[,] contentMap)
        {
            this.contentMap = contentMap;
        }

        public ArrayMap(Vector size, T value) : this(size.X, size.Y, value)
        {
        }

        public ArrayMap(int sizeX, int sizeY, T value)
        {
            contentMap = new T[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    contentMap[x, y] = value;
                }
            }
        }

        public Vector Size { get => new Vector(contentMap.GetLength(0), contentMap.GetLength(1)); }

        public int SizeX { get => contentMap.GetLength(0); }

        public int SizeY { get => contentMap.GetLength(0); }

        T IMapView<T>.this[Vector position] => contentMap[position.X, position.Y];

        T IMapView<T>.this[int x, int y] => contentMap[x, y];

        public T this[Vector position] { get => contentMap[position.X, position.Y]; set => contentMap[position.X, position.Y] = value; }

        public T this[int x, int y] { get => contentMap[x, y]; set => contentMap[x, y] = value; }


        public T GetContent(int x, int y)
        {
            return contentMap[x, y];
        }

        public T GetContent(Vector position)
        {
            return contentMap[position.X, position.Y];
        }

        public void SetContent(T content, int x, int y)
        {
            contentMap[x, y] = content;
        }

        public void SetContent(T content, Vector position)
        {
            contentMap[position.X, position.Y] = content;
        }
    }
}
