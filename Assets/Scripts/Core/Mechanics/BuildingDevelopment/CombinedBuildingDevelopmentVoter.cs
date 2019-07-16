namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A voter that dispatches requests to a list of child voters and aggregate the results.
    /// </summary>
    public class CombinedBuildingDevelopmentVoter : IBuildingDevelopmentVoter
    {
        private readonly LinkedList<IBuildingDevelopmentVoter> childVoters;

        public CombinedBuildingDevelopmentVoter(params IBuildingDevelopmentVoter[] childVoters)
        {
            this.childVoters = new LinkedList<IBuildingDevelopmentVoter>(childVoters);
        }

        public int VotersCount => this.childVoters.Count;

        public void OnNewWorld(IWorldView worldView)
        {
            foreach (var voter in this.childVoters)
            {
                voter.OnNewWorld(worldView);
            }
        }

        public void RegisterVoters(params IBuildingDevelopmentVoter[] voters)
        {
            Contract.Requires(voters != null);

            foreach (var v in voters)
            {
                this.childVoters.AddLast(v);
            }
        }

        public double Vote(Vector pos, int buildingModel)
        {
            if (this.childVoters.Count == 0)
            {
                return 0;
            }
            else
            {
                double result = 1.0;
                foreach (var voter in this.childVoters)
                {
                    result *= voter.Vote(pos, buildingModel);
                    if (result == 0)
                    {
                        break;
                    }
                }

                return result;
            }
        }
    }
}
