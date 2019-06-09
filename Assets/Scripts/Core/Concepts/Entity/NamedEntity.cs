namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A tuple (id, name) to represent an entity.
    /// The bare integer representation should be preferred when possible but for code maintainability it is sometimes better to reference the name.
    /// </summary>
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
            hashCode = hashCode * -1521134295 + this.Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Name);
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
