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

        public T GetElement(int x, int y)
        {
            return contentMap[x, y];
        }

        public T GetElement(Vector position)
        {
            return contentMap[position.X, position.Y];
        }

        public Vector GetSize()
        {
            return new Vector(contentMap.GetLength(0), contentMap.GetLength(1));
        }

        public int GetSizeX()
        {
            return contentMap.GetLength(0);
        }

        public int GetSizeY()
        {
            return contentMap.GetLength(1);
        }

        public void SetElement(T content, int x, int y)
        {
            contentMap[x, y] = content;
        }

        public void SetElement(T content, Vector position)
        {
            contentMap[position.X, position.Y] = content;
        }
    }
}
