using System;

namespace TWF.Agent
{
    /// <summary>
    /// An agent mutates the state of the game.
    /// 
    /// The agent is triggered by the GameService. 
    /// Instead of directly mutating the state of the game, the agent returns a list of actions.
    /// The actions are then processed by the service.
    /// </summary>
    public interface IAgent
    {
        String Name { get; }

        /// <summary>
        /// Executes the agent, returns a list of actions to be processed.
        /// 
        /// The agents execution are designed to be parallelized and can be expensive.
        /// The agents executions are read-only.
        /// 
        /// The resulting actions are designed to be serialized and should be cheap.
        /// The resulting actions mutate the game state.
        /// 
        /// </summary>
        /// <returns>Mutating actions to execute on the game.</returns>
        /// <param name="gameState">The game state.</param>
        Action<GameService> execute(IGameState gameState);
    }
}
