using System.Collections.Generic;
using System.Linq;
using Domain.Units;


namespace Domain.GameLogic
{
    public class Level
    {
        public Map Map { get; }
        public int GemCount { get; set; }
        public List<Wave> Waves { get; }
        public bool IsWin => !Map.Malefactors.Any() && PassedWaves == Waves.Count;
        public IDescribe[] AvailableHeroes { get; }  
        public int PassedWaves;

        public Level(Map map, IDescribe[] availableHeroes)
        {
            GemCount = 100;
            Map = map;
            AvailableHeroes = availableHeroes;
            Waves = new List<Wave>();
        }
    }
}