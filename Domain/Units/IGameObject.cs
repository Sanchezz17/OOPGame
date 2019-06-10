using System.Windows;

namespace Domain.Units
{
    public interface IGameObject
    {
        int Health { get; }
        Vector Position { get; }
        State State { get; set; }
        bool IsDead { get;  }
    }
}