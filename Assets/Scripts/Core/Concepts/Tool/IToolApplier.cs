using System.Collections.Generic;

namespace TWF
{
    public interface IToolApplier
    {
        /// <summary>
        /// Applies the given toolType and associated modifier to the given positions processed with the brush type.
        /// </summary>
        ToolOutcome ApplyTool(string toolBehaviorName, string toolBehaviorModifier, string toolBrushName, IEnumerable<Vector> positions);

        /// <summary>
        /// Previews the application of the given toolType and associated modifier to the given positions processed with the brush type.
        /// </summary>
        PreviewOutcome PreviewTool(string toolBehaviorName, string toolBehaviorModifier, string toolBrushName, IEnumerable<Vector> positions);
    }
}
