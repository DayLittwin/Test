using System;
using System.Collections.Generic;
using System.Text;

namespace LiarsDice
{
    class Player
    {
        int[] dice = new int[5];
        bool human = false;
        int numOfDice = 5;

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

