using System;
using System.Collections.Generic;

namespace TWF
{
    /// <summary>
    /// An interface to define the behavior of a tool.
    /// 
    /// Two main methods need to be defined: 
    /// * Preview, called to determine whether a tool usage with the specified input is valid or not compared to the current world.
    /// * CreateActions, called when the tool usage has been validated to enact the tool effect.
    /// 
    /// Preview can be called repeatively for a Tool usage to implement the preview mode as the player input changes.
    /// CreateActions will only be called once per Tool usage to implement the tool effect enactement.
    /// 
    /// CreateActions will only be called if the tool effect has been validated, therefore it should always success.
    /// The CreateActions and Validate call are called in a logical transaction on the world so there won't be concurrent modifications taking place.
    /// This is to considerate when implementing these methods as potential slowness would create a prolonged lock blocking other world mutations.
    /// 
    /// See Tool for more details.
    /// 
    /// Implementers should therefore not implement validation on CreateActions and use exceptions if for any reason the tool cannot be enacted 
    /// (ideally that reason should be implemented as check in the Preview method).
    /// 
    /// </summary>
    public interface IToolBehavior
    {
        string Name { get; }

        Action<World> CreateActions(IEnumerable<Vector> inputPositions);

        PreviewOutcome Preview(IWorldView worldView, IEnumerable<Vector> inputPositions);
    }
}
