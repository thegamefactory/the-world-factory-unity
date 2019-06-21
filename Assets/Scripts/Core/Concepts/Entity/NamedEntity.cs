namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A tuple (id, name) to represent an entity.
    /// </summary>
    public struct NamedEntity : IEquatable<NamedEntity>
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
        public int Id;
        public string Name;
#pragma warning restore CA1051 // Do not declare visible instance fields

        public NamedEntity(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public static bool operator ==(NamedEntity left, NamedEntity right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NamedEntity left, NamedEntity right)
        {
            return !(left == right);
        }

        public void SetId(int id)
        {
            this.Id = id;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is NamedEntity entity && this.Equals(entity);
        }

        public bool Equals(NamedEntity other)
        {
            return this.Id == other.Id &&
                   this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = -1919740922;
            hashCode = (hashCode * -1521134295) + this.Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Name);
            return hashCode;
        }
    }
}
