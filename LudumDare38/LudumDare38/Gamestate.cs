using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class Gamestate
    {
        //The current state the game is in
        public state currentState { get; set; }

        //The different states the game can be in
        public enum state
        {
            mainMenu,
            pause,
            options,
            game,
            winningScreen
        }

        public Gamestate()
        {
            //Start the game in the 'game' state
            currentState = state.game;
        }
    }
}
