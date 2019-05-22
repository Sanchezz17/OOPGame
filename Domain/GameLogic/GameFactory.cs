using System.Collections.Generic;
using Domain.Infrastructure;
using Domain.Units;
using Domain.Units.OOP_Game.Units.MaleFactors;

namespace Domain.GameLogic
{
    public static class GameFactory
    {
        private static WaveMaker waveMaker = new WaveMaker();

        public static Game GetStandardGame()
        {
            var player = Player.Instance;
            var levelMaker = new LevelMaker(Player.Instance.Heroes);
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
            return new Game(levels);
        }

        private static List<Wave> GetFirstLevelWaves()
        {
            return new List<Wave>()
            {
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 1)
                    .MakeWave(200),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 3)
                    .MakeWave(600),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 5)
                    .AddMalefactorsOnRandomPositions(typeof(Thanos), 1)
                    .MakeWave(1000)
            };
        }
        
        private static List<Wave> GetSecondLevelWaves()
        {
            return new List<Wave>()
            {
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 1)
                    .MakeWave(200),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 3)
                    .MakeWave(400),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 5)
                    .AddMalefactorsOnRandomPositions(typeof(Thanos), 1)
                    .MakeWave(900)
            };
        }
        
        private static List<Wave> GetThirdLevelWaves()
        {
            return new List<Wave>()
            {
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 3)
                    .MakeWave(200),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 2)
                    .AddMalefactorsOnRandomPositions(typeof(Thanos), 1)
                    .MakeWave(400),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 5)
                    .AddMalefactorsOnRandomPositions(typeof(Thanos), 2)
                    .MakeWave(1000)
            };
        }
        
        private static List<Wave> GetFourthLevelWaves()
        {
            return new List<Wave>()
            {
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 5)
                    .MakeWave(300),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 2)
                    .AddMalefactorsOnRandomPositions(typeof(Thanos), 1)
                    .MakeWave(500),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 5)
                    .AddMalefactorsOnRandomPositions(typeof(Thanos), 2)
                    .MakeWave(900),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 5)
                    .AddMalefactorsOnRandomPositions(typeof(Thanos), 5)
                    .MakeWave(1400)
            };
        }
        
        private static List<Wave> GetFifthLevelWaves()
        {
            return new List<Wave>()
            {
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 7)
                    .MakeWave(350),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 5)
                    .AddMalefactorsOnRandomPositions(typeof(Thanos), 2)
                    .MakeWave(500),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 7)
                    .AddMalefactorsOnRandomPositions(typeof(Thanos), 3)
                    .MakeWave(900),
                waveMaker
                    .AddMalefactorsOnRandomPositions(typeof(Octavius), 9)
                    .AddMalefactorsOnRandomPositions(typeof(Thanos), 5)
                    .MakeWave(1400)
            };
        }
    }
}