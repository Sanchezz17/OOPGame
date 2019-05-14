using OOP_Game.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
