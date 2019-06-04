using System.Collections.Generic;

namespace TWF.Input
{
    /// <summary>
    /// An identifier for a user action that can be triggered by a key combination.
    /// </summary>
    public class Tool
    {
        public Tool(string name, string toolBehaviorName, string toolBehaviorModifier, string toolBrushName)
        {
            Name = name;
            ToolBehaviorName = toolBehaviorName;
            ToolBehaviorModifier = toolBehaviorModifier;
            ToolBrushName = toolBrushName;
        }

        public string Name { get; }
        public string ToolBehaviorName { get; }

        public string ToolBehaviorModifier { get; }
        public string ToolBrushName { get; }

        public PreviewOutcome Preview(IToolApplier toolApplier, ICollection<Vector> positions)
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
