using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units.Strikes
{
    class IronManAttack : Shot
    {
        public IronManAttack(int damage, Vector position, Direction direction)
        {
            Health = 1;
            Damage = damage;
            Position = position;
            Direction = direction;
            State = State.Moves;
        }

    }
}
