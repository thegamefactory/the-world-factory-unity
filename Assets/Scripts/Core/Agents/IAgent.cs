using System;

namespace TWF.Agent
{
    public interface IAgent
    {
        Action<GameService> execute();
    }
}
