using System;
using System.Drawing;

namespace OOP_Game
{
    public class AnimationUtils
    {
        public static void AnimateImage(Animation animation, Bitmap currentAnimation, EventHandler invalidator)
        {
            if (animation.CurrentAnimation != currentAnimation)
                animation.CurrentlyAnimating = false;
            if (!animation.CurrentlyAnimating)
            {
                ImageAnimator.Animate(currentAnimation, invalidator);
                animation.CurrentlyAnimating = true;
                animation.CurrentAnimation = currentAnimation;
            }
        }
    }
}