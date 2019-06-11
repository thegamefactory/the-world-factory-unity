namespace TWF
{
    using System.Collections.Generic;

    public static class ToolBrushes
    {
        public static readonly IToolBrush Pen = RegisterToolBrush(new PenToolBrush());
        public static readonly IToolBrush Manatthan = RegisterToolBrush(new ManatthanToolBrush());
        public static readonly IToolBrush Rectangle = RegisterToolBrush(new RectangleToolBrush());

        public static Dictionary<string, IToolBrush> AllToolBrushes
        {
            get
            {
                if (null == allToolBrushes)
                {
                    allToolBrushes = new Dictionary<string, IToolBrush>();
                }
                return allToolBrushes;
            }
        }

        private static Dictionary<string, IToolBrush> allToolBrushes;

        private static IToolBrush RegisterToolBrush(IToolBrush toolBrush)
        {
            AllToolBrushes.Add(toolBrush.Name, toolBrush);
            return toolBrush;
        }
    }
}
