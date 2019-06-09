namespace TWF.Input
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using UnityEngine;

    /// <summary>
    /// A fully defined tool (behavior, modifier, brush).
    /// </summary>
    public class Tool : ITool
    {
        public Tool(string name, string toolBehaviorName, string toolBehaviorModifier, string toolBrushName, Color? previewColor)
        {
            this.Name = name;
            this.ToolBehaviorName = toolBehaviorName;
            this.ToolBehaviorModifier = toolBehaviorModifier;
            this.ToolBrushName = toolBrushName;
            this.PreviewColor = previewColor;
        }

        public string Name { get; }

        public string ToolBehaviorName { get; }

        public string ToolBehaviorModifier { get; }

        public string ToolBrushName { get; }

        public Color? PreviewColor { get; }

        public ToolPreviewOutcome Preview(IToolApplier toolApplier, ICollection<Vector> positions)
        {
            Contract.Requires(toolApplier != null);
            return toolApplier.PreviewTool(this.ToolBehaviorName, this.ToolBehaviorModifier, this.ToolBrushName, positions);
        }

        public ToolOutcome Apply(IToolApplier toolApplier, ICollection<Vector> positions)
        {
            Contract.Requires(toolApplier != null);
            return toolApplier.ApplyTool(this.ToolBehaviorName, this.ToolBehaviorModifier, this.ToolBrushName, positions);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
