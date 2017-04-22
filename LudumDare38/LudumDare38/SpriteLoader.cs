using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    public class SpriteLoader
    {

        private static SpriteLoader loader;

        public Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();
        
        private SpriteLoader()
        {

        }

        public static SpriteLoader Loader
        {
            get { if (loader == null) { loader = new SpriteLoader(); } return loader; }
        }

        public void LoadSprites(ContentManager content)
        {
            sprites.Add("world", content.Load<Texture2D>("Planet"));
            sprites.Add("circle", content.Load<Texture2D>("Circle"));
        }
    }
}
