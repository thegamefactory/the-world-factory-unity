namespace TWF
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    public class CombinedTileDevelopmentVoter : ITileDevelopmentVoter
    {
        private readonly LinkedList<ITileDevelopmentVoter> voters;

        public CombinedTileDevelopmentVoter(params ITileDevelopmentVoter[] developmentVoters)
        {
            this.voters = new LinkedList<ITileDevelopmentVoter>(developmentVoters);
        }

        public int VotersCount => this.voters.Count;

        public void OnNewWorld(IWorldView worldView)
        {
            foreach (var voter in this.voters)
            {
                voter.OnNewWorld(worldView);
            }
        }

        public void RegisterVoters(params ITileDevelopmentVoter[] voters)
        {
            Contract.Requires(voters != null);

            foreach (var v in voters)
            {
                this.voters.AddLast(v);
            }
        }

        public double Vote(Vector pos, int buildingModel)
        {
            if (this.voters.Count == 0)
            {
                return 0;
            }
            else
            {
                double result = 1.0;
                foreach (var voter in this.voters)
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
