using TWF.Agent;
using TWF.Map;
using TWF.Tool;
using System.Collections.Generic;
using System;

namespace TWF
{
    public static class GameFactory
    {
        public static GameService Create(int width, int height)
        {
            var tileMap = new TileMap(width, height);
            var entityMap = new EntityMap(width, height);

            return new GameService(tileMap, entityMap, CreateAgents(), CreateTools());
        }

        private static IList<(IAgent, float)> CreateAgents()
        {
            var agents = new List<(IAgent, float)>();
            agents.Add((new Constructor(() => new Random().NextDouble() < 0.1), 1.0f));
            return agents;
        }

        private static IDictionary<ToolType, ITool> CreateTools()
        {
            var tools = new Dictionary<ToolType, ITool>();
            tools.Add(ToolType.ZONER, new Zoner());
            return tools;
        }
    }
}
