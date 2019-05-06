using System.Collections;
using System.Collections.Generic;
using OOP_Game.Units;

namespace OOP_Game.GameLogic
{
    public class Level
    {
        public Map Map { get; }
        public int Score { get; }
        public IEnumerable<IHero> availableHeroes { get; }
        

        public Level(Map map)
        {
            Score = 0;
            Map = map;
        }

    }
}