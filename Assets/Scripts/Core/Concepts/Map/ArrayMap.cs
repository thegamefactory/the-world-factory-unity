namespace TWF
{
    /// <summary>
    /// A concrete map implementation based on a bidimensional array.
    /// </summary>
    public class ArrayMap<T> : IMap<T>
    {
        private readonly T[,] contentMap;

        public ArrayMap(Vector size)
            : this(size.X, size.Y)
        {
        }

        public ArrayMap(int sizeX, int sizeY)
        {
            this.contentMap = new T[sizeX, sizeY];
        }

        public ArrayMap(T[,] contentMap)
        {
            this.contentMap = contentMap;
        }

        public ArrayMap(Vector size, T value)
            : this(size.X, size.Y, value)
        {
        }

        public ArrayMap(int sizeX, int sizeY, T value)
        {
            this.contentMap = new T[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    this.contentMap[x, y] = value;
                }
            }
        }

        public Vector Size { get => new Vector(this.contentMap.GetLength(0), this.contentMap.GetLength(1)); }

        public int SizeX { get => this.contentMap.GetLength(0); }

        public int SizeY { get => this.contentMap.GetLength(0); }

        T IMapView<T>.this[Vector position] => this.contentMap[position.X, position.Y];

        T IMapView<T>.this[int x, int y] => this.contentMap[x, y];

        public T this[Vector position] { get => this.contentMap[position.X, position.Y]; set => this.contentMap[position.X, position.Y] = value; }

        public T this[int x, int y] { get => this.contentMap[x, y]; set => this.contentMap[x, y] = value; }

        public T GetContent(int x, int y)
        {
            return this.contentMap[x, y];
        }

        public T GetContent(Vector position)
        {
            return this.contentMap[position.X, position.Y];
        }

        public void SetContent(T content, int x, int y)
        {
            this.contentMap[x, y] = content;
        }

        public void SetContent(T content, Vector position)
        {
            this.contentMap[position.X, position.Y] = content;
        }
    }
}
