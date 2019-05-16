using System;
using System.Windows;

namespace Domain.Infrastructure
{
    public enum Direction
    {
        Left,
        Right,
        Down, 
        Up
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
                case Direction.Down:
                    return new Vector(0, 1);
                case Direction.Up:
                    return new Vector(0, -1);
                default:
                    throw new ArgumentException();
            }
        }
    }   
}