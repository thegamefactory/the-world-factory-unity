using System;
using System.Collections.Generic;
using TWF.Agent;

namespace TWF
{
    public class Ticker
    {
        IDictionary<String, float> lastTicks = new Dictionary<String, float>();

        public void Tick(IGameActionQueue gameActionQueue, IGameState gameState, IList<(IAgent, float)> agents, float currentTime)
        {
            foreach (var agent in agents)
            {
                if (lastTicks.ContainsKey(agent.Item1.Name))
                {
                    float lastTick = lastTicks[agent.Item1.Name];
                    if (lastTick + agent.Item2 < currentTime)
                    {
                        gameActionQueue.executeSynchronous(agent.Item1.execute(gameState));
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
