using System;

namespace TWF
{
    public class Modifier
    {
        public string Identifier { get; }

        public Modifier(string identifier)
        {
            this.Identifier = identifier;
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (!(o is Modifier)) return false;
            Modifier other = (Modifier)o;
            return this.Identifier.Equals(other.Identifier);
        }

        public override int GetHashCode()
        {
            return 41 * Identifier.GetHashCode();
        }

        public override string ToString()
        {
            return "Modifier(" + Identifier + ")";
        }
    }
}