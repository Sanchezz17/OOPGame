using System;
using System.Collections.Generic;
using OOP_Game.GameLogic;
using OOP_Game.Units;
using OOP_Game.Units.Heroes;

namespace OOP_Game.Infrastructure
{
    public class LevelMaker
    {
        private Level level;

        public LevelMaker()
        {
            level = new Level(new Map(5, 9), new List<Type> {typeof(IronMan), typeof(Octavius)});
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

        public Level MakeLevel()
        {
            return level;
        }
        

    }
}