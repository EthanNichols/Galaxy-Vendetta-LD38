using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class Indicator
    {
        //The position and size of the spaceship
        public Rectangle rectangle { get; set; }
        public bool active { get; set; }
        public int indicatorNumber { get; set; }

        //The amount of points the spaceship has
        //The color of the spaceship
        public float currentSpeed { get; set; }
        public float baseSpeed { get; set; }
        public int points { get; set; }
        public Color color { get; set; }

        //Sprites
        SpriteLoader spriteLoader = SpriteLoader.Loader;

        public Indicator(int indicatorNum, Color setColor)
        {
            //Set the indicator number relative to the spaceship number
            indicatorNumber = indicatorNum;

            //The height and width of the indicators
            int height = (int)spriteLoader.WindowSize.Y / 8;
            int width = (int)(height * 1.8f);

            //Set the color relative to the spaceship color
            //Set the position and size of the indicator
            color = setColor;
            rectangle = new Rectangle(height / 2, height / 2 + (height * 2) * (indicatorNum - 1), width, height);

            //Set the other four indicators to the right side of the play area
            if (indicatorNum > 4)
            {
                rectangle = new Rectangle((int)(spriteLoader.WindowSize.X - width - height / 2), height / 2 + (height * 2) * (indicatorNum - 5), width, height);
            }
        }

        public void Update(Spaceship spaceship)
        {
            //Set the current information based off the spaceships
            points = spaceship.points;
            currentSpeed = spaceship.currentSpeed;
            baseSpeed = spaceship.maxSpeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Set the image of the indicator
            //Draw the indicator
            Texture2D image = spriteLoader.Sprites["playerInfo"];
            spriteBatch.Draw(image, rectangle, color);

            //Display the information about the player
            spriteBatch.DrawString(spriteLoader.font, "Points: " + points, new Vector2(rectangle.X + 10, rectangle.Y + 10), Color.White);
            spriteBatch.DrawString(spriteLoader.font, "Speed: " + Math.Round(currentSpeed, 1) + "/" + Math.Round(baseSpeed, 1), new Vector2(rectangle.X + 10, rectangle.Y + 10 + 20), Color.White);
        }
    }
}
