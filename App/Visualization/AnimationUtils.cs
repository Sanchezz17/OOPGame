using System;
using System.Drawing;

namespace App.Visualization
{
    public static class AnimationUtils
    {
        public static void AnimateImage(Animation animation, Bitmap currentAnimation, EventHandler invalidator)
        {
            if (animation.CurrentAnimation != currentAnimation)
                animation.CurrentlyAnimating = false;
            if (animation.CurrentlyAnimating) 
                return;
            ImageAnimator.Animate(currentAnimation, invalidator);
            animation.CurrentlyAnimating = true;
            animation.CurrentAnimation = currentAnimation;
        }
    }
}