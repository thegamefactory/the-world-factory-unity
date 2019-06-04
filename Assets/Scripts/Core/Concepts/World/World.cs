using System;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// Interface to interact with anything that is unique to a specific world instance.
    /// </summary>
    public class World : IWorldView
    {
        private Maps maps;
        private WorldConfig config;
        private Ticker ticker;

        public World(Maps maps, WorldConfig config, Ticker ticker)
        {
            this.maps = maps;
            this.config = config;
            this.ticker = ticker;
        }

        public Vector Size { get => maps.Size; }
        public int SizeX { get => maps.SizeX; }
        public int SizeY { get => maps.SizeY; }
        public IReadOnlyRegistry Terrains => config.Terrains;
        public IReadOnlyRegistry Zones => config.Zones;
        public IReadOnlyDictionary<string, ScheduledAgent> Agents => config.Agents;
        public IReadOnlyDictionary<string, Func<string, IToolBehavior>> ToolBehaviors => config.ToolBehaviors;
        public IReadOnlyDictionary<string, IToolBrush> ToolBrushes => config.ToolBrushes;

        /// <summary>
        /// Get the map view corresponding to the given type.
        /// </summary>
        /// <return>The map view corresponding to the given type.</return>
        /// <param name="mapType">The type of the map.</param>
        public IMapView<T> GetMapView<T>(string mapType)
        {
            return maps.GetMap<T>(mapType);
        }

        /// <summary>
        /// Get the map corresponding to the given type.
        /// </summary>
        /// <return>The map view corresponding to the given type.</return>
        /// <param name="mapType">The type of the map.</param>
        public IMap<T> GetMap<T>(string mapType)
        {
            return maps.GetMap<T>(mapType);
        }

        /// <summary>
        /// Triggers all the <paramref name="agents"/>, based on the <paramref name="currentTime"/>, given in seconds.
        /// </summary>
        /// <param name="agents">The agents (jobs) that are mutating the world.</param>
        /// <param name="currentTime">The current time, given in seconds.</param>
        public void Tick(IEnumerable<ScheduledAgent> agents, float currentTime)
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
