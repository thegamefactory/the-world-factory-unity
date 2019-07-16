namespace TWF
{
    /// <summary>
    /// A development voter is an agent that is consulted to decide if a building should be developed on the tile.
    /// It votes a number between 0 (the tile should not be developed) and 1 (the tile should be developed).
    /// Note that it can be consulted for installing new buildings (constructor) or abandoning existing buildings.
    /// </summary>
    public interface IBuildingDevelopmentVoter
    {
        void OnNewWorld(IWorldView worldView);

        double Vote(Vector pos, int buildingModel);
    }
}
