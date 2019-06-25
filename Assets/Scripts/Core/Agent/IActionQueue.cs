namespace TWF
{
    using System;

    /// <summary>
    /// Interface for objects that can process actions on a game state.
    /// </summary>
    public interface IActionQueue
    {
        void ExecuteSynchronously(Action<World> action);
    }
}
