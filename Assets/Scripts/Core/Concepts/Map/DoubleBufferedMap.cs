namespace TWF
{
    public class DoubleBufferedMap<T> : IMap<T>
    {
        private readonly ArrayMap<T>[] arrayMaps;
        private int activeMap;

        public DoubleBufferedMap(Vector size)
        {
            this.arrayMaps = new ArrayMap<T>[2];
            this.arrayMaps[0] = new ArrayMap<T>(size);
            this.arrayMaps[1] = new ArrayMap<T>(size);
            this.activeMap = 0;
        }

        public DoubleBufferedMap(Vector size, T value)
            : this(size.X, size.Y, value)
        {
        }

        public DoubleBufferedMap(int sizeX, int sizeY, T value)
        {
            this.arrayMaps = new ArrayMap<T>[2];
            this.activeMap = 0;

            for (int i = 0; i < 2; ++i)
            {
                this.arrayMaps[i] = new ArrayMap<T>(sizeX, sizeY, value);
            }
        }

        public Vector Size => this.ActiveMap.Size;

        public int SizeX => this.ActiveMap.SizeX;

        public int SizeY => this.ActiveMap.SizeY;

        public ArrayMap<T> ActiveMap => this.arrayMaps[this.activeMap];

#pragma warning disable CA1043 // Use Integral Or String Argument For Indexers
        public T this[Vector position] { get => this.ActiveMap[position]; set => this.ActiveMap[position] = value; }
#pragma warning restore CA1043 // Use Integral Or String Argument For Indexers

        T IMapView<T>.this[Vector position] => this.ActiveMap[position];

        public void RegisterListenener(MapUpdateListener<T> updateListener)
        {
            this.arrayMaps[0].RegisterListenener(updateListener);
            this.arrayMaps[1].RegisterListenener(updateListener);
        }

        public void Swap()
        {
            this.activeMap = 1 - this.activeMap;
        }
    }
}
