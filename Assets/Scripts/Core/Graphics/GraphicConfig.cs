namespace TWF.Graphics
{
    /// <summary>
    /// Stores the graphic configuration of of the current world.
    /// </summary>
    public class GraphicConfig
    {
        public TileLayers TileLayers { get; } = new TileLayers();

        public void OnNewWorld(IWorldView worldView)
        {
            this.TileLayers.OnNewWorld(worldView);
        }
    }
}
