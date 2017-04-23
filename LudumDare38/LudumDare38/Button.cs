using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class Button
    {
        //The position and size of the button
        //The text in the button
        //Whether the button is being hovered over or not
        public Rectangle position { get; set; }
        public string text { get; set; }
        public bool hovering { get; set; }

        public Button(Rectangle setPos, string setText)
        {
            //Set information about the button
            position = setPos;
            text = setText;
            hovering = false;
        }
    }
}
