namespace TWF
{
    using System.Collections.Generic;

    public static class ToolBrushes
    {
        public static readonly IToolBrush Pen = RegisterToolBrush(new PenToolBrush());
        public static readonly IToolBrush Manatthan = RegisterToolBrush(new ManatthanToolBrush());
        public static readonly IToolBrush Rectangle = RegisterToolBrush(new RectangleToolBrush());

        public static Dictionary<string, IToolBrush> AllToolBrushes { get; } = new Dictionary<string, IToolBrush>();

        private static IToolBrush RegisterToolBrush(IToolBrush toolBrush)
        {
            AllToolBrushes.Add(toolBrush.Name, toolBrush);
            return toolBrush;
        }
    }
}
