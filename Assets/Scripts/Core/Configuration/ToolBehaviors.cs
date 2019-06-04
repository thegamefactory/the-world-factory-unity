using System.Collections.Generic;
using System.Linq;
using System;

namespace TWF
{
    /// <summary>
    /// A definition of all the tool behaviors.
    /// </summary>
    public static class ToolBehaviors
    {
        public static string ZONER = "zoners";

        public static Func<string, IToolBehavior> Zoners(Registry zones)
        {
            return (m) =>
            {
                var zone = zones.GetNamedEntity(m);
                if (!zones.GetMarkerComponentRegistry(Zones.MANUALLY_ZONABLE).IsMarked(zone.Id))
                {
                    throw new ArgumentException(zone.Name);
                }
                return new Zoner(zone);
            };
        }
    }
}
