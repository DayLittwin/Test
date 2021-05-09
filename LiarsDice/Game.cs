using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LiarsDice

{
    public class Game
    {
        Player[] players = new Player[4];
        int numOfPlayers;
        int betQuantity; //the countable instances of the bet
        int betFace; //the "face" value shown on the dice/bet
        int turn; //starts at 0 for turn one, goes up to 3
        int totalDice;
        static int SLEEPTIME = 2500;
        static int NAPTIME = 1500;

        /// <summary>
        /// Game constructor. Takes input num as the number of players.
        /// It sets numOfPlayers to num, initializes the players[] array with a new Player until i reaches numOfPlayers.
        /// Initializes betQuantity, betFace, and turn to 0.
        /// Sets totalDice to the numOfPlayers * 5.
        /// </summary>
        /// <param name="num">The number of players.</param>
        public Game(int num)
        {
            numOfPlayers = num;
            for (int i = 0; i < numOfPlayers; i++)
            {
                players[i] = new Player(i+1);
            }
            betQuantity = 0;
            betFace = 0;
            turn = 0;
            totalDice = numOfPlayers*5;
        }

        /// <summary>
        /// Delays the game to provide for easier viewing for the user.
        /// It does this by putting the thread to sleep for the designated time SLEEPTIME (in ms)
        /// </summary>
        public void sleep()
        {
            Thread.Sleep(SLEEPTIME);
        }

        /// <summary>
        /// Delays the game to provide for easier viewing, but for less time than sleep.
        /// It does this by putting the thread to sleep for the designated time NAPTIME (in ms)
        /// </summary>
        public void nap()
        {
            Thread.Sleep(NAPTIME);
        }

        /// <summary>
        /// Sets turn to turnNum
        /// </summary>
        /// <param name="turnNum">Turn number provided</param>
        public void setTurn(int turnNum)
        {
            turn = turnNum;
        }

        /// <summary>
        /// Sets human to true inside the Player class.
        /// </summary>
        /// <param name="i">The index of the array</param>
        public void setHuman(int i)
        {
            players[i].setHuman();
        }

        /// <summary>
        /// Sets the current betQuantity to the amt provided.
        /// </summary>
        /// <param name="amt">The new betQuantity</param>
        public void setbetQuantity(int amt)
        {
            betQuantity = amt;
        }

        /// <summary>
        /// Sets the current betFace to the num provided.
        /// </summary>
        /// <param name="num">The new betFace</param>
        public void setbetFace(int num)
        {
            betFace = num;
        }

        /// <summary>
        /// Returns the numOfPlayers
        /// </summary>
        /// <returns>The number of players in the game.</returns>
        public int getNumOfPlayers()
        {
            return numOfPlayers;
        }

        /// <summary>
        /// Sets the current numOfPlayers to the num provided.
        /// </summary>
        /// <param name="num">The new numOfPlayers</param>
        public void setNumOfPlayers(int num)
        {
            numOfPlayers = num;
        }

        /// <summary>
        /// Returns the betQuantity
        /// </summary>
        /// <returns>The current betQuantity equal to countable instances of a die</returns>
        public int getbetQuantity()
        {
            return betQuantity;
        }

        /// <summary>
        /// Returns the betFace
        /// </summary>
        /// <returns>The current betFace equal to the face of the die</returns>
        public int getbetFace()
        {
            return betFace;
        }

        /// <summary>
        /// Returns the Player of players[i]. Allows for use of the Player class functions.
        /// </summary>
        /// <param name="i">Index of the array</param>
        /// <returns>A Player object</returns>
        public Player getPlayer(int i)
        {
            return players[i];
        }

        /// <summary>
        /// Returns the index of the current Player in players[i]
        /// </summary>
        /// <returns>The index of the current Player in the array players[i]</returns>
        public int getTurn()
        {
            return turn;
        }

        /// <summary>
        /// Returns the totalDice
        /// </summary>
        /// <returns>The total amount of dice in the game.</returns>
        public int getTotalDice()
        {
            return totalDice;
        }

        /// <summary>
        /// Returns false if the current player is not an ai/computer.
        /// </summary>
        /// <param name="i">The index of the players[i] array</param>
        /// <returns>Returns true if the player at index i is the human player. False if current player is the ai/computer.</returns>
        public bool isHuman(int i)
        {
            return players[i].isHuman();
        }

        /// <summary>
        /// Removes a player who has run out of all their dice, ie. has 0 dice remaining.
        /// Reformats the players[] array to avoid NullPointerException.
        /// If the spot provided is 0, at the beginning of the array, it will move all players back one, then sets the last player 
        /// in the array to null.
        /// If the spot provided it numOfPlayers - 1, the end of the array, it will simply set the last player to null.
        /// Otherwise, the spot is somewhere in the middle of the array. Therefore, it will move the players after the spot
        /// back one and set the last player in the array to null.
        /// Lastly, it decrements the numOfPlayers by 1.
        /// (Requirement 3.2.5)	Players will be removed by computer.
        /// (Requirement 3.3.3) AI is removed by computer.
        /// </summary>
        /// <param name="spot">The index of the Player to be removed in players[]</param>
        public void removePlayer(int spot)
        {
            if (spot == 0)
            {
                for (int i = 0; i < numOfPlayers - 1; i++)
                {
                    players[i] = players[i + 1];
                }
                players[numOfPlayers - 1] = null;
            }
            else if (spot == numOfPlayers - 1)
            {
                players[spot] = null;
            }
            else
            {
                for (int i = spot; i < numOfPlayers - 1; i++)
                {
                    players[i] = players[i + 1];
                }
                players[numOfPlayers - 1] = null;
            }
            numOfPlayers--;
        }
    }
}
