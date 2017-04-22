using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class Spaceship
    {
        //The rotation location of the spaceship
        //The ring that the spaceship it on
        //The speed the spaceship is going
        //The amount of points the spaceship has
        //The color of the spaceship
        public float rotation { get; set; }
        public int orbitRing { get; set; }
        public float speed { get; set; }
        public int points { get; set; }
        public Color color { get; set; }

        //The key that moves the spaceship in or out
        public Keys moveIn { get; set; }
        public Keys moveOut { get; set; }

        //Sprites
        private SpriteLoader spriteLoader = SpriteLoader.Loader;

        public Spaceship()
        {
            //Set information about the spaceship
            rotation = 0;
            orbitRing = 2;
            speed = 1;
            points = 0;
            color = Color.Red;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //The spaceship image
            //Draw the spaceship
            Texture2D spaceship = spriteLoader.Sprites["spaceship"];
            spriteBatch.Draw(spaceship, new Rectangle((int)spriteLoader.WindowSize.X / 2 - spaceship.Width / 2, (int)spriteLoader.WindowSize.Y / 2 - spaceship.Height / 2, spaceship.Width, spaceship.Height), color);
        }
    }
}
