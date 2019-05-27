using System;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// An identifier for land usage type.
    /// </summary>
    public class Zone : IEquatable<Zone>
    {
        public string Name { get; }
        private IDictionary<Type, ITrait> Traits { get; } = new Dictionary<Type, ITrait>();

        public Zone(string name)
        {
            Name = name;
        }

        public Zone(string name, params ITrait[] traits) : this(name)
        {
            foreach (ITrait t in traits)
            {
                Traits.Add(t.GetType(), t);
            }
        }

        public bool HasTrait(Type trait)
        {
            return Traits.ContainsKey(trait);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Zone);
        }

        public bool Equals(Zone other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(Zone zone1, Zone zone2)
        {
            return EqualityComparer<Zone>.Default.Equals(zone1, zone2);
        }

        public static bool operator !=(Zone zone1, Zone zone2)
        {
            return !(zone1 == zone2);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
