using UnityEngine;

namespace TWF.Graphics
{
    public interface ITileLayer
    {
        string Name { get; }
        void OnNewWorld(IWorldView worldView);

        Color? GetColor(Vector pos);
    }
}