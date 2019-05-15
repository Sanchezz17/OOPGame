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
            var enemyHealth = (int)parameters.Health;
            if (enemyHealth <= 100)
                return enemyHealth;
            return enemyHealth / 2;
        }
    }
}
