using TWF.Agent;
using TWF.Map;
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
            var agents = new List<IAgent>();
            agents.Add(CreateConstructionAgent());
            return new GameService(tileMap, entityMap, agents);
        }

        private static IAgent CreateConstructionAgent()
        {
            return new Constructor(() => new Random().NextDouble() < 0.1);
        }
    }
}
