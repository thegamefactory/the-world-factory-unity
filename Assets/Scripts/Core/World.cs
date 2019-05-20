using System;
using System.Collections.Generic;
using TWF.Map;
using TWF.Map.Tile;
using TWF.Agent;
using TWF.Map.Building;

namespace TWF
{
    /// <summary>
    /// Interface to interact with anything that is unique to a specific world instance.
    /// </summary>
    public class World : IWorldView
    {
        private IMap<Tile> tileMap;
        private Ticker ticker;

        public World(IMap<Tile> tileMap, Ticker ticker)
        {
            this.tileMap = tileMap;
            this.ticker = ticker;
        }

        /// <inheritdoc/>
        public Vector Size { get => tileMap.Size; }

        /// <inheritdoc/>
        public int SizeX { get => tileMap.SizeX; }

        /// <inheritdoc/>
        public int SizeY { get => tileMap.SizeY; }

        public ITileView GetTile(int x, int y)
        {
            return tileMap.GetElement(x, y);
        }

        public ITileView GetTile(Vector position)
        {
            return tileMap.GetElement(position);
        }

        public void SetTileZone(TileZone zone, int x, int y)
        {
            tileMap.GetElement(x, y).Zone = zone;
        }

        public void SetTileZone(TileZone zone, Vector position)
        {
            SetTileZone(zone, position.X, position.Y);
        }

        public void SetTileBuilding(IBuilding building, int x, int y)
        {
            tileMap.GetElement(x, y).Building = building;
        }

        public void SetTileBuilding(IBuilding entity, Vector position)
        {
            SetTileBuilding(entity, position.X, position.Y);
        }

        /// <summary>
        /// Triggers all the <paramref name="agents"/>, based on the <paramref name="currentTime"/>, given in seconds.
        /// </summary>
        /// <param name="agents">The agents (jobs) that are mutating the world.</param>
        /// <param name="currentTime">The current time, given in seconds.</param>
        public void Tick(IList<(IAgent, float)> agents, float currentTime)
        {
            ticker.Tick(GetActionQueue(), this, agents, currentTime);
        }

        /// <summary>
        /// Temporary hack to get action queue working. Should use a proper queue in the future.
        /// </summary>
        public IActionQueue GetActionQueue()
        {
            return new ActionQueue(this);
        }

        private class ActionQueue : IActionQueue
        {
            private World world;

            internal ActionQueue(World world)
            {
                this.world = world;
            }

            public void ExecuteSynchronously(Action<World> action)
            {
                action(world);
            }
        }
    }
}
