using OOP_Game.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOP_Game.Infrastructure
{
    public class UnitParameters
    {

        public int Health { get; private set; }
        public Vector Position { get; private set; }
        public State State { get; private set; }
        public bool IsDead { get; private set; }
        public int baseRechargeTimeInTicks { get; private set; }
        public Direction Direction { get; private set; }

        public UnitParameters(int health, Vector position, State state, bool isDead, int baseRechargeTimeInTicks)
        {
            Health = health;
            Position = position;
            State = state;
            IsDead = isDead;
            this.baseRechargeTimeInTicks = baseRechargeTimeInTicks;
        }
    }
}
