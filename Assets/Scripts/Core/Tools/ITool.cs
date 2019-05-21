using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// An interface to define objects that mutate the state of the world based on the player actions.
    /// The tool offers a dry run mode (preview) that can be used to generate feedback on the effect of the action before enacting it.
    /// 
    /// This interface is implemented by Tool. The approach to implement a new tool is to create a new IToolBehavior implementation.
    /// </summary>
    public interface ITool
    {
        ToolOutcome Apply(IActionQueue actionQueue, Modifier modifier, IEnumerable<Vector> inputPositions, IToolBrush toolBrush);
        PreviewOutcome Preview(IWorldView worldView, Modifier modifier, IEnumerable<Vector> inputPositions, IToolBrush toolBrush);
    }
}
