namespace TWF
{
    using System.Collections.Generic;

    /// <summary>
    /// A development voter is an agent that is consulted to decide if a tile should be developed (i.e if a building should be installed on the tile).
    /// It votes a number between 0 (the tile should not be developed) and 1 (the tile should be developed).
    /// </summary>
    public interface ITileDevelopmentVoter
    {
        void OnNewWorld(IWorldView worldView);

        double Vote(Vector pos, int buildingModel);
    }
}
