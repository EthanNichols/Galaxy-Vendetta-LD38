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
    class Controls
    {
        //Local list of spaceships
        //List of buttons in this menu
        //The position in the list of buttons
        List<Spaceship> spaceships = new List<Spaceship>();
        List<Button> buttons = new List<Button>();
        int menuPosition = 0;

        //Sprites
        SpriteLoader spriteLoader = SpriteLoader.Loader;
        
        //The previous state of the keyboard
        KeyboardState prevState;

        //Whether key binding are being setup or not
        //Whether the in-wards or out-wards button is being setup
        //The prompt to tell the player what to do
        bool setUpBindings;
        bool inButtonSetup;
        string prompt;

        public Controls(List<Spaceship> spaceshipList)
        {
            //Store a local list of all of the spaceships
            spaceships = spaceshipList;

            //The height and width of the button
            int height = (int)spriteLoader.WindowSize.Y / 8;
            int width = (int)(height * 1.8f);
            Rectangle rectangle;

            //Create all 8 buttons for the different players
            for (int i = 0; i < 8; i++)
            {
                //Calculate the position of the button
                rectangle = new Rectangle(height / 2, height / 2 + (height * 2) * (i), width, height);

                //Set the other four buttons to the right side of the play area
                if (i > 3)
                {
                    rectangle = new Rectangle((int)(spriteLoader.WindowSize.X - width - height / 2), height / 2 + (height * 2) * (i - 4), width, height);
                }

                //Add the buttons to the list of buttons
                buttons.Add(new Button(rectangle, "Controls for\nSpaceship " + i));
            }

            //Set that there is no key binding happening
            setUpBindings = false;
            inButtonSetup = false;
            prompt = "";
        }

        public void ResetMenu()
        {
            //Set all the buttons to not be hovered
            foreach (Button button in buttons)
            {
                button.hovering = false;
            }

            //Set the position of the hovering menu to 0
            //Set the button to be hovering over
            menuPosition = 0;
            buttons[menuPosition].hovering = true;
        }

        public void Update()
        {
            //Get keyboard input
            //The amount of keys being pressed at once
            KeyboardState keyState = Keyboard.GetState();
            Keys[] keysPressed = keyState.GetPressedKeys();

            //test if there is a different keystate
            //Test if a key binding is happening
            //Make sure only one key is being pressed
            //Setup the key binding
            if (keyState != prevState &&
                setUpBindings &&
                keysPressed.Count() == 1)
            {
                KeyboardBinding(keyState);
                return;
            }

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
                setUpBindings = true;
                inButtonSetup = true;
                prompt = "Press the key you want to move your spaceship out-wards";
            }

            //Set the previous keystate to the current keystate
            prevState = keyState;
        }

        public void KeyboardBinding(KeyboardState keyState)
        {
            //An array of all the buttons being pressed
            Keys[] keysPressed = keyState.GetPressedKeys();

            //Make sure only one button is being pressed
            //Check if the in button hasn't been setup
            if (keysPressed.Count() == 1 &&
                inButtonSetup)
            {
                //Set the move in-wards button relative to the keypress
                //Set that the ou-ward button needs to be setup
                //Set the prompt to display to the player(s)
                spaceships[menuPosition].moveIn = keysPressed[0];
                inButtonSetup = false;
                prompt = "Press the key you want to move your spaceship in-wards";
                return;

            }

            //Make sure only one button is being pressed
            //Check if the in button has been setup
            else if (keysPressed.Count() == 1 &&
              !inButtonSetup)
            {
                //Set the move out-wards button relative to the keypress
                //Set that the key binding process is over
                //Set the prompt to display to the player(s)
                spaceships[menuPosition].moveOut = keysPressed[0];
                setUpBindings = false;
                prompt = "";
                return;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //The image of the button
            //The font
            Texture2D image = spriteLoader.Sprites["button"];
            SpriteFont font = spriteLoader.font;

            //Get information about all the buttons
            foreach (Button button in buttons)
            {
                //Draw the button green if it's being hovered over
                //Else just draw it white
                if (button.hovering)
                {
                    spriteBatch.Draw(image, button.position, Color.Green);
                }
                else
                {
                    spriteBatch.Draw(image, button.position, Color.White);
                }

                //Display the text of the button in the center of the button
                spriteBatch.DrawString(font, button.text, new Vector2(button.position.X + button.position.Width / 2 - font.MeasureString(button.text).X * 2 / 2, button.position.Y + button.position.Height / 2 - font.MeasureString(button.text).Y * 2 / 2), Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
            }

            //Test if there is prompt for the player
            //Display the prompt
            if (prompt != "")
            {
                spriteBatch.DrawString(font, prompt, new Vector2(spriteLoader.WindowSize.X / 2 - font.MeasureString(prompt).X * 3 / 2, spriteLoader.WindowSize.Y / 2 - font.MeasureString(prompt).Y * 3 / 2), Color.White, 0, Vector2.Zero, 3f, SpriteEffects.None, 0);
            }

            //Display what the option menu does
            string text = "Change the controls for specific players";
            spriteBatch.DrawString(font, text, new Vector2(spriteLoader.WindowSize.X / 2 - font.MeasureString(text).X * 3 / 2, 100 - font.MeasureString(text).Y * 3 / 2), Color.White, 0, Vector2.Zero, 3f, SpriteEffects.None, 0);
        }
    }
}
