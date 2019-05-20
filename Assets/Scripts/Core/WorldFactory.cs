using TWF.Generation;
using System;

namespace TWF
{
    public static class WorldFactory
    {
        /// <summary>
        /// Return a list of tuples (IAgent, float), where float is the period in seconds at which the agent should be executed.
        /// </summary>
        public static World Create(Vector size, ITileMapGenerator tileMapGenerator, Random random)
        {
            var tileMap = tileMapGenerator.Generate(size);
            return new World(tileMap, new Ticker());
        }
    }
}
