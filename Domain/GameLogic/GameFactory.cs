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
                    .AddHero(new IronMan(player.GetHeroParametres(typeof(IronMan)).Parameters, new Vector(2, 2)))
                    .AddHero(new IronMan(player.GetHeroParametres(typeof(IronMan)).Parameters, new Vector(3, 4)))
                    .AddHero(new Vision(player.GetHeroParametres(typeof(Vision)).Parameters, new Vector(1, 1)))
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
                .AddMalefactorsOnRandomPositions(typeof(Octavius), 3)
                .MakeWave(300);
            var wave3 = waveMaker
                .AddMalefactorsOnRandomPositions(typeof(Octavius), 5)
                .AddMalefactorsOnRandomPositions(typeof(Thanos), 1)
                .MakeWave(500);
            return new List<Wave> {wave1, wave2, wave3};
        }
    }
}