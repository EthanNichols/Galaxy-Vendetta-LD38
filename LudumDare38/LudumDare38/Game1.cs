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
        Background background;

        //The rings around the planet
        //The spaceships that are playing
        List<Ring> rings = new List<Ring>();
        List<Spaceship> spaceships = new List<Spaceship>();
        List<Indicator> indicators = new List<Indicator>();

        //List of boosts on the map
        //The amount of time between boosts spawning
        List<Boost> boosts = new List<Boost>();
        float boostTimer = 50;
        bool newRound = true;

        //Random function
        Random random = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Set the screen size
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            //graphics.IsFullScreen = true;
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

            background = new Background();

            //The amount of rings that will be created around the planet
            //Create the rings and add them to the list
            int ringAmount = 5;
            for (int i = 0; i < ringAmount; i++)
            {
                rings.Add(new Ring(i + 1, ringAmount));
            }

            //REMOVE THIS!!!
            //Create a temperary spaceship
            spaceships.Add(new Spaceship(rings, 1, 8));
            spaceships.Add(new Spaceship(rings, 2, 8));
            spaceships.Add(new Spaceship(rings, 3, 8));
            spaceships.Add(new Spaceship(rings, 4, 8));
            spaceships.Add(new Spaceship(rings, 5, 8));
            spaceships.Add(new Spaceship(rings, 6, 8));
            spaceships.Add(new Spaceship(rings, 7, 8));
            spaceships.Add(new Spaceship(rings, 8, 8));

            foreach (Spaceship spaceship in spaceships)
            {
                indicators.Add(new Indicator(spaceship.shipNumber, spaceship.color));
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Test if there isn't a new round happening
            if (!newRound)
            {
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
            }
            else
            {
                //Set that a new round has started
                newRound = false;
            }

            //Update the display information based off the spaceships
            foreach (Indicator indicator in indicators)
            {
                indicator.Update(spaceships[indicator.indicatorNumber - 1]);
            }

            //List of all the scores
            List<int> scores = new List<int>();

            //Get information about the indicators
            //Set the rank to 0 and add the score to the list of scores
            foreach (Indicator indicator in indicators)
            {
                scores.Add(indicator.points);
                indicator.rank = 0;
            }

            //Sort the list from smallest to largest
            //Reverse the list
            //Start the rank at 1
            scores.Sort();
            scores.Reverse();
            int rank = 1;

            //Go through all of the scores
            foreach (int score in scores)
            {
                //Get information about the indicators
                foreach (Indicator indicator in indicators)
                {
                    //Test if the indicator score is equal to the score in the list
                    //Set the rank of the indicator relative to the current rank
                    if (score == indicator.points &&
                        indicator.rank == 0)
                    {
                        indicator.rank = rank;
                    }
                }

                //Increase the rank value
                rank++;
            }

            //Whether to reset the match or not
            //The amount of spaceships alive
            bool reset = true;
            bool oneAlive = false;

            //Get information about the spaceships
            foreach (Spaceship spaceship in spaceships)
            {
                //Test if the spaceship is active and there isn't one that is alive
                //Set that there is one that is alive
                if (spaceship.active &&
                    !oneAlive)
                {
                    oneAlive = true;

                    //Test if the spaceship is active and there is one that is alive
                    //Set that the match doesn't need to be restarted
                }
                else if (spaceship.active &&
                  oneAlive)
                {
                    reset = false;
                    break;
                }
            }

            //Test if the match needs to be restarted
            if (reset)
            {
                //Reset all of the spaceships
                foreach (Spaceship spaceship in spaceships)
                {
                    spaceship.active = false;
                    spaceship.Reset(rings, spaceships.Count);
                }

                //Clear all of the boosts on the screen
                //Set the next boost to spawn in 50
                //Set that a new round has started
                boosts.Clear();
                boostTimer = 50;
                newRound = true;
            }

            //Update the background
            background.Update();

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
                boostTimer = random.Next(1, 5);
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

            //Draw the background of the game
            background.Draw(spriteBatch);

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

            //Draw the indicators for the players
            foreach (Indicator indicator in indicators)
            {
                indicator.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
