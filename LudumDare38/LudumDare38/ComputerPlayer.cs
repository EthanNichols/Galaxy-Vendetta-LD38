using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
    class ComputerPlayer
    {
        Spaceship spaceShip;
        public bool active { get; set; }

        public ComputerPlayer(Spaceship setSpaceship)
        {
            spaceShip = setSpaceship;
            active = true;
        }

        public void Update(Random random, List<Ring> rings)
        {
            int move = random.Next(random.Next(2000));

            if (spaceShip.IsComputer)
            {
                if (move == 0 &&
                    spaceShip.currentRing == spaceShip.movementRing)
                {
                    spaceShip.movementRing--;

                    if (spaceShip.movementRing < 1)
                    {
                        spaceShip.movementRing = 1;
                    }
                }
                else if (move == 1 &&
                  spaceShip.currentRing == spaceShip.movementRing)
                {
                    spaceShip.movementRing++;

                    if (spaceShip.movementRing > rings.Count)
                    {
                        spaceShip.movementRing = rings.Count;
                    }
                }
            }
        }
    }
}
