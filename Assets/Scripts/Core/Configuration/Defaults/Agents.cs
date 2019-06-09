namespace TWF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines default background agents scheduled at fixed interval which mutate the state of the world.
    /// </summary>
    public static class Agents
    {
        public static Dictionary<string, ScheduledAgent> AllAgents(Random random)
        {
            ScheduledAgent[] agents = { new ScheduledAgent(new ZoneDeveloperFactory(random).CreateZoneDeveloper(0.1), 1.0f) };
            return new List<ScheduledAgent>(agents).ToDictionary((a) => a.Name);
        }
    }
}
