namespace Domain.Units
{
    public interface IGemManufacturer : IGameObject
    {
        bool IsAvailableGem();
        Gem GetGem();
    }
}