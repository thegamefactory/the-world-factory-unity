namespace TWF
{
    using System;
    using System.Diagnostics.Contracts;

    public static class ToolBehaviors
    {
        public static readonly string Zoner = "zoner";

        public static void RegisterDefaults(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            var zones = worldRules.Zones;

            worldRules.ToolBehaviors[Zoner] = m =>
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
