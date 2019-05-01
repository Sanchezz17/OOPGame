using System;
using System.Drawing;

namespace OOP_Game.Units
{
    public enum Direction
    {
        Left,
        Right
    }

    public class DirectionExtension
    {
        public static Point DirectionToPoint(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new Point(1, 0);
                case Direction.Right:
                    return new Point(-1, 0);
                default:
                    throw new ArgumentException();
            }
        }
    }   
}