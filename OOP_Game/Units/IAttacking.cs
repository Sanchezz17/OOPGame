namespace OOP_Game.Units
{
    public interface IAttacking
    {
        IStrike Attack();
        bool IsAttackAvailable();
    }
}