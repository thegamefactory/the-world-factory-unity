using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWF.Graphics
{
    /// <summary>
    /// A registry of all the layers that exist.
    /// </summary>
    public class TileLayers
    {
        private LinkedList<Func<IWorldView, ITileLayer>> layerProviders = new LinkedList<Func<IWorldView, ITileLayer>>();
        private IDictionary<string, ITileLayer> layers = new Dictionary<string, ITileLayer>();

        public void RegisterLayerProvider(Func<IWorldView, ITileLayer> layerProvider)
        {
            layerProviders.AddLast(layerProvider);
        }

        public void OnNewWorld(IWorldView worldView)
        {
            layers = new Dictionary<string, ITileLayer>();
            foreach (var layerProvider in layerProviders)
            {
                var layer = layerProvider(worldView);
                layers[layer.Name] = layer;
            }
        }

        public ITileLayer this[string name]
        {
            get
            {
                if (!layers.ContainsKey(name))
                {
                    throw new KeyNotFoundException(name);
                }
                return layers[name];
            }
        }
    }
}
