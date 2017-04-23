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
        //The previous state the game was in
        //The win condition for the game
        public state currentState { get; set; }
        public state prevState { get; set; }
        public winCondition winState { get; set; }

        //The different states the game can be in
        public enum state
        {
            mainMenu,
            pause,
            controls,
            game,
            winningScreen,
            gameSetup
        }

        //The condition that determines who wins and ranks
        public enum winCondition
        {
            Kills,
            Points,
            Wins
        }

        public Gamestate()
        {
            //Start the game in the 'game' state
            //Set the current win condition
            currentState = state.mainMenu;
            prevState = state.mainMenu;
            winState = winCondition.Points;
        }
    }
}
