using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Infrastructure;
using Domain.Units;
using Domain.Units.Heroes;

namespace Domain.GameLogic
{
    public class Player
    {
        public int Coins { get; set; }
        public HashSet<DescribeObject> Heroes { get; }
        private static Player instance = null;
        private Player()
        {
            Coins = 100;
            Heroes = new HashSet<DescribeObject>
            {
                new DescribeObject(typeof(IronMan), 100,new UnitParameters()
                .SetHealth(3000).SetDamage(10).SetReload(15)),

                new DescribeObject(typeof(Vision), 50,
                new UnitParameters().SetHealth(1000).SetReload(150)),

                new DescribeObject(typeof(CaptainAmerica), 50,
                new UnitParameters().SetHealth(10000))
            };
        }

        public DescribeObject GetHeroParameters(Type heroType)
        {
            return Heroes
                    .First(c => c.Type == heroType);
        }

        public static Player Instance => instance ?? (instance = new Player());
    }
}