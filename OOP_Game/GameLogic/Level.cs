using System.Collections.Generic;
using OOP_Game.Infrastructure;

namespace OOP_Game.GameLogic
{
    public class Level
    {
        public Map Map { get; }
        public int GemCount { get; set; }
        public List<PurchaseObject> AvailableHeroes { get; }      

        public Level(Map map, List<PurchaseObject> availableHeroes)
        {
            GemCount = 100000;
            Map = map;
            AvailableHeroes = availableHeroes;
        }
    }
}