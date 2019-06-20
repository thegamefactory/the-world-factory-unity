namespace TWF
{
    /// <summary>
    /// A development voter is an agent that is consulted to decide if a zone should be developed (i.e if a building should be installed on the zone).
    /// It votes a number between 0 (the zone should not be developed) and 1 (the zone should be developed).
    /// </summary>
    public interface IZoneDevelopmentVoter
    {
        void OnNewWorld(IWorldView worldView);

        double Vote(Vector pos);
    }
}
