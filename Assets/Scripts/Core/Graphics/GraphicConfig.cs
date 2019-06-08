namespace TWF.Graphics
{
    /// <summary>
    /// Stores the graphic configuration of of the current world.
    /// Although the core doesn't need to be aware of graphics, it is handy as:
    /// 1) it enables to easily plug the graphic components to callbacks such as OnNewWorld
    /// 2) it gives a unified point to load/save the graphic configuration.
    /// A better implementation would probably be to:
    /// 1) expose events from the game service and enable arbitrary listener to subscribe to them
    /// 2) enable extension points for load/save game
    /// To revisit.
    /// </summary>
    public class GraphicConfig
    {
        public TileLayers TileLayers { get; } = new TileLayers();

        public void OnNewWorld(IWorldView worldView)
        {
            TileLayers.OnNewWorld(worldView);
        }
    }
}
