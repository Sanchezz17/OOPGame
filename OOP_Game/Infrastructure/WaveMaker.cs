using OOP_Game.Units;
using System;
using System.Collections.Generic;
using System.Windows;
using OOP_Game.GameLogic;

namespace OOP_Game.Infrastructure
{
    public class WaveMaker
    {
        private List<IMalefactor> malefactors { get; }
        private readonly int XPrecision = 2;
        private readonly int YPrecision = 4;
        private Random random;

        public WaveMaker()
        {
            malefactors = new List<IMalefactor>();    
            random = new Random();
        }

        public WaveMaker AddMalefactorsOnRandomPositions(Type malefactorType, int count)
        {
            for(var i = 0; i < count; i++)
            {
                var x = random.NextDouble() * XPrecision + 9;
                var y = random.Next(0, YPrecision);
                var ctor = malefactorType.GetConstructor(new Type[] { typeof(Vector) });
                var malefactor = (IMalefactor)ctor.Invoke(new object[] { new Vector(x, y) });
                malefactors.Add(malefactor);
            }
            return this;
        }

        public Wave MakeWave(int timeToStart)
        {
            var result = new Wave(new List<IMalefactor>(malefactors), timeToStart);
            malefactors.Clear();
            return result;
        }
    }
}
