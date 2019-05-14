using OOP_Game.Units;
using System.Collections.Generic;

namespace OOP_Game.GameLogic
{
    public class Wave
    {
        public IEnumerable<IMalefactor> malefactors;

        public Wave(IEnumerable<IMalefactor> malefactors)
        {
            this.malefactors = malefactors;
        }
    }
}
