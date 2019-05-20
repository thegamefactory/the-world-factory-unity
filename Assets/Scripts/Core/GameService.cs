using TWF.State;
using TWF.Agent;
using TWF.Tool;
using TWF.State.Tile;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// The entry point for the UI to interact with the game.
    /// 
    /// The game service is designed to be used as singleton, although this is not a hard requirement.
    /// On the other hands different instances of world can be created and destroyed, for example to restart a game or load a game.
    /// As it is not useful to have multiple worlds open simultaneously on a desktop application, the game service contains currently an unique world object.
    /// There's no hard requirement for that, having the game service managing multiple worlds would require to alter the methods interfaces and pass alongside a world identifier.
    /// 
    /// All the UI interactions should go via the this class (facade pattern).
    /// 
    /// As convenience, the current world can never be null; this means the game service needs to be initialized with a valid world.
    /// </summary>
    public class GameService : IWorldView
    {
        World world;
        IList<(IAgent, float)> agents;
        ToolConfig toolConfig;
        Ticker ticker = new Ticker();

        /// <summary>
        /// Constructs a new game service.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="agents">Background jobs that update the world based on defined rules.
        /// Defined as a list of tuples (IAgent, float), where float is the period in seconds at which the agent should be executed.</param>
        /// <param name="toolConfig">Tools that can be used by the player to interact with the world.</param>
        public GameService(World world, IList<(IAgent, float)> agents, ToolConfig toolConfig)
        {
            this.world = world;
            this.agents = agents;
            this.toolConfig = toolConfig;
        }

        /// <summary>
        /// Sets the world. This should be used for things like loading a world or resetting the world to a new one.
        /// </summary>
        public void SetWorld(World world)
        {
            this.world = world;
        }

        /// <summary>
        /// Converts the position given on a normalized scale onto an absolute position.
        /// </summary>
        /// <return>The absolute position.</return>
        /// <param name="x">The x coordinate between 0 and 1.</param>
        /// <param name="y">The y coordinate between 0 and 1.</param>
        public Vector ConvertPosition(float x, float y)
        {
            Vector size = world.Size;
            return new Vector((int)(size.X * x), (int)(size.Y * y));
        }

        /// <summary>
        /// Applies the given toolType and associated modifier to the given positions processed with the brush type.
        /// </summary>
        public ToolOutcome ApplyTool(ToolBehaviorType toolType, Modifier modifier, IEnumerable<Vector> positions, ToolBrushType toolBrushType)
        {
            return toolConfig.GetTool(toolType).Apply(world.GetActionQueue(), modifier, positions, toolConfig.GetToolBrush(toolBrushType));
        }

        /// <summary>
        /// Previews the application of the given toolType and associated modifier to the given positions processed with the brush type.
        /// </summary>
        public PreviewOutcome PreviewTool(ToolBehaviorType toolType, Modifier modifier, IEnumerable<Vector> positions, ToolBrushType toolBrushType)
        {
            return toolConfig.GetTool(toolType).Preview(world, modifier, positions, toolConfig.GetToolBrush(toolBrushType));
        }

        /// <summary>
        /// Triggers all the agents, based on the <paramref name="currentTime"/>, given in seconds.
        /// </summary>
        /// <param name="currentTime">The current time, given in seconds.</param>
        public void Tick(float currentTime)
        {
            world.Tick(agents, currentTime);
        }

        /// <inheritdoc/>
        public Vector Size { get => world.Size; }

        /// <inheritdoc/>
        public int SizeX { get => world.SizeX; }

        /// <inheritdoc/>
        public int SizeY { get => world.SizeY; }

        /// <inheritdoc/>
        public ITileView GetTile(int x, int y)
        {
            return world.GetTile(x, y);
        }

        /// <inheritdoc/>
        public ITileView GetTile(Vector position)
        {
            return world.GetTile(position);
        }
    }
}
