using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class Ring
    {
        //Sprites
        SpriteLoader spriteLoader = SpriteLoader.Loader;

        //The ring number
        //The size of the ring
        public int ringNumber { get; set; }
        public int scale { get; set; }

        public Ring(int ringNum, int ringAmount)
        {
            //Set the ring number and the scale
            ringNumber = ringNum;
            scale = (int)(spriteLoader.WindowSize.Y / (ringAmount + .3f)) * ringNum;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Get the center of the screen
            int centerX = (int)spriteLoader.WindowSize.X / 2;
            int centerY = (int)spriteLoader.WindowSize.Y / 2;

            //Draw the ring
            spriteBatch.Draw(spriteLoader.Sprites["circle"], new Rectangle(centerX - scale / 2, centerY - scale / 2, scale, scale), Color.White);
        }
    }
}
