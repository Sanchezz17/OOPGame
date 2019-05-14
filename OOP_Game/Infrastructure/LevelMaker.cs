using System;
using System.Collections.Generic;
using OOP_Game.GameLogic;
using OOP_Game.Units;
using OOP_Game.Units.Heroes;

namespace OOP_Game.Infrastructure
{
    public class PurchaseObject
    {
        public Type Type { get; }
        public int Price { get; }
        public int Health { get; }

        public PurchaseObject(Type type, int price, int health)
        {
            Type = type;
            Price = price;
            Health = health;
        }
    }
    
    public class LevelMaker
    {
        private Level level;

        public LevelMaker()
        {
            level = new Level(
                new Map(5, 11),
                new List<PurchaseObject>
                {
                    new PurchaseObject(typeof(Vision), 50, 1000),
                    new PurchaseObject(typeof(IronMan), 100, 3000)
                });
        }

        public LevelMaker AddMalefactor(IMalefactor malefactor)
        {
            level.Map.Add(malefactor);
            return this;
        }

        public LevelMaker AddHero(IHero hero)
        {
            level.Map.Add(hero);
            return this;
        }

        public LevelMaker AddWave(Wave wave)
        {
            level.Waves.Add(wave);
            return this;
        }

        public LevelMaker AddWaves(IEnumerable<Wave> waves)
        {
            level.Waves.AddRange(waves);
            return this;
        }

        public Level MakeLevel()
        {
            return level;
        }
    }
}