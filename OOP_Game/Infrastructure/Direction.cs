using System;
using System.Drawing;
using System.Windows;

namespace OOP_Game.Infrastructure
{
    public enum Direction
    {
        Left,
        Right
    }

    public static class DirectionExtension
    {
        public static Vector ToVector(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new Vector(-1, 0);
                case Direction.Right:
                    return new Vector(1, 0);
                default:
                    throw new ArgumentException();
            }
        }
    }   
}