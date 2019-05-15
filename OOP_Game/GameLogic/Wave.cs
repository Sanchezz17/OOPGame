using OOP_Game.Infrastructure;
using OOP_Game.Units;
using System.Collections.Generic;

namespace OOP_Game.GameLogic
{
    public class Wave
    {
        public List<IMalefactor> Malefactors { get; }
        private readonly TickСontroller tickСontroller;
        public bool IsPassed;

        public Wave(List<IMalefactor> malefactors, int timeToStart)
        {
            Malefactors = malefactors;
            tickСontroller = new TickСontroller(timeToStart);
        }

        public bool IsReadyToStart() => tickСontroller.Check();
    }
}
