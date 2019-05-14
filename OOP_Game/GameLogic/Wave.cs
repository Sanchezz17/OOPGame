using OOP_Game.Infrastructure;
using OOP_Game.Units;
using System.Collections.Generic;

namespace OOP_Game.GameLogic
{
    public class Wave
    {
        public List<IMalefactor> malefactors { get; }
        private TickСontroller tickСontroller;
        public bool IsPassed;

        public Wave(List<IMalefactor> malefactors, int timeToStart)
        {
            this.malefactors = malefactors;
            tickСontroller = new TickСontroller(timeToStart);
        }

        public bool IsReadyToStart() => tickСontroller.Check();
    }
}
