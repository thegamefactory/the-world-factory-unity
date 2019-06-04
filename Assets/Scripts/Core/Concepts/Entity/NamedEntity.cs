using System;
using System.Collections.Generic;

namespace TWF
{
    public struct NamedEntity : IEquatable<NamedEntity>
    {
        public NamedEntity(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id;
        public string Name;

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is NamedEntity entity && Equals(entity);
        }

        public bool Equals(NamedEntity other)
        {
            return Id == other.Id &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = -1919740922;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public static bool operator ==(NamedEntity left, NamedEntity right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NamedEntity left, NamedEntity right)
        {
            return !(left == right);
        }
    }
}
