namespace TWF.Input
{
    using System;

    internal class MousePositionProvider : IMousePositionProvider
    {
        public Vector? GetMousePosition()
        {
            Tuple<float, float> clickedPosition = CoordinateMapper.ScreenPositionToMeshPosition(UnityEngine.Input.mousePosition);
            if (clickedPosition != null)
            {
                return Root.GameService.ConvertPosition(clickedPosition.Item1, clickedPosition.Item2);
            }
            else
            {
                return null;
            }
        }
    }
}
