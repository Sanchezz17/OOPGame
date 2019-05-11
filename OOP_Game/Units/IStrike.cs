using OOP_Game.Infrastructure;

namespace OOP_Game.Units
{
    public interface IStrike : IGameObject
    {
        int ToDamage(UnitParameters parametres);
    }
}