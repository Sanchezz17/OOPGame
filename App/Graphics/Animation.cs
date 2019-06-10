using System.Drawing;

namespace App.Graphics
{
    public class Animation
    {
        public bool CurrentlyAnimating { get; set; }
        public double StartAnimatingTime { get; set; }
        public Bitmap CurrentAnimation { get; set; }

        public Animation(bool currentlyAnimating, Bitmap currentAnimation)
        {
            CurrentlyAnimating = currentlyAnimating;
            CurrentAnimation = currentAnimation;
        }
    }
}