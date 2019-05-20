using System;
using System.Collections.Generic;
using TWF.Agent;

namespace TWF.State
{
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
        IDictionary<String, float> lastTicks = new Dictionary<String, float>();

        public void Tick(IActionQueue actionQueue, IWorldView worldView, IList<(IAgent, float)> agents, float currentTime)
        {
            foreach (var agent in agents)
            {
                if (lastTicks.ContainsKey(agent.Item1.Name))
                {
                    float lastTick = lastTicks[agent.Item1.Name];
                    if (lastTick + agent.Item2 < currentTime)
                    {
                        actionQueue.ExecuteSynchronously(agent.Item1.execute(worldView));
                        lastTicks[agent.Item1.Name] = lastTick + agent.Item2;
                    }
                }
                else
                {
                    lastTicks[agent.Item1.Name] = currentTime;
                }
            }
        }
    }
}
