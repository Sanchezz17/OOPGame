using System.Collections.Generic;
using System.Windows;
using OOP_Game.Infrastructure;
using OOP_Game.Units;
using OOP_Game.Units.Heroes;
using OOP_Game.Units.OOP_Game.Units.MaleFactors;

namespace OOP_Game.GameLogic
{
    public class GameFactory
    {
        public static Game GetStandardGame()
        {
            var levelMaker = new LevelMaker();
            var levels = new List<Level>
            {
                levelMaker
                    .AddHero(new IronMan(3000, new Vector(2, 2)))
                    .AddHero(new IronMan(3000, new Vector(3, 4)))
                    .AddHero(new Vision(1000, new Vector(1, 1)))
                    .AddMalefactor(new Thanos(1000, new Vector(8, 2)))
                    .AddMalefactor(new Thanos(1000, new Vector(8, 0)))
                    .AddMalefactor(new Thanos(1000, new Vector(7, 1)))
                    .AddMalefactor(new Thanos(1000, new Vector(7, 3)))
                    .AddMalefactor(new Octavius(100, new Vector(8, 4)))
                    .MakeLevel()
            };
            return new Game(levels);
        }
    }
    
    
}