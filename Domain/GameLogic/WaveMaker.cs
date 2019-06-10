using System;
using System.Collections.Generic;
using System.Windows;
using Domain.GameLogic;
using Domain.Units;

namespace Domain.Infrastructure
{
    public class WaveMaker
    {
        private List<IMalefactor> Malefactors { get; }
        private readonly Vector precision = new Vector(2, 5);

        private Func<Vector, Vector> GetNextPosition;
        
        public WaveMaker(Func<Vector, Vector> getNextPosition)
        {
            Malefactors = new List<IMalefactor>();
            GetNextPosition = getNextPosition;
        }

        public WaveMaker AddMalefactors(Type malefactorType, int count)
        {
            for(var i = 0; i < count; i++)
            {
                var nextPosition = GetNextPosition(precision);
                Console.WriteLine(nextPosition);
                var ctor = malefactorType.GetConstructor(new [] { typeof(Vector) });
                var malefactor = (IMalefactor)ctor.Invoke(new object[] { new Vector(nextPosition.X, nextPosition.Y) });
                Malefactors.Add(malefactor);
            }
            return this;
        }

        public Wave MakeWave(int timeToStart)
        {
            var result = new Wave(new List<IMalefactor>(Malefactors), timeToStart);
            Malefactors.Clear();
            return result;
        }
    }
}
