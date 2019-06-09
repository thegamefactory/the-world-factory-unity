namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ToolBehaviors
    {
        public static readonly string ZONER = "zoners";

        public static Func<string, IToolBehavior> Zoners(Entities zones)
        {
            return (m) =>
            {
                var zone = zones.GetNamedEntity(m);
                if (!zones.GetMarkerComponents(Zones.ManuallyZonable).IsMarked(zone.Id))
                {
                    throw new ArgumentException(zone.Name);
                }

                return new Zoner(zone);
            };
        }
    }
}
