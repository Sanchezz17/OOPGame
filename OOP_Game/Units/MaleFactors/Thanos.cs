﻿using System.Windows;
using OOP_Game.Infrastructure;
using OOP_Game.Units.Strikes;

namespace OOP_Game.Units
{
    namespace OOP_Game.Units.MaleFactors
    {
        public class Thanos : BaseMalefactor
        {
            public Thanos(Vector position) : this(500, position) { }
            
            public Thanos(int health, Vector position) : base(health, position, State.Moves, 0.0125, 15) {}

            public override IStrike Attack() => new ThanosAttack(Position, Direction);
        }
    }
}