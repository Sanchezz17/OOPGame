using System.Collections.Generic;
using System.Windows;
using Domain.Infrastructure;
using Domain.Units;
using Domain.Units.Heroes;
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
                    .MakeLevel()
            };
            return new Game(levels);
        }

        private static List<Wave> GetFirstLevelWaves()
        {
            var wave1 = waveMaker
                .AddMalefactorsOnRandomPositions(typeof(Octavius), 1)
                .MakeWave(200);
            var wave2 = waveMaker
                .AddMalefactorsOnRandomPositions(typeof(Octavius), 3)
                .MakeWave(600);
            var wave3 = waveMaker
                .AddMalefactorsOnRandomPositions(typeof(Octavius), 5)
                .AddMalefactorsOnRandomPositions(typeof(Thanos), 1)
                .MakeWave(1000);
            return new List<Wave> {wave1, wave2, wave3};
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
    }
}