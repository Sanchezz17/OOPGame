using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units.Strikes
{
    class ThanosAttack : Shot
    {
        public ThanosAttack(Vector position, Direction direction) : base(direction, 1, position) {}

        public override int ToDamage(UnitParameters parameters)
        {           
            Health -= 1;
            if (Health <= 0)
                IsDead = true;
            var enemyHeath = parameters.Health;
            if (enemyHeath <= 100)
                return enemyHeath;
            return enemyHeath / 2;
        }
    }
}
