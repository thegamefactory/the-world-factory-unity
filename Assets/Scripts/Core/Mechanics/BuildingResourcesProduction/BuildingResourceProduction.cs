namespace TWF
{
    using System;

    /// <summary>
    /// Represents a resource production attached to a building model.
    /// A negative quantity represents a need.
    /// </summary>
    public struct BuildingResourceProduction : IEquatable<BuildingResourceProduction>
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
        public int ResourceId;
        public int ResourceQuantity;
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter
#pragma warning restore CA1051 // Do not declare visible instance fields

        public BuildingResourceProduction(int resourceId, int resourceQuantity)
        {
            this.ResourceId = resourceId;
            this.ResourceQuantity = resourceQuantity;
        }

        public static bool operator ==(BuildingResourceProduction left, BuildingResourceProduction right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BuildingResourceProduction left, BuildingResourceProduction right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj is BuildingResourceProduction need && this.Equals(need);
        }

        public bool Equals(BuildingResourceProduction other)
        {
            return this.ResourceId == other.ResourceId &&
                   this.ResourceQuantity == other.ResourceQuantity;
        }

        public override int GetHashCode()
        {
            var hashCode = -1050504456;
            hashCode = (hashCode * -1521134295) + this.ResourceId.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.ResourceQuantity.GetHashCode();
            return hashCode;
        }
    }
}
