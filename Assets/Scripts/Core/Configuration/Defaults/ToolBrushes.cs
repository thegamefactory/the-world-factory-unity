namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    public static class ToolBrushes
    {
        public static readonly IToolBrush Pen = new PenToolBrush();
        public static readonly IToolBrush Manatthan = new ManatthanToolBrush();
        public static readonly IToolBrush Rectangle = new RectangleToolBrush();

        public static void RegisterDefaults(WorldRules worldRules)
        {
            Contract.Requires(worldRules != null);

            worldRules.ToolBrushes[Pen.Name] = Pen;
            worldRules.ToolBrushes[Manatthan.Name] = Manatthan;
            worldRules.ToolBrushes[Rectangle.Name] = Rectangle;
        }
    }
}
