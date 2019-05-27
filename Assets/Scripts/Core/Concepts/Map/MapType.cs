using System;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// A map type identifier. This corresponds to a world "layer" identifier. See "Maps".
    /// </summary>
    public class MapType : IEquatable<MapType>
    {
        private string Name { get; }

        public MapType(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MapType);
        }

        public bool Equals(MapType other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(MapType type1, MapType type2)
        {
            return EqualityComparer<MapType>.Default.Equals(type1, type2);
        }

        public static bool operator !=(MapType type1, MapType type2)
        {
            return !(type1 == type2);
        }
    }
}
