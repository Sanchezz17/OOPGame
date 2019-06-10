using System;
using System.Windows;

namespace OOP_Game.GameLogic
{
    public static class Generation
    {
        private static Random random = new Random();
        public static Vector Random(Vector precision)
        {
            return new Vector(random.NextDouble() * precision.X + 9,
                (int)(random.NextDouble() * precision.Y));
        }
    }
}