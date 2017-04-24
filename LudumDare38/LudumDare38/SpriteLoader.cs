using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    public class SpriteLoader
    {
        //Itself
        private static SpriteLoader loader;

        //The sprites for the game
        //The size of thw indow
        public Dictionary<string, Texture2D> Sprites = new Dictionary<string, Texture2D>();
        public Vector2 WindowSize;

        public SpriteFont font;
        public Song song;
        public SoundEffect crash;

        private SpriteLoader()
        {
        }

        //Get the size of the window
        public void SetWindowSize(GraphicsDevice graphics)
        {
            WindowSize = new Vector2(graphics.PresentationParameters.Bounds.Width, graphics.PresentationParameters.Bounds.Height);
        }

        //Make sure only one spriteloader is created
        public static SpriteLoader Loader
        {
            get { if (loader == null) { loader = new SpriteLoader(); } return loader; }
        }

        public void LoadSprites(ContentManager content)
        {
            //Add the sprites to the dictionary
            Sprites.Add("planet", content.Load<Texture2D>("Planet"));
            Sprites.Add("circle", content.Load<Texture2D>("Circle"));
            Sprites.Add("spaceship", content.Load<Texture2D>("Spaceship"));
            Sprites.Add("maxSpeed", content.Load<Texture2D>("MaxSpeed"));
            Sprites.Add("speedBoost", content.Load<Texture2D>("SpeedBoost"));
            Sprites.Add("stopBoost", content.Load<Texture2D>("StopBoost"));
            Sprites.Add("starAnimation", content.Load<Texture2D>("StarAnimation"));
            Sprites.Add("playerInfo", content.Load<Texture2D>("PlayerInfo"));
            Sprites.Add("button", content.Load<Texture2D>("Button"));
            Sprites.Add("buttonControl", content.Load<Texture2D>("ButtonControl"));

            song = content.Load<Song>("Music");
            crash = content.Load<SoundEffect>("Crash");

            font = content.Load<SpriteFont>("ScienceFair");
        }
    }
}
