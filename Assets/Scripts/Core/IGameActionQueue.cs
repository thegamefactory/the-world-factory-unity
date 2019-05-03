using System;
namespace TWF
{
    public interface IGameActionQueue
    {
        void executeSynchronous(Action<GameService> action);
    }
}
