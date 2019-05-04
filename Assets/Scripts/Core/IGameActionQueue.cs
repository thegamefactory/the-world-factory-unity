using System;
namespace TWF
{
    public interface IGameActionQueue
    {
        void ExecuteSynchronous(Action<GameService> action);
    }
}
