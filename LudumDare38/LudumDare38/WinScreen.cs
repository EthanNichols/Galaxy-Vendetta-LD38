using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class WinScreen
    {

        List<Indicator> indicators = new List<Indicator>();
        int timer = -10;

        SpriteLoader spriteLoader = SpriteLoader.Loader;

        public WinScreen(List<Indicator> setIndicators)
        {
            indicators = setIndicators;
        }

        public void Update(List<Indicator> setIndicators, Gamestate gamstate)
        {
            indicators = setIndicators;

            if (timer <= 0 &&
                timer != -10)
            {
                timer = -10;
                gamstate.currentState = Gamestate.state.mainMenu;
                gamstate.prevState = gamstate.currentState;
            }

            if (timer == -10)
            {
                timer = 500;
            }

            timer--;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture2D image = spriteLoader.Sprites["playerInfo"];
            SpriteFont font = spriteLoader.font;

            int centerX = (int)spriteLoader.WindowSize.X / 2;
            int centerY = (int)spriteLoader.WindowSize.Y / 2;

            int width = (int)spriteLoader.WindowSize.X / 6;
            int height = (int)(width / 1.8f);

            Rectangle pos = new Rectangle(0, 0, 0, 0);
            List<int> ranks = new List<int>();

            string congrats = "Congratualations, here are the rankings";
            string credits = "Created for Ludum Dare 38: 'A Small World' by: Ethan Nichols";
            spriteBatch.DrawString(font, congrats, new Vector2(centerX - font.MeasureString(congrats).X * 5 / 2, 50), Color.White, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, credits, new Vector2(centerX - font.MeasureString(credits).X * 3 / 2, spriteLoader.WindowSize.Y - 75), Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);

            foreach (Indicator indicator in indicators)
            {
                if (indicator.active)
                {
                    while (ranks.Contains(indicator.rank))
                    {
                        indicator.rank++;
                    }

                    ranks.Add(indicator.rank);

                    switch (indicator.rank)
                    {
                        case 1:
                            pos = new Rectangle(centerX - width / 2, height, width, height);
                            spriteBatch.Draw(image, pos, indicator.color);
                            break;
                        case 2:
                            pos = new Rectangle((int)(centerX - width - width / 2 - 50), (int)(height * 1.5f), width, height);
                            spriteBatch.Draw(image, pos, indicator.color);
                            break;
                        case 3:
                            pos = new Rectangle((int)(centerX + width / 2 + 50), height * 2, width, height);
                            spriteBatch.Draw(image, pos, indicator.color);
                            break;
                        case 4:
                            pos = new Rectangle((int)(centerX - width * 2 - width / 2 - 100), (int)(height * 3.5f), width, height);
                            spriteBatch.Draw(image, pos, indicator.color);
                            break;
                        case 5:
                            pos = new Rectangle((int)(centerX - width - width / 2 - 50), (int)(height * 3.5f), width, height);
                            spriteBatch.Draw(image, pos, indicator.color);
                            break;
                        case 6:
                            pos = new Rectangle((int)(centerX - width / 2), (int)(height * 3.5f), width, height);
                            spriteBatch.Draw(image, pos, indicator.color);
                            break;
                        case 7:
                            pos = new Rectangle((int)(centerX + width / 2 + 50), (int)(height * 3.5f), width, height);
                            spriteBatch.Draw(image, pos, indicator.color);
                            break;
                        case 8:
                            pos = new Rectangle((int)(centerX + (width / 2) * 3 + 100), (int)(height * 3.5f), width, height);
                            spriteBatch.Draw(image, pos, indicator.color);
                            break;
                    }

                    spriteBatch.DrawString(font, "Rank: " + indicator.rank, new Vector2(pos.X + 10, pos.Y + 10), Color.White);
                    spriteBatch.DrawString(font, "Wins: " + indicator.wins, new Vector2(pos.X + 10, pos.Y + 10 + 20), Color.White);
                    spriteBatch.DrawString(font, "Score: " + indicator.points, new Vector2(pos.X + 10, pos.Y + 10 + 40), Color.White);
                    spriteBatch.DrawString(font, "Kills: " + indicator.kills.Count, new Vector2(pos.X + 10, pos.Y + 10 + 60), Color.White);

                    int killNum = 0;

                    foreach (Color kill in indicator.kills)
                    {
                        if (killNum < 15)
                        {
                            spriteBatch.Draw(spriteLoader.Sprites["spaceship"], new Rectangle(pos.X + 10 + 15 * killNum, pos.Y + 10 + 80 + 15 * (killNum / 15), 10, 10), kill);
                        }
                        else if (killNum < 30)
                        {
                            spriteBatch.Draw(spriteLoader.Sprites["spaceship"], new Rectangle(pos.X + 10 + 15 * (killNum - 15), pos.Y + 10 + 80 + 15, 10, 10), kill);
                        }
                        else if (killNum < 45)
                        {
                            spriteBatch.Draw(spriteLoader.Sprites["spaceship"], new Rectangle(pos.X + 10 + 15 * (killNum - 30), pos.Y + 10 + 80 + 30, 10, 10), kill);
                        }

                        //Increase the kill number to draw the sprite in the right position
                        killNum++;
                    }
                }
            }
        }
    }
}
