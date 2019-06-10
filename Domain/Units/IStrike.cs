namespace Domain.Units
{
    public interface IStrike : IGameObject
    {
        int ToDamage(UnitParameters parametres);
    }
}