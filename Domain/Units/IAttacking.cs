namespace Domain.Units
{
    public interface IAttacking
    {
        IStrike Attack();
        bool IsAttackAvailable();
    }
}