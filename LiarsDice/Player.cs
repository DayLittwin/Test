using System;
using System.Collections.Generic;
using System.Text;

namespace LiarsDice
{
    public class Player
    {
        int[] dice = new int[5];
        bool human;
        int numOfDice;
        int playerNumber;

        /// <summary>
        /// Player constructor. Takes input num and assigns the player with the playerNumber of num.
        /// Initializes human to false, numOfDice to 5, and each die to 0.
        /// (Requirement 3.1.2) Each player should be given five dice to start.
        /// </summary>
        /// <param name="num">Variable to be assigned to playerNum.</param>
        public Player(int num)
        {
            for (int i = 0; i < 5; i++)
            {
                dice[i] = 0;
            }
            human = false;

            //(Requirement 3.1.2) Each player should be given five dice to start.
            numOfDice = 5;
            playerNumber = num;
        }

        /// <summary>
        /// Returns the playerNumber
        /// </summary>
        /// <returns>Returns the playerNumber</returns>
        public int getPlayerNumber()
        {
            return playerNumber;
        }

        /// <summary>
        /// Sets the variable human to true.
        /// (Requirement 3.2.1) Players will be set as Human or AI.
        /// </summary>
        public void setHuman()
        {
            human = true;
        }

        /// <summary>
        /// Randomizes each die to faces between 1 and 6 inclusively.
        /// </summary>
        public void setDice()
        {
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                dice[i] = rnd.Next(1, 7);
            }
        }

        /// <summary>
        /// Returns false if the current player is not an ai/computer.
        /// </summary>
        /// <returns>Returns true if current player is the human. Returns false if the current player is the ai/computer.</returns>
        public bool isHuman()
        {
            return human;
        }

        /// <summary>
        /// Returns the dice face of array spot i in dice[].
        /// </summary>
        /// <param name="i">The index of the array.</param>
        /// <returns>The dice face of the index provided.</returns>
        public int getDice(int i)
        {
            return dice[i];
        }

        /// <summary>
        /// Returns the variable numOfDice
        /// </summary>
        /// <returns>Returns the variable numOfDice</returns>
        public int getNumOfDice()
        {
            return numOfDice;
        }

        /// <summary>
        /// Removes a die whenever the player loses a bet.
        /// (Requirement 3.1.4) Dice will be removed by computer.
        /// </summary>
        public void removeDie()
        {
            numOfDice -= 1;
            int[] dice = new int[numOfDice];
        }
    }
}

