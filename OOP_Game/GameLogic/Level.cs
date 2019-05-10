using System;
using System.Collections;
using System.Collections.Generic;
using OOP_Game.Units;

namespace OOP_Game.GameLogic
{
    public class Level
    {
        public Map Map { get; }
        public int Score { get; }
        public IEnumerable<Type> availableHeroes { get; }
        

        public Level(Map map, IEnumerable<Type> _availableHeroes)
        {
            Score = 0;
            Map = map;
            availableHeroes = _availableHeroes;
        }

    }
}