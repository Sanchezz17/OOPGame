using System.Collections.Generic;
using System.Windows.Forms;

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
            return new Game(levels);
        }
    }
}