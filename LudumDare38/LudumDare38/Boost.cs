using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class Boost
    {
        //The type of boost
        //Where the boost is located
        public BoostType boost { get; set; }
        public Rectangle rectangle { get; set; }

        //The amount of time before the boost dissapears
        //Whether the boost has been collected or not
        public float timeActive { get; set; }
        public bool active { get; set; }

        //The position the boost is on a ring
        //The ring that the boost is on
        public float rotation { get; set; }
        public int orbitRing { get; set; }

        //Sprites
        SpriteLoader spriteLoader = SpriteLoader.Loader;

        //The different types of boosts
        public enum BoostType
        {
            maxSpeed,
            stop,
            speedBoost
        }

        public Boost(List<Ring> rings, Random random)
        {
            //Set a random boost to spawn
            var boosts = Enum.GetValues(typeof(BoostType));
            boost = (BoostType)boosts.GetValue(random.Next(boosts.Length));

            //Set the amount of time that the boost stays
            //Set that the boost is active
            timeActive = 30;
            active = true;

            //Set a random rotation for the boost to be on the ring
            //Set a random orbit ring for the boost to be on
            //Set the ring that the boost is on
            rotation = random.Next(360);
            orbitRing = random.Next(rings.Count());
            Ring ring = rings[orbitRing];

            //Calculate the size of the spaceship
            int size = (int)(spriteLoader.WindowSize.Y / (rings.Count() * 12));

            //Calulate the center of the screen
            //Calculate the position of the spaceship on the ring
            //Calculate the actual position of the spaceship
            int centerX = (int)spriteLoader.WindowSize.X / 2;
            int centerY = (int)spriteLoader.WindowSize.Y / 2;
            int circleX = (int)(ring.size / 2 * Math.Cos(rotation * (Math.PI / 180)));
            int circleY = (int)(ring.size / 2 * Math.Sin(rotation * (Math.PI / 180)));
            int xPos = centerX - size / 2 + circleX;
            int yPos = centerY - size / 2 + circleY;

            //Set the specific location of the boost
            rectangle = new Rectangle(xPos, yPos, size, size);
        }

        public void Collision(List<Spaceship> spaceships)
        {
            //Get information about the spaceships
            foreach (Spaceship spaceship in spaceships)
            {
                //Test if the spaceship is over the boost
                //Make sure the boost is active
                if (spaceship.rectangle.Contains(rectangle) &&
                    active)
                {
                    //Give the spaceship the boost relative to the type
                    switch (boost)
                    {
                        case BoostType.maxSpeed:
                            spaceship.maxSpeed += .1f;
                            break;
                        case BoostType.speedBoost:
                            spaceship.currentSpeed *= 2;
                            break;
                        case BoostType.stop:
                            spaceship.currentSpeed = 0;
                            break;
                    }
                    
                    //Set that the boost has been used
                    active = false;
                }
            }
        }

        public void update()
        {
            //Decrease the amount of time for the boost to exist
            timeActive -= .1f;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            //The spaceship image
            Texture2D boostImage = null;

            //Test the type of boost
            //Set the image relative to the type
            switch (boost)
            {
                case BoostType.maxSpeed:
                    boostImage = spriteLoader.Sprites["maxSpeed"];
                    break;
                case BoostType.speedBoost:
                    boostImage = spriteLoader.Sprites["speedBoost"];
                    break;
                case BoostType.stop:
                    boostImage = spriteLoader.Sprites["stopBoost"];
                    break;
            }

            //Test if the boost image exists and that the boost is active
            //Draw the boost
            if (boostImage != null &&
                active)
            {
                spriteBatch.Draw(boostImage, rectangle, Color.White);
            }
        }
    }
}
