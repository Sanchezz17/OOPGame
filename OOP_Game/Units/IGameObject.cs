using System.Drawing;

namespace OOP_Game.Units
{
    public interface IGameObject
    {
        int Health { get; set; }
        Point Position { get; set; }
        State State { get; set; }
    }
}