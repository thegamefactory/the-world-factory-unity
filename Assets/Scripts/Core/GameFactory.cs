﻿using TWF.Agent;
using TWF.Map;
using TWF.Tool;
using System.Collections.Generic;
using System;

namespace TWF
{
    public static class GameFactory
    {
        public static GameService Create(int width, int height, Random random)
        {
            var tileMap = new TileMap(width, height);
            var entityMap = new EntityMap(width, height);

            return new GameService(tileMap, entityMap, CreateAgents(random), CreateTools());
        }

        private static IList<(IAgent, float)> CreateAgents(Random random)
        {
            var agents = new List<(IAgent, float)>();
            agents.Add((new Constructor(() => random.NextDouble() < 0.1), 1.0f));
            return agents;
        }

        private static IDictionary<ToolBehaviorType, ITool> CreateTools()
        {
            var tools = new Dictionary<ToolBehaviorType, ITool>();
            tools.Add(ToolBehaviorType.ZONER, new Tool.Tool(new Zoner()));
            return tools;
        }
    }
}
