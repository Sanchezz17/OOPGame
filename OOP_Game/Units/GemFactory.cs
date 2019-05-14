using System;
using System.Windows;

namespace OOP_Game.Units
{
    public class GemFactory : IGemManufacturer
    {
        private int rechargeTimeInTicks;
        private int baseRechargeTimeInTicks;
        private Random random;

        public GemFactory()
        {
            baseRechargeTimeInTicks = 200;
            rechargeTimeInTicks = baseRechargeTimeInTicks;
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