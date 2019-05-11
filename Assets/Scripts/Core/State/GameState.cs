using System;
using System.Collections.Generic;
using TWF.State.Map;
using TWF.State.Tile;
using TWF.Agent;
using TWF.State.Entity;

namespace TWF.State
{
    /// <summary>
    /// Interface to interact with anything that is unique to a specific game instance.
    /// </summary>
    public class GameState : IGameStateView
    {
        private IMap<Tile.Tile> tileMap;
        private Ticker ticker;

        public GameState(IMap<Tile.Tile> tileMap, Ticker ticker)
        {
            this.tileMap = tileMap;
            this.ticker = ticker;
        }

        public Vector GetSize()
        {
            return tileMap.GetSize();
        }

        public ITileView GetTile(int x, int y)
        {
            return tileMap.GetElement(x, y);
        }

        public ITileView GetTile(Vector position)
        {
            return tileMap.GetElement(position);
        }

        public IEnumerable<(Vector, ITileView)> GetTiles()
        {
            return MapTraverser.GetElements(tileMap as IMap<ITileView>);
        }

        public void SetTileZone(TileZone zone, int x, int y)
        {
            tileMap.GetElement(x, y).Zone = zone;
        }

        public void SetTileZone(TileZone zone, Vector position)
        {
            SetTileZone(zone, position.X, position.Y);
        }

        public void SetTileEntity(IEntity entity, int x, int y)
        {
            tileMap.GetElement(x, y).Entity = entity;
        }

        public void SetTileEntity(IEntity entity, Vector position)
        {
            SetTileEntity(entity, position.X, position.Y);
        }

        /// <summary>
        /// Triggers all the <paramref name="agents"/>, based on the <paramref name="currentTime"/>, given in seconds.
        /// </summary>
        /// <param name="agents">The agents (jobs) that are mutating the game.</param>
        /// <param name="currentTime">The current time, given in seconds.</param>
        public void Tick(IList<(IAgent, float)> agents, float currentTime)
        {
            ticker.Tick(GetActionQueue(), this, agents, currentTime);
        }

        /// <summary>
        /// Temporary hack to get action queue working. Should use a proper queue in the future.
        /// </summary>
        public IGameActionQueue GetActionQueue()
        {
            return new ActionQueue(this);
        }

        private class ActionQueue : IGameActionQueue
        {
            private GameState gameState;

            internal ActionQueue(GameState gameState)
            {
                this.gameState = gameState;
            }

            public void ExecuteSynchronously(Action<GameState> action)
            {
                action(gameState);
            }
        }
    }
}
