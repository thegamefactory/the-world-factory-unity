using System;

namespace TWF.Input
{
    class MousePositionProvider : IMousePositionProvider
    {
        public Vector? GetMousePosition()
        {
            try
            {
                Tuple<float, float> clickedPosition = CoordinateMapper.ScreenPositionToMeshPosition(UnityEngine.Input.mousePosition);
                return Root.GameService.ConvertPosition(clickedPosition.Item1, clickedPosition.Item2);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }
    }
}
