using System.Collections.Generic;

namespace TWF
{
    public static class ToolBrushes
    {
        public static Dictionary<string, IToolBrush> AllToolBrushes { get; } = new Dictionary<string, IToolBrush>();

        public static IToolBrush Pen = RegisterToolBrush(new PenToolBrush());
        public static IToolBrush Manatthan = RegisterToolBrush(new ManatthanToolBrush());
        public static IToolBrush Rectangle = RegisterToolBrush(new RectangleToolBrush());

        private static IToolBrush RegisterToolBrush(IToolBrush toolBrush)
        {
            AllToolBrushes.Add(toolBrush.Name, toolBrush);
            return toolBrush;
        }
    }
}
