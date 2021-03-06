namespace TWF
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An object that can receive tool instructions and apply them to modify the state of the world.
    /// The tool offers a dry run mode (preview) that can be used to generate feedback on the effect of the action before enacting it.
    /// </summary>
    public class ToolApplier : IToolApplier
    {
        private readonly World world;

        public ToolApplier(World world)
        {
            this.world = world;
        }

        public ToolOutcome ApplyTool(string toolBehaviorName, string modifier, string toolBrushName, IEnumerable<Vector> positions)
        {
            return this.Apply(this.world.GetActionQueue(), this.GetToolBehavior(toolBehaviorName, modifier), this.GetToolBrush(toolBrushName), positions);
        }

        public ToolPreviewOutcome PreviewTool(string toolBehaviorName, string modifier, string toolBrushName, IEnumerable<Vector> positions)
        {
            return this.Preview(this.world, this.GetToolBehavior(toolBehaviorName, modifier), this.GetToolBrush(toolBrushName), positions);
        }

        public void AddPosition(string toolBrushName, LinkedList<Vector> positions, Vector position)
        {
            this.GetToolBrush(toolBrushName).AddPosition(positions, position);
        }

        private static bool Validate(IWorldView worldView, IToolBehavior toolBehavior, IEnumerable<Vector> toolPositions)
        {
            return toolBehavior.Preview(worldView, toolPositions).IsPossible();
        }

        private IToolBehavior GetToolBehavior(string toolBehaviorName, string modifier)
        {
            var toolBehaviorProvider = this.world.Rules.ToolBehaviors[toolBehaviorName] ?? throw new ArgumentException("Invalid tool behavior: " + toolBehaviorName);
            return toolBehaviorProvider(modifier);
        }

        private IToolBrush GetToolBrush(string toolBrushName)
        {
            return this.world.Rules.ToolBrushes[toolBrushName] ?? throw new ArgumentException("Invalid tool brush: " + toolBrushName);
        }

        private ToolPreviewOutcome Preview(IWorldView worldView, IToolBehavior toolBehavior, IToolBrush toolBrush, IEnumerable<Vector> inputPositions)
        {
            IEnumerable<Vector> toolPositions = toolBrush.ComputePositions(inputPositions);
            return toolBehavior.Preview(worldView, toolPositions);
        }

        private ToolOutcome Apply(IActionQueue actionQueue, IToolBehavior toolBehavior, IToolBrush toolBrush, IEnumerable<Vector> inputPositions)
        {
            IEnumerable<Vector> toolPositions = toolBrush.ComputePositions(inputPositions);
            var action = toolBehavior.CreateActions(toolPositions);
            void ValidatedAction(World gs)
            {
                if (Validate(gs, toolBehavior, toolPositions))
                {
                    action(gs);
                }
                else
                {
                    throw new InvalidOperationException("Action is not valid");
                }
            }

            // TODO: don't use exception to control the flow. Need google for this.
            try
            {
                actionQueue.ExecuteSynchronously(ValidatedAction);
                return ToolOutcome.Success;
            }
            catch (InvalidOperationException)
            {
                return ToolOutcome.Failure;
            }
        }

        private IEnumerable<Vector> ConvertPositions(IEnumerable<Vector> input, IToolBrush toolBrush)
        {
            if (toolBrush.IsValid(input))
            {
                return toolBrush.ComputePositions(input);
            }
            else
            {
                return null;
            }
        }
    }
}
