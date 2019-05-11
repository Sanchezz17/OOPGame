using System;
using System.Collections;
using System.Collections.Generic;
using OOP_Game.Infrastructure;
using OOP_Game.Units;

namespace OOP_Game.GameLogic
{
    public class Level
    {
        public Map Map { get; }
        public int GemCount { get; set; }
        public List<PurchaseObject> availableHeroes { get; }      

        public Level(Map map, List<PurchaseObject> availableHeroes)
        {
            GemCount = 100;
            Map = map;
            this.availableHeroes = availableHeroes;
        }
    }
}