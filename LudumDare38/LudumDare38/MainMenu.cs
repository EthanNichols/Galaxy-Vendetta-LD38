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
    class MainMenu
    {
        //List of buttons in this menu
        //The position in the list of buttons
        List<Button> buttons = new List<Button>();
        int menuPosition = 0;

        //Sprites
        SpriteLoader spriteLoader = SpriteLoader.Loader;

        //The previous state of the keyboard
        KeyboardState prevState;

        public MainMenu()
        {
            //The width, height and position of the button
            int width = 500;
            int height = 250;
            int x = (int)spriteLoader.WindowSize.X / 2;
            int y = (int)spriteLoader.WindowSize.Y / 2;

            //Add the four different buttons to the list of button in this menu
            buttons.Add(new Button(new Rectangle(x - width / 2, (int)(y - height * 2), width, height), "Play Game"));
            buttons.Add(new Button(new Rectangle(x - width / 2, (int)(y - height), width, height), "Controls"));
            buttons.Add(new Button(new Rectangle(x - width / 2, (int)(y), width, height), "Options"));
            buttons.Add(new Button(new Rectangle(x - width / 2, (int)(y + height), width, height), "Quit"));
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
                    case 0:
                        gamestate.currentState = Gamestate.state.gameSetup;
                        gamestate.prevState = Gamestate.state.mainMenu;
                        break;
                    case 1:
                        gamestate.currentState = Gamestate.state.controls;
                        gamestate.prevState = Gamestate.state.mainMenu;
                        break;
                    case 2:

                        break;
                    case 3:
                        game.ExitGame();
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

                //Display the text in the center of the button
                spriteBatch.DrawString(font, button.text, new Vector2(button.position.X + button.position.Width / 2 - font.MeasureString(button.text).X * 5 / 2, button.position.Y + button.position.Height / 2 - font.MeasureString(button.text).Y * 5 / 2), Color.White, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
            }
        }
    }
}
