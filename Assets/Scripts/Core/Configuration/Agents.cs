using System.Collections.Generic;
using System;
using System.Linq;

namespace TWF
{
    /// <summary>
    /// Defines all the world agents.
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
