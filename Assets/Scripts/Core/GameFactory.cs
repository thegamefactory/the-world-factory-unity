using TWF.Agent;
using TWF.Map;
using TWF.Tool;
using TWF.Terrain;
using System.Collections.Generic;
using System;

namespace TWF
{
    public static class GameFactory
    {
        public static GameService Create(Vector size, ITileMapGenerator tileMapGenerator, Random random)
        {
            var tileMap = tileMapGenerator.Generate(size);
            var entityMap = new EntityMap(size);

            return new GameService(tileMap, entityMap, CreateAgents(random), CreateToolConfig());
        }

        private static IList<(IAgent, float)> CreateAgents(Random random)
        {
            var agents = new List<(IAgent, float)>();
            agents.Add((new Constructor(() => random.NextDouble() < 0.1, random.Next), 1.0f));
            return agents;
        }

        private static ToolConfig CreateToolConfig()
        {
            return new ToolConfig(
                CreateTools(),
                CreateToolBrushes());
        }

        private static IDictionary<ToolBehaviorType, ITool> CreateTools()
        {
            var tools = new Dictionary<ToolBehaviorType, ITool>();
            tools.Add(ToolBehaviorType.ZONER, new Tool.Tool(new Zoner()));
            return tools;
        }

        private static IDictionary<ToolBrushType, IToolBrush> CreateToolBrushes()
        {
            var toolBrushes = new Dictionary<ToolBrushType, IToolBrush>();
            toolBrushes.Add(ToolBrushType.MANATTHAN, new ManatthanBrush());
            toolBrushes.Add(ToolBrushType.PEN, new PenToolBrush());
            toolBrushes.Add(ToolBrushType.RECTANGLE, new RectangleToolBrush());
            return toolBrushes;
        }
    }
}
