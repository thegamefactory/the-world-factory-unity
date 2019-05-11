using TWF.Map;
using TWF.Agent;
using TWF.Tool;
using System.Collections.Generic;
using System;

namespace TWF
{
    /// <summary>
    /// The entry point for the UI to interact with the game.
    /// 
    /// The gameService manages the gameState (corresponding to the state of a single game instance) as well as other elements that are unique to the service.
    /// 
    /// In short, the game service defines game mechanics while the game state is a container for game data. 
    /// 
    /// The gameService is designed to be used as singleton, although this is not a hard requirement.
    /// On the other hands different instances of gameState can be created and destroyed, for example to restart a game or load a game.
    /// As it is not useful to have multiple games open simultaneously on a desktop application, the game service contains currently an unique game state object.
    /// There's no hard requirement for that, having the game service managing multiple games would require to alter the methods interfaces and pass alongside a gameState identifier.
    /// 
    /// All the UI interactions with the game should go via the GameService.
    /// 
    /// As convenience, gameState can never be null; this means the game service needs to be initialized with a valid game state.
    /// </summary>
    public class GameService : IGameStateView
    {
        GameState state;
        IList<(IAgent, float)> agents;
        ToolConfig toolConfig;
        Ticker ticker = new Ticker();

        /// <summary>
        /// Constructs a new game service.
        /// </summary>
        /// <param name="state">The state of the game.</param>
        /// <param name="agents">Background jobs that update the game state based on defined rules.
        /// Defined as a list of tuples (IAgent, float), where float is the period in seconds at which the agent should be executed.</param>
        /// <param name="toolConfig">Tools that can be used by the player to interact with the game.</param>
        public GameService(GameState state, IList<(IAgent, float)> agents, ToolConfig toolConfig)
        {
            this.state = state;
            this.agents = agents;
            this.toolConfig = toolConfig;
        }

        /// <summary>
        /// Sets the game state. This should be used for things like loading a game or resetting the state to a new game.
        /// </summary>
        public void SetGameState(GameState newState)
        {
            this.state = newState;
        }

        /// <summary>
        /// Converts the position given on a normalized scale onto an absolute position.
        /// </summary>
        /// <return>The absolute position.</return>
        /// <param name="x">The x coordinate between 0 and 1.</param>
        /// <param name="y">The y coordinate between 0 and 1.</param>
        public Vector ConvertPosition(float x, float y)
        {
            Vector size = state.GetSize();
            return new Vector((int)(size.X * x), (int)(size.Y * y));
        }

        /// <summary>
        /// Applies the given toolType and associated modifier to the given positions processed with the brush type.
        /// </summary>
        public ToolOutcome ApplyTool(ToolBehaviorType toolType, Modifier modifier, IEnumerable<Vector> positions, ToolBrushType toolBrushType)
        {
            return toolConfig.GetTool(toolType).Apply(state.GetActionQueue(), modifier, positions, toolConfig.GetToolBrush(toolBrushType));
        }

        /// <summary>
        /// Previews the application of the given toolType and associated modifier to the given positions processed with the brush type.
        /// </summary>
        public PreviewOutcome PreviewTool(ToolBehaviorType toolType, Modifier modifier, IEnumerable<Vector> positions, ToolBrushType toolBrushType)
        {
            return toolConfig.GetTool(toolType).Preview(state, modifier, positions, toolConfig.GetToolBrush(toolBrushType));
        }

        /// <summary>
        /// Triggers all the agents, based on the <paramref name="currentTime"/>, given in seconds.
        /// </summary>
        /// <param name="currentTime">The current time, given in seconds.</param>
        public void Tick(float currentTime)
        {
            state.Tick(agents, currentTime);
        }

        /// <summary>
        /// <summary>
        /// Return the size of the map.
        /// </summary>
        /// <return>The size of the map.</return>
        public Vector GetSize()
        {
            return state.GetSize();
        }

        /// <summary>
        /// Get an enumerable to iterate over all the tiles.
        /// </summary>
        /// <return>An enumerable to iterate over all the tiles.</return>
        public IEnumerable<(Vector, ITileView)> GetTiles()
        {
            return state.GetTiles();
        }

        /// <summary>
        /// Get the tile at the given position using an absolute scale.
        /// </summary>
        /// <return>The tile at the given position.</return>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public ITileView GetTile(int x, int y)
        {
            return state.GetTile(x, y);
        }

        /// <summary>
        /// Get the tile at the given position using an absolute scale.
        /// </summary>
        /// <return>The tile at the given position.</return>
        /// <param name="position">The position.</param>
        public ITileView GetTile(Vector position)
        {
            return state.GetTile(position);
        }
    }
}
