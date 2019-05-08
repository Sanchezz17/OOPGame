namespace OOP_Game.Units
{
    public interface IAttacking
    {
        IStrike Attack();
        int RechargeTimeInTicks { get; set; }
    }
}