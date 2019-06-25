namespace TWF
{
    using System.Collections.Generic;

    /// <summary>
    /// Encapsulates the information related to whether using a tool is possible.
    ///
    /// This class conveys which positions, if any, are problematic and which are not.
    /// </summary>
    public class ToolPreviewOutcome : IToolPreviewOutcomeMap
    {
        public static readonly ToolPreviewOutcome EMPTY = ToolPreviewOutcome.Builder().Build();

        private readonly Dictionary<Vector, ToolOutcome> outcomes;
        private readonly bool isPossible;

        public ToolPreviewOutcome(Dictionary<Vector, ToolOutcome> outcomes, bool isPossible)
        {
            this.outcomes = outcomes;
            this.isPossible = isPossible;
        }

        public static PreviewOutcomeBuilder Builder()
        {
            return new PreviewOutcomeBuilder();
        }

        /// <summary>
        /// If using the tool is possible i.e. if all the positions can be successfully mutated.
        /// </summary>
        /// <returns><c>true</c>, if using tool is possible, <c>false</c> otherwise.</returns>
        public bool IsPossible()
        {
            return this.isPossible;
        }

        public ToolOutcome? GetPreview(Vector pos)
        {
            if (this.outcomes.ContainsKey(pos))
            {
                return this.outcomes[pos];
            }
            else
            {
                return null;
            }
        }
    }
}