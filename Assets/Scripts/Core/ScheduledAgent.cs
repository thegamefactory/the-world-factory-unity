﻿namespace TWF
{
    /// <summary>
    /// A tuple for an agent scheduled to be executed regularly at a given period of time (in seconds).
    /// </summary>
    public class ScheduledAgent
    {
        public ScheduledAgent(IAgent agent, float period)
        {
            Agent = agent;
            Period = period;
        }

        public IAgent Agent { get; }
        public float Period { get; }
        public string Name => Agent.Name;
    }
}
