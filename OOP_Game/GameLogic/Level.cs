using System.Collections.Generic;
using System.Linq;
using OOP_Game.Infrastructure;

namespace OOP_Game.GameLogic
{
    public class Level
    {
        public Map Map { get; }
        public int GemCount { get; set; }
        public List<Wave> Waves { get; }
        public bool IsWin => !Map.Malefactors.Any() && PassedWaves == Waves.Count;
        public HashSet<DescribeObject> AvailableHeroes { get; }  
        public int PassedWaves;

        public Level(Map map, HashSet<DescribeObject> availableHeroes)
        {
            GemCount = 100;
            Map = map;
            AvailableHeroes = availableHeroes;
            Waves = new List<Wave>();
        }
    }
}