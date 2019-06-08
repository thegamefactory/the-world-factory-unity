using UnityEngine;

namespace TWF.Graphics
{
    /// <summary>
    /// A layer applicable to the tile map.
    /// A layer returns an optional color for each position.
    /// If the returned color is null, the next active layer is queried for a color, and so on until a layer provides a color, or the default color is used if there are no more layers.
    /// </summary>
    public interface ITileLayer
    {
        string Name { get; }
        void OnNewWorld(IWorldView worldView);

        Color? GetColor(Vector pos);
    }
}