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
        private IDictionary<string, ITileLayer> layers = new Dictionary<string, ITileLayer>();

        public void RegisterLayer(ITileLayer layer)
        {
            layers[layer.Name] = layer;
        }

        public void OnNewWorld(IWorldView worldView)
        {
            foreach (ITileLayer layer in layers.Values)
            {
                layer.OnNewWorld(worldView);
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
