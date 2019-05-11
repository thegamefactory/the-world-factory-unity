using System;
using System.Collections.Generic;
using TWF.State;

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

        public PreviewOutcome Preview(IGameStateView gameState, Modifier modifier, IEnumerable<Vector> inputPositions, IToolBrush toolBrush)
        {
            IEnumerable<Vector> toolPositions = toolBrush.computePositions(inputPositions);
            return toolBehavior.Preview(gameState, modifier, inputPositions);
        }

        private bool Validate(IGameStateView gameState, Modifier modifier, IEnumerable<Vector> toolPositions)
        {
            return toolBehavior.Preview(gameState, modifier, toolPositions).IsPossible();
        }

        public ToolOutcome Apply(IGameActionQueue gameActionQueue, Modifier modifier, IEnumerable<Vector> inputPositions, IToolBrush toolBrush)
        {
            IEnumerable<Vector> toolPositions = toolBrush.computePositions(inputPositions);
            var action = toolBehavior.CreateActions(modifier, toolPositions);
            Action<GameState> validatedAction = (gs) =>
            {
                if (Validate(gs, modifier, toolPositions))
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
                gameActionQueue.ExecuteSynchronously(validatedAction);
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
