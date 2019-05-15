using System.Collections.Generic;
using System.Windows;
using OOP_Game.Infrastructure;
using OOP_Game.Units;
using OOP_Game.Units.Heroes;
using OOP_Game.Units.OOP_Game.Units.MaleFactors;

namespace OOP_Game.GameLogic
{
    public static class GameFactory
    {
        private static WaveMaker waveMaker = new WaveMaker();

        public static Game GetStandardGame()
        {
            var levelMaker = new LevelMaker();
            var levels = new List<Level>
            {
                levelMaker
                    .AddHero(new IronMan(3000, new Vector(2, 2)))
                    .AddHero(new IronMan(3000, new Vector(3, 4)))
                    .AddHero(new Vision(1000, new Vector(1, 1)))
                    .AddMalefactor(new Octavius(new Vector(5, 2)))
                    .AddWaves(GetFirstLevelWaves())
                    .MakeLevel()
            };
            return new Game(levels);
        }

        private static List<Wave> GetFirstLevelWaves()
        {
            var wave1 = waveMaker
                .AddMalefactorsOnRandomPositions(typeof(Octavius), 1)
                .MakeWave(100);
            var wave2 = waveMaker
                .AddMalefactorsOnRandomPositions(typeof(Octavius), 2)
                .AddMalefactorsOnRandomPositions(typeof(Thanos), 1)
                .MakeWave(200);
            var wave3 = waveMaker
                .AddMalefactorsOnRandomPositions(typeof(Octavius), 5)
                .AddMalefactorsOnRandomPositions(typeof(Thanos), 2)
                .MakeWave(400);
            return new List<Wave> {wave1, wave2, wave3};
        }
    }
}