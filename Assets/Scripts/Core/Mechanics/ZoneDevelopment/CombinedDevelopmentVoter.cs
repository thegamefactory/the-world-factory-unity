namespace TWF
{
    using System.Collections.Generic;

    public class CombinedDevelopmentVoter : IDevelopmentVoter
    {
        private readonly LinkedList<IDevelopmentVoter> voters;

        public CombinedDevelopmentVoter(params IDevelopmentVoter[] developmentVoters)
        {
            this.voters = new LinkedList<IDevelopmentVoter>(developmentVoters);
        }

        public int VotersCount => this.voters.Count;

        public void OnNewWorld(IWorldView worldView)
        {
            foreach (var voter in this.voters)
            {
                voter.OnNewWorld(worldView);
            }
        }

        public void RegisterVoter(IDevelopmentVoter voter)
        {
            this.voters.AddLast(voter);
        }

        public double Vote(Vector pos)
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
                    result *= voter.Vote(pos);
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
