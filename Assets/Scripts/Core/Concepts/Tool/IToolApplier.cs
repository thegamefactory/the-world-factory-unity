namespace TWF
{
    using System.Collections.Generic;

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

        /// <summary>
        /// Add a position to the linked list of the current positions.
        /// This enable optimizations, for example a rectangle brush may only care about the first position and the last position.
        /// Therefore adding a position can simply replace the last element of the linked list and the linked list size can be bound to two.
        /// The positions parameter is modified in place
        /// Usage of this method is optional.
        /// </summary>
        void AddPosition(string toolBrushName, LinkedList<Vector> positions, Vector position);
    }
}
