using TWF.Graphics;

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
    /// As convenience, the current world can never be null.
    /// </summary>
    public class GameService
    {
        public WorldFactory WorldFactory { get; }
        private World world;

        public GraphicConfig GraphicConfig { get; } = new GraphicConfig();

        /// <summary>
        /// Constructs a new game service.
        /// </summary>
        /// <param name="world">The world.</param>
        /// Defined as a list of tuples (IAgent, float), where float is the period in seconds at which the agent should be executed.</param>
        public GameService()
        {
            WorldFactory = new WorldFactory();
            NewWorld();
        }

        /// <summary>
        /// Recreates the world.
        /// </summary>
        public IWorldView NewWorld()
        {
            world = WorldFactory.Create();
            GraphicConfig.OnNewWorld(world);
            return world;
        }

        /// <summary>
        /// Sets the world. This should be used for things like loading a world or resetting the world to a new one.
        /// </summary>
        public void SetWorld(World world)
        {
            this.world = world;
        }

        /// <summary>
        /// Returns the active world.
        /// </summary>
        /// <return>The active world view.</return>
        public IWorldView GetWorldView()
        {
            return this.world;
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
        /// Return a tool applier to use tools against the current world.
        /// </summary>
        /// <return>A tool applier to use tools against the current world.</return>
        public IToolApplier GetToolApplier()
        {
            return new ToolApplier(world);
        }

        /// <summary>
        /// Triggers all the agents, based on the <paramref name="currentTime"/>, given in seconds.
        /// </summary>
        /// <param name="currentTime">The current time, given in seconds.</param>
        public void Tick(float currentTime)
        {
            world.Tick(world.Agents.Values, currentTime);
        }
    }
}
