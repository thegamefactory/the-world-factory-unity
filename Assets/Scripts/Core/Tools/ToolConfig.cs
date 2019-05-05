using System.Collections.Generic;
namespace TWF.Tool
{
    public class ToolConfig
    {
        IDictionary<ToolBehaviorType, ITool> tools;
        IDictionary<ToolBrushType, IToolBrush> toolBrushes;

        public ToolConfig(IDictionary<ToolBehaviorType, ITool> tools, IDictionary<ToolBrushType, IToolBrush> toolBrushes)
        {
            this.tools = tools;
            this.toolBrushes = toolBrushes;
        }

        public ITool GetTool(ToolBehaviorType toolBehaviorType)
        {
            return tools[toolBehaviorType];
        }

        public IToolBrush GetToolBrush(ToolBrushType toolBrushType)
        {
            return toolBrushes[toolBrushType];
        }
    }
}
