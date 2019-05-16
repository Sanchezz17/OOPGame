namespace Domain.Infrastructure
{
    public class TickСontroller
    {
        private int rechargeTimeInTicks;
        private int baseRechargeTimeInTicks;
        
        public TickСontroller(int countTick)
        {
            baseRechargeTimeInTicks = countTick;
            rechargeTimeInTicks = countTick;
        }

        public bool Check()
        {
            if (rechargeTimeInTicks == 0)
            {
                rechargeTimeInTicks = baseRechargeTimeInTicks;
                return true;
            }
            rechargeTimeInTicks--;
            return false;
        }
    }
}