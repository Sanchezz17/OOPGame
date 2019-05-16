using System;
using System.Collections.Generic;
using Domain.GameLogic;
using Domain.Units;
using Domain.Units.Heroes;

namespace Domain.Infrastructure
{
 
    public class LevelMaker
    {
        private Level level;

        public LevelMaker(HashSet<DescribeObject> availableHeroes)
        {
            level = new Level(
                new Map(5, 11), availableHeroes);
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