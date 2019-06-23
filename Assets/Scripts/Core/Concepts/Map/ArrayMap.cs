namespace TWF
{
    /// <summary>
    /// A concrete map implementation based on a bidimensional array.
    /// </summary>
    /// <typeparam name="T">The type of elements stored in the map.</typeparam>
    public class ArrayMap<T> : IMap<T>
    {
        private readonly T[,] contentMap;
        private MapUpdateListener<T> updateListener;

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

#pragma warning disable CA1043 // Use Integral Or String Argument For Indexers
        public T this[Vector position]
        {
            get => this.contentMap[position.X, position.Y];
            set
            {
                this.contentMap[position.X, position.Y] = value;
                this.updateListener?.Invoke(position, this.contentMap[position.X, position.Y], value);
            }
        }
#pragma warning restore CA1043 // Use Integral Or String Argument For Indexers

        public void RegisterListenener(MapUpdateListener<T> updateListener)
        {
            if (this.updateListener == null)
            {
                this.updateListener = updateListener;
            }
            else
            {
                this.updateListener += updateListener;
            }
        }
    }
}
