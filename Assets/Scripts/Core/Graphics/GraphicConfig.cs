namespace TWF.Graphics
{
    public class GraphicConfig
    {
        public TileLayers TileLayers { get; } = new TileLayers();

        public void OnNewWorld(IWorldView worldView)
        {
            TileLayers.OnNewWorld(worldView);
        }
    }
}
