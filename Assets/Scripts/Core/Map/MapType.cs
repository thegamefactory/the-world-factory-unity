namespace TWF
{
    /// <summary>
    /// A map type identifier. This corresponds to a world "layer" identifier. See "Maps".
    /// </summary>
    public class MapType
    {
        private string Name { get; }

        public MapType(string name)
        {
            Name = name;
        }

        public override bool Equals(object o)
        {
            if (o == null) return false;
            if (!(o is MapType)) return false;
            MapType other = (MapType)o;
            return this.Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return 41 * Name.GetHashCode();
        }

        public override string ToString()
        {
            return "MapType(" + Name + ")";
        }
    }
}
