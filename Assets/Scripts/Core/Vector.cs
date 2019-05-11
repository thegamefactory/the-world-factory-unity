namespace TWF
{
    /// <summary>
    /// An X, Y tuple used to represent either vectors or positions.
    /// </summary>
    public struct Vector
    {
        public int X;
        public int Y;

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}