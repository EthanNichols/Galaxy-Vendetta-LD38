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

        SpriteLoader spriteLoader = SpriteLoader.Loader;

        public Center()
        {

        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            int centerX = graphics.PresentationParameters.Bounds.Width / 2;
            int centerY = graphics.PresentationParameters.Bounds.Height / 2;

            int Scale = (int)(graphics.PresentationParameters.Bounds.Height / 4.5f);

            spriteBatch.Draw(spriteLoader.sprites["world"], new Rectangle(centerX - Scale / 2, centerY - Scale / 2, Scale, Scale), Color.White);

            Scale = Scale * 2;
            spriteBatch.Draw(spriteLoader.sprites["circle"], new Rectangle(centerX - Scale / 2, centerY - Scale / 2, Scale, Scale), Color.White);

            Scale = Scale = (int)(graphics.PresentationParameters.Bounds.Height / 4.5f);
            Scale = Scale * 3;
            spriteBatch.Draw(spriteLoader.sprites["circle"], new Rectangle(centerX - Scale / 2, centerY - Scale / 2, Scale, Scale), Color.White);

            Scale = Scale = (int)(graphics.PresentationParameters.Bounds.Height / 4.5f);
            Scale = Scale * 4;
            spriteBatch.Draw(spriteLoader.sprites["circle"], new Rectangle(centerX - Scale / 2, centerY - Scale / 2, Scale, Scale), Color.White);
        }
    }
}
