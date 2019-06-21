namespace TWF
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Defines default background agents scheduled at fixed interval which mutate the state of the world.
    /// </summary>
    public static class Agents
    {
        public static void RegisterDefaults(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            Random random = worldRules.Random;
            var zoneDeveloper = new ZoneDeveloper(random);
            worldRules.Agents[zoneDeveloper.Name] = new ScheduledAgent(zoneDeveloper, 1.0f);
        }
    }
}
