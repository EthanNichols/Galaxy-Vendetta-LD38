using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class Star
    {
        //Whether the star is animated or not
        //The position of the star
        public bool animated { get; set; }
        public Rectangle position { get; set; }

        //The current frame the star is on
        //Currently stars only have two frames
        //The timer between each frame
        //The current time before the next frame
        public int frame { get; set; }
        public int frame1 { get; set; }
        public int frame2 { get; set; }
        public int frameTimer { get; set; }
        public float timer { get; set; }

        public Star(Random random, Vector2 windowSize, int frameAmount)
        {
            //Decide if the star should be animated or not
            int animate = random.Next(2);
            if (animate == 1)
            {
                animated = true;
            } else
            {
                animated = false;
            }

            //TEMPERARY?
            //Set all stars to be animated
            animated = true;

            //Set a random position and size for the star
            int size = random.Next(10, 50);
            position = new Rectangle(random.Next((int)windowSize.X), random.Next((int)windowSize.Y), size, size);

            //Set a random frame for the star
            //Set the next frame to the one after the random frame
            int randomFrame = random.Next(0, frameAmount);
            frame1 = randomFrame;
            frame2 = randomFrame + 1;
            frame = frame1;

            //Test if the star i being animated
            //Give the star a animation timer
            if (animated)
            {
                frameTimer = random.Next(3, 10);
            }
        }

        public void UpdateAnimation(int frameAmount)
        {
            //Test if the timer is greate the the animation timer
            if (timer >= frameTimer)
            {
                //Reset the timer
                timer = 0;

                //Test which frame the star is on
                //Set the current frame to the other frame
                if (frame == frame1)
                {
                    frame = frame2;
                } else
                {
                    frame = frame1;
                }
            } else
            {
                //Increase the animation timer 
                timer += .1f;
            }
        }
    }
}
