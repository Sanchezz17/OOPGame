namespace Domain.Units
{
    public interface ITriggered
    {
        void Trigger(IStrike strike);
    }
}