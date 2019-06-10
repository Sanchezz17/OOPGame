using System.Collections.Generic;
using Domain.Infrastructure;
using Domain.Units;
using Domain.Units.OOP_Game.Units.MaleFactors;
using OOP_Game.GameLogic;

namespace Domain.GameLogic
{
    public static class GameFactory
    {
        private static WaveMaker waveMaker = new WaveMaker(Generation.Random);

        public static Game GetStandardGame(Player player)
        {
            var levelMaker = new LevelMaker(player.Heroes);
            var levels = new List<Level>
            {
                levelMaker
                    .AddWaves(GetFirstLevelWaves())
                    .MakeLevel(),
                levelMaker
                    .AddWaves(GetSecondLevelWaves())
                    .MakeLevel(),
                levelMaker
                    .AddWaves(GetThirdLevelWaves())
                    .MakeLevel(),
                levelMaker
                    .AddWaves(GetFourthLevelWaves())
                    .MakeLevel(),
                levelMaker
                    .AddWaves(GetFifthLevelWaves())
                    .MakeLevel()
            };
            return new Game(levels, player);
        }

        private static List<Wave> GetFirstLevelWaves()
        {
            return new List<Wave>
            {
                waveMaker
                    .AddMalefactors(typeof(Octavius), 1)
                    .MakeWave(200),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 3)
                    .MakeWave(600),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 5)
                    .AddMalefactors(typeof(Thanos), 1)
                    .MakeWave(1000)
            };
        }
        
        private static List<Wave> GetSecondLevelWaves()
        {
            return new List<Wave>
            {
                waveMaker
                    .AddMalefactors(typeof(Octavius), 1)
                    .MakeWave(200),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 3)
                    .MakeWave(400),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 5)
                    .AddMalefactors(typeof(Thanos), 1)
                    .MakeWave(900)
            };
        }
        
        private static List<Wave> GetThirdLevelWaves()
        {
            return new List<Wave>
            {
                waveMaker
                    .AddMalefactors(typeof(Octavius), 3)
                    .MakeWave(200),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 2)
                    .AddMalefactors(typeof(Thanos), 1)
                    .MakeWave(400),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 5)
                    .AddMalefactors(typeof(Thanos), 2)
                    .MakeWave(1000)
            };
        }
        
        private static List<Wave> GetFourthLevelWaves()
        {
            return new List<Wave>
            {
                waveMaker
                    .AddMalefactors(typeof(Octavius), 5)
                    .MakeWave(300),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 2)
                    .AddMalefactors(typeof(Thanos), 1)
                    .MakeWave(500),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 5)
                    .AddMalefactors(typeof(Thanos), 2)
                    .MakeWave(900),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 5)
                    .AddMalefactors(typeof(Thanos), 5)
                    .MakeWave(1400)
            };
        }
        
        private static List<Wave> GetFifthLevelWaves()
        {
            return new List<Wave>
            {
                waveMaker
                    .AddMalefactors(typeof(Octavius), 7)
                    .MakeWave(350),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 5)
                    .AddMalefactors(typeof(Thanos), 2)
                    .MakeWave(500),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 7)
                    .AddMalefactors(typeof(Thanos), 3)
                    .MakeWave(900),
                waveMaker
                    .AddMalefactors(typeof(Octavius), 9)
                    .AddMalefactors(typeof(Thanos), 5)
                    .MakeWave(1400)
            };
        }
    }
}