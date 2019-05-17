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
        private readonly int xPrecision = 2;
        private readonly int yPrecision = 5;
        private readonly Random random;

        public WaveMaker()
        {
            Malefactors = new List<IMalefactor>();    
            random = new Random();
        }

        public WaveMaker AddMalefactorsOnRandomPositions(Type malefactorType, int count)
        {
            for(var i = 0; i < count; i++)
            {
                var x = random.NextDouble() * xPrecision + 9;
                var y = (int)(random.NextDouble() * yPrecision);
                var ctor = malefactorType.GetConstructor(new Type[] { typeof(Vector) });
                var malefactor = (IMalefactor)ctor.Invoke(new object[] { new Vector(x, y) });
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
