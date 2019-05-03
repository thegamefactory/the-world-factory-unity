using System;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Tool
{
    public interface ITool
    {
        ToolOutcome Apply(IGameActionQueue gameActionQueue, LinkedList<Vector> inputPositions, Modifier modifier);
        ToolOutcome Preview(IGameState gameState, LinkedList<Vector> inputPositions, Modifier modifier);
    }
}
