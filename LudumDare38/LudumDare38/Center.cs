using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class Center
    {
        /// <summary>
        /// !!!THIS WILL BE REMOVED AND REPLACED BY ANOTHER CLASS!!!
        /// </summary>

        SpriteLoader spriteLoader = SpriteLoader.Loader;

        public Center()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int centerX = (int)spriteLoader.WindowSize.X / 2;
            int centerY = (int)spriteLoader.WindowSize.Y / 2;

            int scale = (int)(spriteLoader.WindowSize.Y / 4.5f);

            spriteBatch.Draw(spriteLoader.Sprites["world"], new Rectangle(centerX - scale / 2, centerY - scale / 2, scale, scale), Color.White);

            scale = scale * 2;
            spriteBatch.Draw(spriteLoader.Sprites["circle"], new Rectangle(centerX - scale / 2, centerY - scale / 2, scale, scale), Color.White);

            scale = (int)(spriteLoader.WindowSize.Y / 4.5f);
            scale = scale * 3;
            spriteBatch.Draw(spriteLoader.Sprites["circle"], new Rectangle(centerX - scale / 2, centerY - scale / 2, scale, scale), Color.White);

            scale = (int)(spriteLoader.WindowSize.Y / 4.5f);
            scale = scale * 4;
            spriteBatch.Draw(spriteLoader.Sprites["circle"], new Rectangle(centerX - scale / 2, centerY - scale / 2, scale, scale), Color.White);
        }
    }
}
