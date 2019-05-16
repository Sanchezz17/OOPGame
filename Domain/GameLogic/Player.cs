using System;
using System.Collections.Generic;
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
            Coins = 5000;
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

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }
    }
}