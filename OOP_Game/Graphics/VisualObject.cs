using System.Collections.Generic;
using System.Drawing;
using OOP_Game.Units;

namespace OOP_Game
{
    public class VisualObject
    {
        public Bitmap PassiveImage { get; set; }
        public Bitmap AttackImage { get; set; }
        public Bitmap MoveImage { get; set; }
        public readonly Dictionary<IGameObject, Animation> Animations = new Dictionary<IGameObject, Animation>();
    }
}