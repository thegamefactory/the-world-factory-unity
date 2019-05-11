using TWF.State;
using TWF.Generation;
using System;

namespace TWF
{
    public static class GameFactory
    {
        /// <summary>
        /// Return a list of tuples (IAgent, float), where float is the period in seconds at which the agent should be executed.
        /// </summary>
        public static GameState Create(Vector size, ITileMapGenerator tileMapGenerator, Random random)
        {
            var tileMap = tileMapGenerator.Generate(size);
            return new GameState(tileMap, new Ticker());
        }
    }
}
