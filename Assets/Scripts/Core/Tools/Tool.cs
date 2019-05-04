using System;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Tool
{
    /// <summary>
    /// The default implementation for Tool.
    /// 
    /// Wraps the IToolBehavior interface which is designed to separate logical concerns of tool validation and tool enactment 
    /// from the ITool interface which is designed to be presented to the player.
    /// 
    /// Note how the Apply method creates a GameService Action which encapsulate validation and enactement.
    /// 
    /// </summary>
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

        public ToolOutcome Preview(IGameState gameState, Modifier modifier, IEnumerable<Vector> inputPositions, IToolBrush toolBrush)
        {
            IEnumerable<Vector> toolPositions = toolBrush.computePositions(inputPositions);
            return toolBehavior.Validate(gameState, modifier, inputPositions);
        }

        private ToolOutcome Validate(IGameState gameState, Modifier modifier, IEnumerable<Vector> toolPositions)
        {
            return toolBehavior.Validate(gameState, modifier, toolPositions);
        }

        public ToolOutcome Apply(IGameActionQueue gameActionQueue, Modifier modifier, IEnumerable<Vector> inputPositions, IToolBrush toolBrush)
        {
            IEnumerable<Vector> toolPositions = toolBrush.computePositions(inputPositions);
            var action = toolBehavior.CreateActions(modifier, toolPositions);
            Action<GameService> validatedAction = (gs) =>
            {
                if (Validate(gs, modifier, toolPositions) == ToolOutcome.SUCCESS)
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
                gameActionQueue.ExecuteSynchronous(validatedAction);
                return ToolOutcome.SUCCESS;
            }
            catch (InvalidOperationException)
            {
                return ToolOutcome.FAILURE;
            }
        }

        private IEnumerable<Vector> ConvertPositions(IEnumerable<Vector> input, IToolBrush toolBrush)
        {
            if (toolBrush.IsValid(input))
            {
                return toolBrush.computePositions(input);
            }
            else
            {
                return null;
            }
        }
    }
}
