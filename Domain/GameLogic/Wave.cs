using System.Collections.Generic;
using Domain.Infrastructure;
using Domain.Units;

namespace Domain.GameLogic
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
