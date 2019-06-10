using Domain.GameLogic;

namespace OOP_Game.GameLogic
{
    public interface IGameFactory
    {
        Game Create(Player player);
    }
}