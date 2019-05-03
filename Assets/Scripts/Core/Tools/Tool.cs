using System;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Tool
{
    public class Tool : ITool
    {
        private IToolBehavior toolBehavior;

        public Tool(IToolBehavior toolBehavior)
        {
            this.toolBehavior = toolBehavior;
        }

        ToolBehaviorType ToolBehaviorType
        {
            get { return toolBehavior.ToolBehaviorType; }
        }

        public ToolOutcome Preview(IGameState gameState, LinkedList<Vector> inputPositions, Modifier modifier)
        {
            return toolBehavior.Validate(gameState, inputPositions, modifier);
        }

        public ToolOutcome Apply(IGameActionQueue gameActionQueue, LinkedList<Vector> inputPositions, Modifier modifier)
        {
            var action = toolBehavior.CreateActions(inputPositions, modifier);
            Action<GameService> validatedAction = (gs) =>
            {
                if (Preview(gs, inputPositions, modifier) == ToolOutcome.SUCCESS)
                {
                    action(gs);
                }
                else
                {
                    throw new InvalidOperationException("Action is not valid");
                }
            };

            // TODO: don't use exception to control the flow. Need google for this.
            try
            {
                gameActionQueue.executeSynchronous(validatedAction);
                return ToolOutcome.SUCCESS;
            }
            catch (InvalidOperationException)
            {
                return ToolOutcome.FAILURE;
            }
        }
    }
}
