using System;
using System.Collections.Generic;
using TWF.Map;

namespace TWF.Tool
{
    /// <summary>
    /// An interface to define the behavior of a tool.
    /// 
    /// Two main methods need to be defined: 
    /// * Validate, called to determine whether a tool usage with the specified input is valid or not compared to the current game state.
    /// * CreateActions, called when the tool usage has been validated to enact the tool effect.
    /// 
    /// Validate can be called repeatively for a Tool usage to implement the preview mode as the player input changes.
    /// CreateActions will only be called once per Tool usage to implement the tool effect enactement.
    /// 
    /// CreateActions will only be called if the tool effect has been validated, therefore it should always success.
    /// The CreateActions and Validate call are called in a logical transaction on the game state so there won't be concurrent modifications taking place.
    /// This is to considerate when implementing these methods as potential slowness would create a prolonged lock blocking other game mutations.
    /// 
    /// See Tool for more details.
    /// 
    /// Implementers should therefore not implement validation on CreateActions and use exceptions if for any reason the tool cannot be enacted 
    /// (ideally that reason should be implemented as check in the Validate method).
    /// 
    /// </summary>
    public interface IToolBehavior
    {
        ToolBehaviorType ToolBehaviorType { get; }

        Action<GameService> CreateActions(IList<Vector> inputPositions, Modifier modifier);

        ToolOutcome Validate(IGameState gameState, IList<Vector> inputPositions, Modifier modifier);
    }
}
