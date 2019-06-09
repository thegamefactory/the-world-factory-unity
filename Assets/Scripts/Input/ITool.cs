using System.Collections.Generic;
using UnityEngine;

namespace TWF.Input
{
    public interface ITool
    {
        ToolPreviewOutcome Preview(IToolApplier toolApplier, ICollection<Vector> positions);
        ToolOutcome Apply(IToolApplier toolApplier, ICollection<Vector> positions);
        Color? PreviewColor { get; } // this seems out of place
    }
}
