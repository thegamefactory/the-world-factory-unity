namespace TWF
{
    using System;

    /// <summary>
    /// An agent mutates the state of the world.
    ///
    /// The agent is triggered periodically by a ticker.
    /// Instead of directly mutating the state of the world, the agent returns a list of actions.
    /// The actions are then processed by the service.
    /// </summary>
    public interface IAgent
    {
        string Name { get; }

        /// <summary>
        /// Executes the agent, returns a list of actions to be processed.
        ///
        /// The agents execution are designed to be parallelized and can be expensive.
        /// The agents executions are read-only.
        ///
        /// The resulting actions are designed to be serialized and should be cheap.
        /// The resulting actions mutate the world.
        ///
        /// </summary>
        /// <returns>Mutating actions to execute on the world.</returns>
        /// <param name="worldView">The world (read-only).</param>
        Action<World> Execute(IWorldView worldView);
    }
}
