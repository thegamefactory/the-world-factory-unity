namespace TWF
{
    using System.Collections.Generic;

    public class PreviewOutcomeBuilder
    {
        private readonly Dictionary<Vector, ToolOutcome> outcomes = new Dictionary<Vector, ToolOutcome>();
        private bool isPossible = true;

        public PreviewOutcomeBuilder WithPositionOutcome(Vector position, ToolOutcome outcome)
        {
            this.outcomes[position] = outcome;
            this.isPossible &= outcome != ToolOutcome.Failure;
            return this;
        }

        public ToolPreviewOutcome Build()
        {
            return new ToolPreviewOutcome(this.outcomes, this.isPossible);
        }
    }
}