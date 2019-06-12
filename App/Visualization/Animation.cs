using System.Drawing;

namespace App.Visualization
{
    public class Animation
    {
        public bool CurrentlyAnimating { get; set; }
        public Bitmap CurrentAnimation { get; set; }

        public Animation(bool currentlyAnimating, Bitmap currentAnimation)
        {
            CurrentlyAnimating = currentlyAnimating;
            CurrentAnimation = currentAnimation;
        }
    }
}