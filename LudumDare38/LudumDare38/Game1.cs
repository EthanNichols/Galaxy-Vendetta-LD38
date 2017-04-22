using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LudumDare38
{
    public class Game1 : Game
    {
        //The graphics and the spritebatch to draw images
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //The sprite manager
        SpriteLoader spriteLoader = SpriteLoader.Loader;

        //The rings around the planet
        //The spaceships that are playing
        List<Ring> rings = new List<Ring>();
        List<Spaceship> spaceships = new List<Spaceship>();

        //List of boosts on the map
        //The amount of time between boosts spawning
        List<Boost> boosts = new List<Boost>();
        float boostTimer = 5;

        //Random function
        Random random = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Set the screen size
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
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
            int ringAmount = 4;
            for (int i = 0; i < ringAmount; i++)
            {
                rings.Add(new Ring(i + 1, ringAmount));
            }

            //REMOVE THIS!!!
            //Create a temperary spaceship
            spaceships.Add(new Spaceship(rings, 1, 5));
            spaceships.Add(new Spaceship(rings, 2, 5));
            spaceships.Add(new Spaceship(rings, 3, 5));
            spaceships.Add(new Spaceship(rings, 4, 5));
            spaceships.Add(new Spaceship(rings, 5, 5));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Get information about all of the spaceships
            foreach (Spaceship spaceship in spaceships)
            {
                //Test if the spaceship is active
                //Test for collisions
                if (spaceship.active)
                {
                    spaceship.Update(rings);
                    spaceship.Collision(spaceships);
                }
            }

            //Spawn and despawn boosts
            Boost();

            base.Update(gameTime);
        }

        private void Boost()
        {
            //Test the timer for a new boost is done
            if (boostTimer <= 0)
            {
                //Spawn a new boost
                //Reset the timer for a new boost
                boosts.Add(new Boost(rings, random));
                boostTimer = random.Next(3, 7);
            }
            else
            {
                //Decrease the amount of time until a new boost spawns
                boostTimer -= .1f;
            }

            //List of boosts that will be removed
            List<Boost> removeBoosts = new List<Boost>();

            //Get information about the boosts
            foreach (Boost boost in boosts)
            {
                //Update the boost's information
                //Test if a spaceship has collided with the boost
                boost.update();
                boost.Collision(spaceships);

                //Test if the boost's has run out
                //Add the boost to the list of boosts to remove
                if (boost.timeActive <= 0)
                {
                    removeBoosts.Add(boost);
                }
            }

            //Remove all boosts in the list
            foreach (Boost boost in removeBoosts)
            {
                boosts.Remove(boost);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            //Clear the window and set it to be black
            GraphicsDevice.Clear(Color.Black);

            //Start the spritebatch with a pixel perfect 'shader'
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            //Draw all of the rings
            foreach (Ring ring in rings)
            {
                ring.Draw(spriteBatch);
            }

            //Draw all of the boosts
            foreach (Boost boost in boosts)
            {
                boost.Draw(spriteBatch);
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
