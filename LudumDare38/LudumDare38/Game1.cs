using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LudumDare38
{
    public class Game1 : Game
    {
        //The graphics and the spritebatch to draw images
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //REMOVE THIS!!!
        Center center = new Center();

        //The sprite manager
        SpriteLoader spriteLoader = SpriteLoader.Loader;

        //The rings around the planet
        //The spaceships that are playing
        List<Ring> rings = new List<Ring>();
        List<Spaceship> spaceships = new List<Spaceship>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Set the screen size
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            //graphics.IsFullScreen = true;

            //REMOVE THIS!!!
            //Create a temperary spaceship
            spaceships.Add(new Spaceship());
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Set the size of the window
            //Load the images for the game
            spriteLoader.SetWindowSize(GraphicsDevice);
            spriteLoader.LoadSprites(Content);

            //The amount of rings that will be created around the planet
            //Create the rings and add them to the list
            int ringAmount = 5;
            for (int i = 0; i < ringAmount; i++)
            {
                rings.Add(new Ring(i + 1, ringAmount));
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Clear the window and set it to be black
            GraphicsDevice.Clear(Color.Black);

            //Start the spritebatch with a pixel perfect 'shader'
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            //REMOVE THIS!!!
            //center.Draw(spriteBatch);

            //Draw all of the rings
            foreach (Ring ring in rings)
            {
                ring.Draw(spriteBatch);
            }

            //Draw all of the spaceships
            foreach (Spaceship spaceship in spaceships)
            {
                spaceship.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
