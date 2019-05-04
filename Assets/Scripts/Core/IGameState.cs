using TWF.Map;

namespace TWF
{
    public interface IGameState : IImmutableTileMap
    {
        IEntity GetEntity(int x, int y);
    }
}
