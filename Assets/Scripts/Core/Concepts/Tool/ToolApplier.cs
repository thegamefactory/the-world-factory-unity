using System;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// An class to mutate the state of the world based on the player actions.
    /// The tool offers a dry run mode (preview) that can be used to generate feedback on the effect of the action before enacting it.
    /// 
    /// This interface is implemented by Tool. The approach to implement a new tool is to create a new IToolBehavior implementation.
    /// </summary>
    public class ToolApplier : IToolApplier
    {
        private World world;

        public ToolApplier(World world)
        {
            this.world = world;
        }

        public ToolOutcome ApplyTool(string toolBehaviorName, string modifier, string toolBrushName, IEnumerable<Vector> positions)
        {
            IToolBehavior toolBehavior = GetToolBehavior(toolBehaviorName, modifier);
            IToolBrush toolBrush = world.ToolBrushes[toolBrushName] ?? throw new ArgumentException("Invalid tool brush: " + toolBrushName);
            return Apply(world.GetActionQueue(), toolBehavior, toolBrush, positions);
        }

        public PreviewOutcome PreviewTool(string toolBehaviorName, string modifier, string toolBrushName, IEnumerable<Vector> positions)
        {
            IToolBehavior toolBehavior = GetToolBehavior(toolBehaviorName, modifier);
            IToolBrush toolBrush = world.ToolBrushes[toolBrushName] ?? throw new ArgumentException("Invalid tool brush: " + toolBrushName);
            return Preview(world, toolBehavior, toolBrush, positions);
        }

        private IToolBehavior GetToolBehavior(string toolBehaviorName, string modifier)
        {
            var toolBehaviorProvider = world.ToolBehaviors[toolBehaviorName] ?? throw new ArgumentException("Invalid tool behavior: " + toolBehaviorName);
            return toolBehaviorProvider(modifier);
        }

        private PreviewOutcome Preview(IWorldView worldView, IToolBehavior toolBehavior, IToolBrush toolBrush, IEnumerable<Vector> inputPositions)
        {
            IEnumerable<Vector> toolPositions = toolBrush.computePositions(inputPositions);
            return toolBehavior.Preview(worldView, inputPositions);
        }

        private bool Validate(IWorldView worldView, IToolBehavior toolBehavior, IEnumerable<Vector> toolPositions)
        {
            return toolBehavior.Preview(worldView, toolPositions).IsPossible();
        }

        private ToolOutcome Apply(IActionQueue actionQueue, IToolBehavior toolBehavior, IToolBrush toolBrush, IEnumerable<Vector> inputPositions)
        {
            IEnumerable<Vector> toolPositions = toolBrush.computePositions(inputPositions);
            var action = toolBehavior.CreateActions(toolPositions);
            Action<World> validatedAction = (gs) =>
            {
                if (Validate(gs, toolBehavior, toolPositions))
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
                actionQueue.ExecuteSynchronously(validatedAction);
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