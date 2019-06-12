using System.Collections.Generic;
using System.Drawing;
using Domain.Units;

namespace App.Visualization
{
    public class VisualObject
    {
        public Bitmap PassiveImage { get; set; }
        public Bitmap AttackImage { get; set; }
        public Bitmap MoveImage { get; set; }
        public readonly Dictionary<IGameObject, Animation> Animations = new Dictionary<IGameObject, Animation>();
    }
}