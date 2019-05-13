using System;
using System.Windows;
using OOP_Game.Infrastructure;

namespace OOP_Game.Units
{
    public class GemFactory
    {
        private int rechargeTimeInTicks;
        private int baseRechargeTimeInTicks;
        private Random random;

        public GemFactory()
        {
            rechargeTimeInTicks = 100;
            baseRechargeTimeInTicks = 100;
            random = new Random();
        }

        public bool IsAvailable()
        {
            if (rechargeTimeInTicks == 0)
            {
                rechargeTimeInTicks = baseRechargeTimeInTicks;
                return true;
            }

            rechargeTimeInTicks--;
            return false;
        }

        public Gem GetGem()
        {
            return new Gem(new Vector(random.NextDouble() * 9, 0));
        }
    }
}