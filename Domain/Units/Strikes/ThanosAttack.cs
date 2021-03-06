﻿using System.Windows;
using Domain.Infrastructure;

namespace Domain.Units.Strikes
{
    class ThanosAttack : Shot
    {
        public ThanosAttack(Vector position, Direction direction) : base(direction, 1, position) {}

        public override int ToDamage(UnitParameters parameters)
        {           
            Health -= 1;
            if (Health <= 0)
                IsDead = true;
            var enemyHealth = parameters.Health;
            if (enemyHealth <= 100)
                return enemyHealth;
            return enemyHealth / 2;
        }
    }
}
