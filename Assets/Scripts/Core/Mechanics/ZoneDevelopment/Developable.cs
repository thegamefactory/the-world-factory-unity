using System;
namespace TWF
{
    /// <summary>
    /// A Zone trait that indicates that the ZoneDeveloper game engine is controlling the zone building development automatically.
    /// </summary>
    public class Developable : ITrait
    {
        private Developable()
        {

        }

        public static Developable Instance { get; } = new Developable();
    }

    static class DevelopableExtensions
    {
        public static Boolean IsDevelopable(this Zone zone)
        {
            return zone.HasTrait(Developable.Instance.GetType());
        }
    }
}
