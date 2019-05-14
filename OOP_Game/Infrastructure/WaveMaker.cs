using OOP_Game.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOP_Game.Infrastructure
{
    public class WaveMaker
    {
        private List<IMalefactor> malefactors;
        private readonly int XPrecision = 2;
        private readonly int YPrecision = 5;
        private Random random;

        public WaveMaker()
        {
            malefactors = new List<IMalefactor>();
            random = new Random();
        }

        public WaveMaker LocateMalefactorsOnRandomPositions(Type malefactorType, int count)
        {
            for(var i = 0; i < count; i++)
            {
                var x = random.NextDouble() * XPrecision + 9;
                var y = random.NextDouble() * YPrecision;
                var ctor = malefactorType.GetConstructor(new Type[] { typeof(Vector) });
                var malefactor = (IMalefactor)ctor.Invoke(new object[] { new Vector(x, y) });
                malefactors.Add(malefactor);
            }
            return this;
        }

        public IEnumerable<IMalefactor> MakeWave()
        {
            var result = new List<IMalefactor>(malefactors);
            malefactors.Clear();
            return result;
        }
    }
}
