namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface to interact with anything that is unique to a specific world instance.
    /// </summary>
    public class World : IWorldView
    {
        private readonly Maps maps;
        private readonly Ticker ticker;

        public World(Maps maps, WorldRules rules, Ticker ticker)
        {
            this.maps = maps;
            this.Rules = rules;
            this.ticker = ticker;
        }

        public Vector Size { get => this.maps.Size; }

        public int SizeX { get => this.maps.SizeX; }

        public int SizeY { get => this.maps.SizeY; }

        public IWorldRules Rules { get; }

        /// <summary>
        /// Get the map view corresponding to the given type.
        /// </summary>
        /// <return>The map view corresponding to the given type.</return>
        /// <param name="mapType">The identifier of the map.</param>
        /// <typeparam name="T">The type of the returned map view.</param>
        public IMapView<T> GetMapView<T>(string mapType)
        {
            return this.maps.GetMap<T>(mapType);
        }

        /// <summary>
        /// Get the map corresponding to the given type.
        /// </summary>
        /// <return>The map view corresponding to the given type.</return>
        /// <param name="mapType">The identifier of the map.</param>
        /// <typeparam name="T">The type of the returned map view.</param>
        public IMap<T> GetMap<T>(string mapType)
        {
            return this.maps.GetMap<T>(mapType);
        }

        /// <summary>
        /// Triggers all the <paramref name="agents"/>, based on the <paramref name="currentTime"/>, given in seconds.
        /// </summary>
        /// <param name="agents">The agents (jobs) that are mutating the world.</param>
        /// <param name="currentTime">The current time, given in seconds.</param>
        public void Tick(IEnumerable<ScheduledAgent> agents, float currentTime)
        {
            this.ticker.Tick(this.GetActionQueue(), this, agents, currentTime);
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
            private readonly World world;

            internal ActionQueue(World world)
            {
                this.world = world;
            }

            public void ExecuteSynchronously(Action<World> action)
            {
                action(this.world);
            }
        }
    }
}
