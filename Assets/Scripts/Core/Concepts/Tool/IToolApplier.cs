using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// An object that can receive tool instructions and apply them to modify the state of the world.
    /// The tool offers a dry run mode (preview) that can be used to generate feedback on the effect of the action before enacting it.
    /// </summary>
    public interface IToolApplier
    {
        /// <summary>
        /// Applies the given tool behavior and associated modifier to the given positions processed with the brush type.
        /// </summary>
        ToolOutcome ApplyTool(string toolBehaviorName, string toolBehaviorModifier, string toolBrushName, IEnumerable<Vector> positions);

        /// <summary>
        /// Previews the application of the given behavior and associated modifier to the given positions processed with the brush type.
        /// </summary>
        ToolPreviewOutcome PreviewTool(string toolBehaviorName, string toolBehaviorModifier, string toolBrushName, IEnumerable<Vector> positions);
    }
}
