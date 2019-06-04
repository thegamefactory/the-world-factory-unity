using UnityEngine;

namespace TWF.Graphics
{
    interface ITileLayer
    {
        Color? GetColor(Vector pos);
    }
}