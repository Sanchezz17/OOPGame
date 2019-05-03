using System;
using System.Drawing;

namespace OOP_Game.Infrastructure
{
    public enum Direction
    {
        Left,
        Right
    }

    public static class DirectionExtension
    {
        public static Point ToPoint(this Direction direction)
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