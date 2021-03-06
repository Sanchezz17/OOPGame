using System.Collections.Generic;
using Domain.GameLogic;
using Domain.Units;

namespace Domain.Infrastructure
{
 
    public class LevelMaker
    {
        private Level level;
        private IDescribe[] availableHeroes;

        public LevelMaker(IDescribe[] availableHeroes)
        {
            this.availableHeroes = availableHeroes;
            ResetLevel();
        }

        private void ResetLevel()
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
            var copy = level;
            ResetLevel();
            return copy;
        }
    }
}