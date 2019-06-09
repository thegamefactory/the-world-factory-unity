namespace TWF.Input
{
    using System.Collections.Generic;
    using UnityEngine;

    public interface ITool
    {
        string ToolBrushName { get; }

        Color? PreviewColor { get; } // this seems out of place

        ToolPreviewOutcome Preview(IToolApplier toolApplier, ICollection<Vector> positions);

        ToolOutcome Apply(IToolApplier toolApplier, ICollection<Vector> positions);
    }
}
