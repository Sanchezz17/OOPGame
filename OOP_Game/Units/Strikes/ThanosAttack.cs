using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units.Strikes
{
    class ThanosAttack : Shot
    {
        public ThanosAttack(Vector position, Direction direction)
        {
            Health = 1;
            Position = position;
            Direction = direction;
            State = State.Moves;
        }

        public override int ToDamage(UnitParameters parameters)
        {           
            Health -= 1;
            if (Health <= 0)
                IsDead = true;
            var enemyHeath = parameters.Health;
            if (enemyHeath <= 100)
                return enemyHeath;
            else
                return enemyHeath / 2;
        }
    }
}
