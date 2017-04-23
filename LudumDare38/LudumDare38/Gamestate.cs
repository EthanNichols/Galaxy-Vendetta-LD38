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
            winningScreen
        }

        //The condition that determines who wins and ranks
        public enum winCondition
        {
            kills,
            points,
            wins
        }

        public Gamestate()
        {
            //Start the game in the 'game' state
            //Set the current win condition
            currentState = state.mainMenu;
            prevState = state.mainMenu;
            winState = winCondition.wins;
        }
    }
}
