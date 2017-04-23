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
    class GameSetup
    {
        //List of buttons in this menu
        //The position in the list of buttons
        List<Button> buttons = new List<Button>();
        int menuPosition = 0;

        public int players { get; set; }
        public int computers { get; set; }
        public int winAmount { get; set; }
        public string winCondition { get; set; }

        //Sprites
        SpriteLoader spriteLoader = SpriteLoader.Loader;

        //The previous state of the keyboard
        KeyboardState prevState;

        public GameSetup()
        {
            players = 2;
            computers = 0;
            winAmount = 10;
            winCondition = "Points";

            //The width, height and position of the button
            int width = 400;
            int height = 150;
            int x = (int)spriteLoader.WindowSize.X / 2;
            int y = (int)spriteLoader.WindowSize.Y / 2;

            //Add the four different buttons to the list of button in this menu
            buttons.Add(new Button(new Rectangle(x - width / 2, (int)(y - height * 3), width, height), "Players"));
            buttons.Add(new Button(new Rectangle(x - width / 2, (int)(y - height * 2), width, height), "Computers"));
            buttons.Add(new Button(new Rectangle(x - width / 2, (int)(y - height), width, height), "Win Condition"));
            buttons.Add(new Button(new Rectangle(x - width / 2, (int)(y), width, height), "Win Amount"));
            buttons.Add(new Button(new Rectangle(x - width / 2, (int)(y + height), width, height), "Controls"));
            buttons.Add(new Button(new Rectangle(x - width / 2, (int)(y + height * 2), width, height), "Start Game"));
        }

        public void Update(Game1 game, Gamestate gamestate)
        {
            //Get keyboard input
            //The amount of keys being pressed at once
            KeyboardState keyState = Keyboard.GetState();

            //Test for a key input to navagate the buttons
            if ((keyState.IsKeyDown(Keys.S) ||
                keyState.IsKeyDown(Keys.Down)) &&
                prevState != keyState)
            {
                menuPosition++;
            }
            else if ((keyState.IsKeyDown(Keys.W) ||
                keyState.IsKeyDown(Keys.Up)) &&
                prevState != keyState)
            {
                menuPosition--;
            }
            else if ((keyState.IsKeyDown(Keys.A) ||
                keyState.IsKeyDown(Keys.Left)) &&
                prevState != keyState)
            {
                if (menuPosition == 0)
                {
                    if (players <= 2)
                    {
                        players = 2;
                    }
                    else
                    {
                        players--;
                    }
                }
                else if (menuPosition == 1)
                {
                    if (computers <= 0)
                    {
                        computers = 0;
                    }
                    else
                    {
                        computers--;
                    }
                }
                else if (menuPosition == 3)
                {
                    if (winAmount <= 1)
                    {
                        winAmount = 1;
                    }
                    else
                    {
                        winAmount--;
                    }
                }
            }
            else if ((keyState.IsKeyDown(Keys.D) ||
                keyState.IsKeyDown(Keys.Right)) &&
                prevState != keyState)
            {
                if (menuPosition == 0)
                {
                    if (players >= 8)
                    {
                        players = 8;
                    }
                    else
                    {
                        players++;
                    }
                }
                else if (menuPosition == 1)
                {
                    if (computers >= players)
                    {
                        computers = players;
                    }
                    else
                    {
                        computers++;
                    }
                }
                else if (menuPosition == 3)
                {
                    if (winAmount >= 30)
                    {
                        winAmount = 30;
                    }
                    else
                    {
                        winAmount++;
                    }
                }
            }

            //Make sure the hovering position is in the list of buttons
            if (menuPosition < 0)
            {
                menuPosition = buttons.Count - 1;
            }
            else if (menuPosition >= buttons.Count)
            {
                menuPosition = 0;
            }

            //Set all the bottons to not be hovered over
            foreach (Button button in buttons)
            {
                button.hovering = false;
            }

            //Set the one button that is being hovered over to being hovered
            buttons[menuPosition].hovering = true;

            //Test for an action to happen on the current button
            //Start the key binding process
            //Display a prompt to the player(s)
            if ((keyState.IsKeyDown(Keys.Space) ||
                keyState.IsKeyDown(Keys.Enter)) &&
              prevState != keyState)
            {
                //Test the current menu that is being hovered over
                //Change the gamestate relative to the menu
                switch (menuPosition)
                {
                    case 2:
                        if (gamestate.winState == Gamestate.winCondition.Kills)
                        {
                            gamestate.winState = Gamestate.winCondition.Points;
                            winCondition = "Points";
                        }
                        else if (gamestate.winState == Gamestate.winCondition.Points)
                        {
                            gamestate.winState = Gamestate.winCondition.Wins;
                            winCondition = "Wins";
                        }
                        else
                        {
                            gamestate.winState = Gamestate.winCondition.Kills;
                            winCondition = "Kills";
                        }
                        break;
                    case 4:
                        gamestate.currentState = Gamestate.state.controls;
                        gamestate.prevState = Gamestate.state.gameSetup;
                        break;
                    case 5:
                        game.Reset();
                        gamestate.currentState = Gamestate.state.game;
                        gamestate.prevState = Gamestate.state.game;
                        break;
                }
            }

            //Set the previous key state to the current key state
            prevState = keyState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //The image of the button
            //The font
            Texture2D image = spriteLoader.Sprites["button"];
            SpriteFont font = spriteLoader.font;
            int buttonNum = 0;

            //Get information about all of the buttons
            foreach (Button button in buttons)
            {
                //If the button is being hovered over
                //Draw the button green
                //Else draw it white
                if (button.hovering)
                {
                    spriteBatch.Draw(image, button.position, Color.Green);
                }
                else
                {
                    spriteBatch.Draw(image, button.position, Color.White);
                }

                float fontScale = 2;

                //Display the text in the center of the button
                if (buttonNum == 0)
                {
                    string text = button.text + ": " + players + "/8";
                    spriteBatch.DrawString(font, text, new Vector2(button.position.X + button.position.Width / 2 - font.MeasureString(text).X * fontScale / 2, button.position.Y + button.position.Height / 2 - font.MeasureString(text).Y * fontScale / 2), Color.White, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
                }
                else if (buttonNum == 1)
                {
                    string text = button.text + ": " + computers + "/" + players;
                    spriteBatch.DrawString(font, text, new Vector2(button.position.X + button.position.Width / 2 - font.MeasureString(text).X * fontScale / 2, button.position.Y + button.position.Height / 2 - font.MeasureString(text).Y * fontScale / 2), Color.White, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
                }
                else if (buttonNum == 2)
                {
                    string text = button.text + ": " + winCondition;
                    spriteBatch.DrawString(font, text, new Vector2(button.position.X + button.position.Width / 2 - font.MeasureString(text).X * fontScale / 2, button.position.Y + button.position.Height / 2 - font.MeasureString(text).Y * fontScale / 2), Color.White, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
                }
                else if (buttonNum == 3)
                {
                    string text = button.text + ":\n";
                    text = text + (winAmount * 1000) + " " + winCondition;
                    fontScale = 1.5f;

                    spriteBatch.DrawString(font, text, new Vector2(button.position.X + button.position.Width / 2 - font.MeasureString(text).X * fontScale / 2, button.position.Y + button.position.Height / 2 - font.MeasureString(text).Y * fontScale / 2), Color.White, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.DrawString(font, button.text, new Vector2(button.position.X + button.position.Width / 2 - font.MeasureString(button.text).X * 4 / 2, button.position.Y + button.position.Height / 2 - font.MeasureString(button.text).Y * 4 / 2), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
                }

                buttonNum++;
            }
        }
    }
}
