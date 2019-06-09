using UnityEngine;
using System.Collections.Generic;

namespace TWF.Input
{
    /// <summary>
    /// A fully defined tool (behavior, modifier, brush).
    /// </summary>
    public class Tool : ITool
    {
        public Tool(string name, string toolBehaviorName, string toolBehaviorModifier, string toolBrushName, Color? previewColor)
        {
            Name = name;
            ToolBehaviorName = toolBehaviorName;
            ToolBehaviorModifier = toolBehaviorModifier;
            ToolBrushName = toolBrushName;
            PreviewColor = previewColor;
        }

        public string Name { get; }
        public string ToolBehaviorName { get; }

        public string ToolBehaviorModifier { get; }
        public string ToolBrushName { get; }

        public Color? PreviewColor { get; }

        public ToolPreviewOutcome Preview(IToolApplier toolApplier, ICollection<Vector> positions)
        {
            return toolApplier.PreviewTool(ToolBehaviorName, ToolBehaviorModifier, ToolBrushName, positions);
        }

        public ToolOutcome Apply(IToolApplier toolApplier, ICollection<Vector> positions)
        {
            return toolApplier.ApplyTool(ToolBehaviorName, ToolBehaviorModifier, ToolBrushName, positions);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
