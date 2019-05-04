using TWF.Map;
using TWF.Agent;
using TWF.Tool;
using System.Collections.Generic;
using System;

namespace TWF
{
    /// <summary>
    /// The entry point for the UI to interact with the Game.
    /// Holds the state of the game defines the game behavior via agents.
    /// Objects that with to interact the game in a read-only fashion should obtain an instance of IGameState instead.
    /// </summary>
    public class GameService : IGameState, IGameActionQueue
    {
        TileMap tileMap;
        EntityMap entityMap;
        IList<(IAgent, float)> agents;
        IDictionary<ToolBehaviorType, ITool> tools;
        Ticker ticker = new Ticker();

        /// <summary>
        /// Constructs a new game.
        /// </summary>
        /// <param name="tileMap">The layout of the map, defines the zone of each tile.</param>
        /// <param name="entityMap">The layout of the entities, maps each position to an entity.</param>
        /// <param name="agents">Background jobs that update the game state based on defined rules.
        /// Defined as a list of tuples (IAgent, float), where float is the period in seconds at which the agent should be executed.</param>
        /// <param name="tools">Tools that can be used by the player to alter the state of the game.
        /// Defined as a dictionary (ToolType, ITool) which map each tool type to a tool implementation.</param>
        public GameService(TileMap tileMap, EntityMap entityMap, IList<(IAgent, float)> agents, IDictionary<ToolBehaviorType, ITool> tools)
        {
            this.tileMap = tileMap;
            this.entityMap = entityMap;
            this.agents = agents;
            this.tools = tools;
        }

        /// <summary>
        /// Returns the Entity corresponding to the given Position, or null if the map is empty there.
        /// </summary>
        /// <returns>The Entity corresponding to the given Position, or null if the map is empty there.</returns>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        public IEntity GetEntity(int x, int y)
        {
            return entityMap.GetEntity(x, y);
        }

        // <summary>
        /// Returns the Entity corresponding to the given Position, or null if the map is empty there.
        /// </summary>
        /// <returns>The Entity corresponding to the given Position, or null if the map is empty there.</returns>
        /// <param name="position">The entity position.</param>
        public IEntity GetEntity(Vector position)
        {
            return entityMap.GetEntity(position);
        }

        /// <summary>
        /// Return the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        public Vector GetSize()
        {
            return tileMap.GetSize();
        }

        /// <summary>
        /// Get an enumerable to iterate over all the tiles.
        /// </summary>
        /// <return>An enumerable to iterate over all the tiles.</return>
        public IEnumerable<(Vector, Tile)> GetTiles()
        {
            return tileMap.GetTiles();
        }

        /// <summary>
        /// Get the tile at the given position.
        /// </summary>
        /// <return>The tile at the given position.</return>
        public Tile GetTile(int x, int y)
        {
            return tileMap.GetTile(x, y);
        }

        /// <summary>
        /// Get the tile at the given position using an absolute scale.
        /// </summary>
        /// <return>The tile at the given position.</return>
        /// <param name="position">The position.</param>
        public Tile GetTile(Vector position)
        {
            return tileMap.GetTile(position);
        }

        /// <summary>
        /// Gets the tile at a continous position using a normalized scale.
        /// </summary>
        /// <returns>The tile at provided position.</returns>
        /// <param name="x">The x coordinate between 0 and 1.</param>
        /// <param name="y">The y coordinate between 0 and 1.</param>
        public Tile GetTile(float x, float y)
        {
            return tileMap.GetTile(x, y);
        }

        /// <summary>
        /// Gets the tile at a continous position using a normalized scale.
        /// </summary>
        /// <returns>The tile at provided position.</returns>
        /// <param name="x">The x coordinate between 0 and 1.</param>
        /// <param name="y">The y coordinate between 0 and 1.</param>
        public Vector GetPosition(float x, float y)
        {
            return tileMap.GetPosition(x, y);
        }

        internal void SetEntity(IEntity entity, int x, int y)
        {
            entityMap.SetEntity(entity, x, y);
        }

        internal void SetTileZone(Tile.TileZone zone, Vector position)
        {
            tileMap.SetTileZone(zone, position);
        }

        internal void SetTileZone(Tile.TileZone zone, int x, int y)
        {
            tileMap.SetTileZone(zone, x, y);
        }

        public ToolOutcome ApplyTool(LinkedList<Vector> positions, ToolBehaviorType toolType, Modifier modifier)
        {
            return tools[toolType].Apply(this, positions, modifier);
        }

        public ToolOutcome PreviewTool(LinkedList<Vector> positions, ToolBehaviorType toolType, Modifier modifier)
        {
            return tools[toolType].Preview(this, positions, modifier);
        }

        /// <summary>
        /// Triggers all the agents, based on the <paramref name="currentTime"/>, given in seconds.
        /// </summary>
        /// <param name="currentTime">The current time, given in seconds.</param>
        public void TickAgents(float currentTime)
        {
            ticker.Tick(this, this, agents, currentTime);
        }

        public void ExecuteSynchronous(Action<GameService> action)
        {
            // TODO: should 1) enqueue 2) wait until asynchronous processing on the queue 3) propagate exception, if any
            action(this);
        }
    }
}
