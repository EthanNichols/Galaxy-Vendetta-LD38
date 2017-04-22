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
        //The position and size of the spaceship
        public Rectangle rectangle { get; set; }
        public bool active { get; set; }

        //The rotation location of the spaceship
        //The ring that the spaceship it on
        //The speed the spaceship is going
        public float rotation { get; set; }
        public int currentRing { get; set; }
        public int movementRing { get; set; }
        public float offset { get; set; }
        public float currentSpeed { get; set; }
        public float maxSpeed { get; set; }

        //The amount of points the spaceship has
        //The color of the spaceship
        public int points { get; set; }
        public Color color { get; set; }

        //The key that moves the spaceship in or out
        public Keys moveIn { get; set; }
        public Keys moveOut { get; set; }
        public KeyboardState previousState { get; set; }

        //Sprites
        private SpriteLoader spriteLoader = SpriteLoader.Loader;

        public Spaceship(List<Ring> rings, int spaceshipNum, int spaceships)
        {
            //Set information about the spaceship
            rotation = (360 / spaceships) * spaceshipNum;
            currentRing = (rings.Count + 1) / 2;
            movementRing = currentRing;
            currentSpeed = 0;
            maxSpeed = 1;
            points = 0;
            active = true;

            moveIn = Keys.Z;
            moveOut = Keys.X;

            //Set the color of the spaceship
            switch (spaceshipNum)
            {
                case 1:
                    color = new Color(255, 0, 0);
                    break;
                case 2:
                    color = new Color(255, 255, 0);
                    break;
                case 3:
                    color = new Color(0, 255, 0);
                    break;
                case 4:
                    color = new Color(0, 255, 255);
                    break;
                case 5:
                    color = new Color(0, 0, 255);
                    break;
                case 6:
                    color = new Color(255, 255, 255);
                    break;
            }
        }

        public void Collision(List<Spaceship> spaceships)
        {
            //Get information about all of the spaceships
            foreach (Spaceship spaceship in spaceships)
            {

                Rectangle shrinkCurrent = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 2, rectangle.Height / 2);
                Rectangle shrinkTest = new Rectangle(spaceship.rectangle.X, spaceship.rectangle.Y, spaceship.rectangle.Width / 2, spaceship.rectangle.Height / 2);
                //Make sure the same spaceship isn't being tested
                //Test if there is an intersection with another spaceship
                //Make sure that spaceship is active
                if (this != spaceship &&
                    shrinkCurrent.Intersects(shrinkTest) &&
                    spaceship.active)
                {
                    //Test for the spaceship with more rotation
                    //Deactivate the spaceship that is in front
                    if (rotation > spaceship.rotation)
                    {
                        active = false;
                    }
                    else
                    {
                        spaceship.active = false;
                    }
                }
            }
        }

        public void Update(List<Ring> rings)
        {
            //All the rings that the spaceship can be on
            Ring ring = rings[currentRing - 1];
            Ring moveToRing = rings[movementRing - 1];
            //Calculate the size of the spaceship
            int size = (int)(spriteLoader.WindowSize.Y / (rings.Count() * 5));

            if (ring.size < moveToRing.size)
            {
                offset += currentSpeed * 15;

                if ((ring.size + offset - 2) / 2 > moveToRing.size / 2)
                {
                    currentRing = movementRing;
                    offset = 0;
                    ring = rings[currentRing - 1];
                }

            } else if (ring.size > moveToRing.size)
            {
                offset -= currentSpeed * 15;

                if ((ring.size + offset - 2) / 2 < moveToRing.size / 2)
                {
                    currentRing = movementRing;
                    offset = 0;
                    ring = rings[currentRing - 1];
                }
            }

            //Calulate the center of the screen
            //Calculate the position of the spaceship on the ring
            //Calculate the actual position of the spaceship
            int centerX = (int)spriteLoader.WindowSize.X / 2;
            int centerY = (int)spriteLoader.WindowSize.Y / 2;
            int circleX = (int)((ring.size + offset) / 2 * Math.Cos(rotation * (Math.PI / 180)));
            int circleY = (int)((ring.size + offset) / 2 * Math.Sin(rotation * (Math.PI / 180)));
            int xPos = centerX - size / 2 + circleX;
            int yPos = centerY - size / 2 + circleY;

            //Set the exact position and size of the spaceship
            rectangle = new Rectangle(xPos, yPos, size, size);

            //TEMPERARY
            //Increase the rotation location of the spaceship
            rotation += currentSpeed / currentRing;

            //TEMPERARY
            //Reset the rotation to avoid bit overflow
            if (rotation >= 360)
            {
                rotation -= 360;
            }

            //Test the current speed relative to the max speed
            //Increase/Decrease the current speed towards the max speed
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += .1f;
            }
            else if (currentSpeed > maxSpeed)
            {
                currentSpeed /= (maxSpeed + .1f);
            }

            //If the current speed is relativly close to the max speed
            //Set the current speed to the max speed
            else if (currentSpeed + .06f > maxSpeed ||
                currentSpeed - .06f < maxSpeed)
            {
                currentSpeed = maxSpeed;
            }

            //Move the spaceship to different rings
            Move(rings);
        }

        private void Move(List<Ring> rings)
        {
            //What keys are currently being pressed
            KeyboardState keystate = Keyboard.GetState();

            //Test if the previous keyboard state is different from the current one
            if (previousState != keystate &&
                currentRing == movementRing)
            {
                //Test if the 'in' or 'out' movement key is pressed
                //Make sure the spaceship will be on a ring
                //Move the spaceship to the new ring
                if (keystate.IsKeyDown(moveOut) &&
                    currentRing < rings.Count)
                {
                    movementRing++;
                }
                else if (keystate.IsKeyDown(moveIn) &&
                  currentRing > 1)
                {
                    movementRing--;
                }
            }

            //Set the previous state to the current state
            previousState = keystate;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //The spaceship image
            Texture2D spaceship = spriteLoader.Sprites["spaceship"];

            //Draw the spaceship
            if (active)
            {
                spriteBatch.Draw(spaceship, rectangle, color);
            }
        }
    }
}
