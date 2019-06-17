namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The Ticker is called periodically and triggers the due agent executions.
    /// In the current implementation the Ticker executes all the due agents synchronously.
    ///
    /// The Ticker normalizes the agents executions.
    /// That is, even if the Ticker is called irregulary, it will ensure that the agents are called at intervals specified at construction.
    /// It does it by keeping track of each last agent execution.
    /// </summary>
    public class Ticker
    {
        private readonly IDictionary<string, float> lastTicks = new Dictionary<string, float>();

        public void Tick(IActionQueue actionQueue, IWorldView worldView, IEnumerable<ScheduledAgent> agents, float currentTime)
        {
            Contract.Requires(actionQueue != null);
            Contract.Requires(agents != null);

            foreach (var agent in agents)
            {
                if (this.lastTicks.ContainsKey(agent.Agent.Name))
                {
                    float lastTick = this.lastTicks[agent.Agent.Name];
                    if (lastTick + agent.Period < currentTime)
                    {
                        actionQueue.ExecuteSynchronously(agent.Agent.Execute(worldView));
                        this.lastTicks[agent.Agent.Name] = lastTick + agent.Period;
                    }
                }
                else
                {
                    this.lastTicks[agent.Agent.Name] = currentTime;
                }
            }
        }
    }
}
