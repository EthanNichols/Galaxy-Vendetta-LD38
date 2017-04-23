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
        public int wins { get; set; }
        public int points { get; set; }
        public Color color { get; set; }
        public List<Color> kills { get; set; }

        public string moveInKey { get; set; }
        public string moveOutKey { get; set; }

        //Sprites
        SpriteLoader spriteLoader = SpriteLoader.Loader;

        public Indicator(int indicatorNum, Color setColor)
        {
            //Set the indicator number relative to the spaceship number
            indicatorNumber = indicatorNum;
            active = false;

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

        public void Update(Spaceship spaceship, int spaceships)
        {
            //Set the current information based off the spaceships
            points = spaceship.points;
            wins = spaceship.wins;
            kills = spaceship.kills;

            moveInKey = spaceship.moveIn.ToString();
            moveOutKey = spaceship.moveOut.ToString();

            color = spaceship.color;

            if (indicatorNumber <= spaceships)
            {
                active = true;
            } else
            {
                active = false;
            }

            if (active &&
                !spaceship.active)
            {
                color = new Color(spaceship.color.R / 5, spaceship.color.G / 5, spaceship.color.B / 5);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Set the image of the indicator
            //The font
            //Draw the indicator
            Texture2D image = spriteLoader.Sprites["playerInfo"];
            SpriteFont font = spriteLoader.font;

            if (active)
            {
                spriteBatch.Draw(image, rectangle, color);

                //The image for the button controls
                //The size of the button controls indicator
                image = spriteLoader.Sprites["buttonControl"];
                int size = rectangle.Height / 2 - 5;

                //All of the indicators on the left side
                if (indicatorNumber <= 4)
                {
                    //Calculate the position and size of the top control button indicator
                    //Calculate the position and size of the bottom control button indicator
                    Rectangle topControl = new Rectangle(rectangle.X + rectangle.Width + 10, rectangle.Y, size, size);
                    Rectangle bottomControl = new Rectangle(rectangle.X + rectangle.Width + 10, rectangle.Y + rectangle.Height / 2 + 5, size, size);

                    //Draw the button control indicators
                    spriteBatch.Draw(image, topControl, color);
                    spriteBatch.Draw(image, bottomControl, color);

                    //Display the move in key and move out key in the respective indicators
                    spriteBatch.DrawString(font, moveInKey, new Vector2((int)(topControl.X + topControl.Width / 2 - font.MeasureString(moveInKey).X * 3 / 2), (int)(topControl.Y + topControl.Height / 2 - font.MeasureString(moveInKey).Y * 3 / 2)), Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, moveOutKey, new Vector2((int)(bottomControl.X + bottomControl.Width / 2 - font.MeasureString(moveOutKey).X * 3 / 2), (int)(bottomControl.Y + bottomControl.Height / 2 - font.MeasureString(moveOutKey).Y * 3 / 2)), Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
                }
                //All of the indicators on the right side
                else
                {
                    //Calculate the position and size of the top control button indicator
                    //Calculate the position and size of the bottom control button indicator
                    Rectangle topControl = new Rectangle(rectangle.X - size - 15, rectangle.Y, rectangle.Height / 2, rectangle.Height / 2 - 5);
                    Rectangle bottomControl = new Rectangle(rectangle.X - size - 15, rectangle.Y + rectangle.Height / 2 + 5, rectangle.Height / 2, rectangle.Height / 2 - 5);

                    //Draw the button control indicators
                    spriteBatch.Draw(image, topControl, color);
                    spriteBatch.Draw(image, bottomControl, color);

                    //Display the move in key and move out key in the respective indicators
                    spriteBatch.DrawString(font, moveInKey, new Vector2((int)(topControl.X + topControl.Width / 2 - font.MeasureString(moveInKey).X * 3 / 2), (int)(topControl.Y + topControl.Height / 2 - font.MeasureString(moveInKey).Y * 3 / 2)), Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
                    spriteBatch.DrawString(font, moveOutKey, new Vector2((int)(bottomControl.X + bottomControl.Width / 2 - font.MeasureString(moveOutKey).X * 3 / 2), (int)(bottomControl.Y + bottomControl.Height / 2 - font.MeasureString(moveOutKey).Y * 3 / 2)), Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
                }

                //Display the information about the player
                spriteBatch.DrawString(font, "Rank: " + rank, new Vector2(rectangle.X + 10, rectangle.Y + 10), Color.White);
                spriteBatch.DrawString(font, "Wins: " + wins, new Vector2(rectangle.X + 10, rectangle.Y + 10 + 20), Color.White);
                spriteBatch.DrawString(font, "Score: " + points, new Vector2(rectangle.X + 10, rectangle.Y + 10 + 40), Color.White);
                spriteBatch.DrawString(font, "Kills: " + kills.Count, new Vector2(rectangle.X + 10, rectangle.Y + 10 + 60), Color.White);

                //Display the player kills with the respective color
                int killNum = 0;
                foreach (Color kill in kills)
                {
                    if (killNum < 15)
                    {
                        spriteBatch.Draw(spriteLoader.Sprites["spaceship"], new Rectangle(rectangle.X + 10 + 15 * killNum, rectangle.Y + 10 + 80 + 15 * (killNum / 15), 10, 10), kill);
                    }
                    else if (killNum < 30)
                    {
                        spriteBatch.Draw(spriteLoader.Sprites["spaceship"], new Rectangle(rectangle.X + 10 + 15 * (killNum - 15), rectangle.Y + 10 + 80 + 15, 10, 10), kill);
                    }
                    else if (killNum < 45)
                    {
                        spriteBatch.Draw(spriteLoader.Sprites["spaceship"], new Rectangle(rectangle.X + 10 + 15 * (killNum - 30), rectangle.Y + 10 + 80 + 30, 10, 10), kill);
                    }

                    //Increase the kill number to draw the sprite in the right position
                    killNum++;
                }
            }
        }
    }
}
