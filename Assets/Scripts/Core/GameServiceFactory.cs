using TWF.Agent;
using TWF.Map;
using TWF.Tool;
using System.Collections.Generic;
using System;

namespace TWF
{
    public static class GameServiceFactory
    {
        public static GameService Create(int width, int height)
        {
            var tileMap = new TileMap(width, height);
            var entityMap = new EntityMap(width, height);

            return new GameService(tileMap, entityMap, CreateAgents(), CreateTools());
        }

        private static List<IAgent> CreateAgents()
        {
            var agents = new List<IAgent>();
            agents.Add(new Constructor(() => new Random().NextDouble() < 0.1));
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
