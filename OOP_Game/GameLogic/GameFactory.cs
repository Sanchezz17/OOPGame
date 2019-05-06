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
            var ironMan = new IronMan(3, new Vector(2, 2));
            levels[0].Map.Add(ironMan);
            var thanos = new Thanos(100000, new Vector(8, 2));
            levels[0].Map.Add(thanos);
            return new Game(levels);
        }
    }
    
    
}