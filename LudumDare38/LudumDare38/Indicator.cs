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
        public int rank { get; set; }
        public float currentSpeed { get; set; }
        public float baseSpeed { get; set; }
        public int points { get; set; }
        public Color color { get; set; }
        public List<Color> kills { get; set; }

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
            kills = spaceship.kills;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Set the image of the indicator
            //Draw the indicator
            Texture2D image = spriteLoader.Sprites["playerInfo"];
            spriteBatch.Draw(image, rectangle, color);

            //Display the information about the player
            spriteBatch.DrawString(spriteLoader.font, "Rank: " + rank, new Vector2(rectangle.X + 10, rectangle.Y + 10), Color.White);
            spriteBatch.DrawString(spriteLoader.font, "Points: " + points, new Vector2(rectangle.X + 10, rectangle.Y + 10 + 20), Color.White);
            spriteBatch.DrawString(spriteLoader.font, "Speed: " + Math.Round(currentSpeed, 1) + "/" + Math.Round(baseSpeed, 1) + " mph", new Vector2(rectangle.X + 10, rectangle.Y + 10 + 40), Color.White);
            spriteBatch.DrawString(spriteLoader.font, "Kills: " + kills.Count, new Vector2(rectangle.X + 10, rectangle.Y + 10 + 60), Color.White);

            //Display the player kills with the respective color
            int killNum = 0;
            foreach (Color kill in kills)
            {
                if (killNum < 15)
                {
                    spriteBatch.Draw(spriteLoader.Sprites["spaceship"], new Rectangle(rectangle.X + 10 + 15 * killNum, rectangle.Y + 10 + 80 + 15 * (killNum / 15), 10, 10), kill);
                } else if (killNum < 30)
                {
                    spriteBatch.Draw(spriteLoader.Sprites["spaceship"], new Rectangle(rectangle.X + 10 + 15 * (killNum - 15), rectangle.Y + 10 + 80 + 15, 10, 10), kill);
                } else if (killNum < 45)
                {
                    spriteBatch.Draw(spriteLoader.Sprites["spaceship"], new Rectangle(rectangle.X + 10 + 15 * (killNum - 30), rectangle.Y + 10 + 80 + 30, 10, 10), kill);
                } 

                //Increase the kill number to draw the sprite in the right position
                killNum++;
            }
        }
    }
}
