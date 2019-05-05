using System.Collections.Generic;
using TWF.Map;

namespace TWF.Tool
{
    /// <summary>
    /// Encapsulates the information related to whether using a tool is possible.
    /// 
    /// This class conveys which positions, if any, are problematic and which are not.
    /// </summary>
    public class PreviewOutcome
    {
        Dictionary<ToolOutcome, List<Vector>> OutcomeToPositionDictionary;

        public PreviewOutcome(Dictionary<ToolOutcome, List<Vector>> outcomeToPositionDictionary)
        {
            this.OutcomeToPositionDictionary = outcomeToPositionDictionary;
        }

        public class Builder
        {
            Dictionary<ToolOutcome, List<Vector>> OutcomeToPositionDictionary;

            public Builder()
            {
                this.OutcomeToPositionDictionary = new Dictionary<ToolOutcome, List<Vector>>
                {
                    { ToolOutcome.SUCCESS, new List<Vector>() },
                    { ToolOutcome.FAILURE, new List<Vector>() }
                };
            }

            public Builder WithPositionOutcome(Vector position, ToolOutcome outcome)
            {
                OutcomeToPositionDictionary[outcome].Add(position);
                return this;
            }

            public Builder WithOutcomePositions(ToolOutcome outcome, List<Vector> positions)
            {
                OutcomeToPositionDictionary[outcome] = positions;
                return this;
            }

            public PreviewOutcome Build()
            {
                return new PreviewOutcome(OutcomeToPositionDictionary);
            }
        }

        public static Builder builder()
        {
            return new Builder();
        }

        /// <summary>
        /// If using the tool is possible i.e. if all the positions can be successfully mutated.
        /// </summary>
        /// <returns><c>true</c>, if using tool is possible, <c>false</c> otherwise.</returns>
        public bool IsPossible()
        {
            return OutcomeToPositionDictionary[ToolOutcome.FAILURE].Count == 0;
        }

        /// <summary>
        /// Gets the failed positions.
        /// </summary>
        /// <returns>The failed positions.</returns>
        public List<Vector> GetFailedPositions()
        {
            return OutcomeToPositionDictionary[ToolOutcome.FAILURE];
        }

        /// <summary>
        /// Gets the successful positions.
        /// </summary>
        /// <returns>The successful positions.</returns>
        public List<Vector> GetSuccessfulPositions()
        {
            return OutcomeToPositionDictionary[ToolOutcome.SUCCESS];
        }
    }
}