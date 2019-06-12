using System;
using System.Drawing;
using System.Windows;
using Size = System.Drawing.Size;

namespace Domain.Infrastructure
{
    public static class CoordinatesUtils
    {
        public static RectangleF GetRectangleToPaintByLocationInMap(Vector locationInMap, Size cellSize)
        {
            var x = (float)(locationInMap.X * cellSize.Width);
            var y = (float)(locationInMap.Y * cellSize.Height);
            return new RectangleF(x, y, cellSize.Width, cellSize.Height);
        }

        public static Vector GetLocationInMapByLocationInControl(Vector locationInControl, Size cellSize)
        {
            var x = Math.Truncate(locationInControl.X / cellSize.Width);
            var y = Math.Truncate(locationInControl.Y / cellSize.Height);
            return new Vector(x, y);
        }
    }
}