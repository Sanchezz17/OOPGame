using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using OOP_Game.Units.Heroes;
using OOP_Game.Units.OOP_Game.Units.MaleFactors;

namespace OOP_Game.GameLogic
{
    public class GameFactory
    {
        public static Game GetStandardGame()
        {
            var levels = new List<Level>
            {
                new Level(new Map(5, 9))
            };
            var ironMan = new IronMan(3000, new Vector(2, 2));
            levels[0].Map.Add(ironMan);
            levels[0].Map.Add(new IronMan(3000, new Vector(3, 4)));
            var thanos = new Thanos(100000, new Vector(8, 2));
            levels[0].Map.Add(thanos);
            levels[0].Map.Add(new Thanos(100000, new Vector(7, 2)));
            
            levels[0].Map.Add(new Thanos(100000, new Vector(8, 0)));
            levels[0].Map.Add(new Thanos(100000, new Vector(7, 1)));
            levels[0].Map.Add(new Thanos(100000, new Vector(7, 3)));
            levels[0].Map.Add(new Thanos(100000, new Vector(8, 4)));
            return new Game(levels);
        }
    }
    
    
}