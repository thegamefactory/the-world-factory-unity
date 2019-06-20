namespace TWF
{
    using System.Collections.Generic;

    public class CombinedZoneDevelopmentVoter : IZoneDevelopmentVoter
    {
        private readonly LinkedList<IZoneDevelopmentVoter> voters;

        public CombinedZoneDevelopmentVoter(params IZoneDevelopmentVoter[] developmentVoters)
        {
            this.voters = new LinkedList<IZoneDevelopmentVoter>(developmentVoters);
        }

        public int VotersCount => this.voters.Count;

        public void OnNewWorld(IWorldView worldView)
        {
            foreach (var voter in this.voters)
            {
                voter.OnNewWorld(worldView);
            }
        }

        public void RegisterVoter(IZoneDevelopmentVoter voter)
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
