using System;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Tool
{
    public interface IToolBehavior
    {
        ToolBehaviorType ToolBehaviorType { get; }

        Action<GameService> CreateActions(LinkedList<Vector> inputPositions, Modifier modifier);

        ToolOutcome Validate(IGameState gameState, LinkedList<Vector> inputPositions, Modifier modifier);
    }
}
