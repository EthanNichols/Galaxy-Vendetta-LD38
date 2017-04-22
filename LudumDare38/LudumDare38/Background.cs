using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class Background
    {
        //List of stars that are visible in the background
        List<Star> stars = new List<Star>();

        //Sprites
        //Random function
        SpriteLoader spriteLoader = SpriteLoader.Loader;
        Random random = new Random();
        
        public Background()
        {
            //Create 100 different stars
            for (int i=0; i<100; i++)
            {
                stars.Add(new Star(random, spriteLoader.WindowSize, spriteLoader.Sprites["starAnimation"].Bounds.Width / 32));
            }
        }

        public void Update()
        {
            //Update the animation of all the stars that are aniamted
            foreach (Star star in stars)
            {
                if (star.animated)
                {
                    star.UpdateAnimation(spriteLoader.Sprites["starAnimation"].Bounds.Width / 32);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Get the star spritesheet
            Texture2D spriteSheet = spriteLoader.Sprites["starAnimation"];

            //Draw each star and that frame that the star is currently on
            foreach (Star star in stars)
            {
                spriteBatch.Draw(spriteSheet, star.position, new Rectangle(32 * star.frame, 0, 32, 32), Color.White);
            }
        }
    }
}
