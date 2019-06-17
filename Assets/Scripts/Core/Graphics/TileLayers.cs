namespace TWF.Graphics
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A registry of all the layers that exist.
    /// </summary>
    public class TileLayers
    {
        private readonly LinkedList<Func<IWorldView, ITileLayer>> layerProviders = new LinkedList<Func<IWorldView, ITileLayer>>();
        private IDictionary<string, ITileLayer> layers = new Dictionary<string, ITileLayer>();

        public ITileLayer this[string name]
        {
            get
            {
                if (!this.layers.ContainsKey(name))
                {
                    throw new KeyNotFoundException(name);
                }

                return this.layers[name];
            }
        }

        public void RegisterLayerProvider(Func<IWorldView, ITileLayer> layerProvider)
        {
            this.layerProviders.AddLast(layerProvider);
        }

        public void OnNewWorld(IWorldView worldView)
        {
            this.layers = new Dictionary<string, ITileLayer>();
            foreach (var layerProvider in this.layerProviders)
            {
                var layer = layerProvider(worldView);
                this.layers[layer.Name] = layer;
            }
        }
    }
}
