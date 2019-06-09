namespace TWF
{
    using System;

    /// <summary>
    /// An X, Y tuple used to represent either vectors or positions.
    /// </summary>
    public struct Vector : IEquatable<Vector>
    {
        public int X;
        public int Y;

        public Vector(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector && this.Equals((Vector)obj);
        }

        public bool Equals(Vector other)
        {
            return this.X == other.X &&
                   this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + this.X.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Y.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return "(" + this.X + ", " + this.Y + ")";
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.Equals(vector2);
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !(vector1 == vector2);
        }
    }
}