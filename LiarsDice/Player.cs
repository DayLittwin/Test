using System;
using System.Collections.Generic;
using System.Text;

namespace LiarsDice
{
    class Player
    {
        int[] dice = new int[5];
        bool human;
        int numOfDice;
        int playerNumber;

        public Player()
        {
            for (int i = 0; i < 5; i++)
            {
                dice[i] = 0;
            }
            human = false;
            numOfDice = 5;
        }

        public Player(int num)
        {
            for (int i = 0; i < 5; i++)
            {
                dice[i] = 0;
            }
            human = false;
            numOfDice = 5;
            playerNumber = num;
        }

        public int getPlayerNumber()
        {
            return playerNumber;
        }

        public void setHuman()
        {
            human = true;
        }

        public void setDice()
        {
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                dice[i] = rnd.Next(1, 6);
            }
        }

        public bool isHuman()
        {
            return human;
        }

        public int getDice(int i)
        {
            return dice[i];
        }

        public int[] getDiceArray()
        {
            return dice;
        }

        public int getNumOfDice()
        {
            return numOfDice;
        }

        public void removeDie()
        {
            numOfDice -= 1;
            int[] dice = new int[numOfDice];
        }
    }
}

