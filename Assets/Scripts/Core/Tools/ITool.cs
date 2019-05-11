using System.Collections.Generic;
using TWF.State;

namespace TWF.Tool
{
    /// <summary>
    /// An interface to define objects that mutate the state of the game based on the player actions.
    /// The tool offers a dry run mode (preview) that can be used to generate feedback on the effect of the action before enacting it.
    /// 
    /// This interface is implemented by Tool. The approach to implement a new tool is to create a new IToolBehavior implementation.
    /// </summary>
    public interface ITool
    {
        ToolOutcome Apply(IGameActionQueue gameActionQueue, Modifier modifier, IEnumerable<Vector> inputPositions, IToolBrush toolBrush);
        PreviewOutcome Preview(IGameStateView gameState, Modifier modifier, IEnumerable<Vector> inputPositions, IToolBrush toolBrush);
    }
}
