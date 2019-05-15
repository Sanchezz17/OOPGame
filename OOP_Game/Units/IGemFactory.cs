namespace OOP_Game.Units
{
    public interface IGemManufacturer : IGameObject
    {
        bool IsAvailableGem();
        Gem GetGem();
    }
}