﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LiarsDice

{
    class Game
    {
        Player[] players = new Player[4];
        int numOfPlayers = 0;
        int betAmt = 0; //the countable instances of the bet
        int betNum = 0; //the value shown on the dice/bet
        int turn = 0; //starts at 0 for turn one, goes up to 3
        int totalDice = 0;

        public void setTurn(int turnNum)
        {
            turn = turnNum;
        }
        public void setHuman(int i)
        {
            players[i].setHuman();
        }

        public void setNumOfPlayers(int num)
        {
            numOfPlayers = num;
            totalDice = numOfPlayers * 5;
        }

        public void setBetAmt(int amt)
        {
            betAmt = amt;
        }

        public void setBetNum(int num)
        {
            betNum = num;
        }
        public int getNumOfPlayers()
        {
            return numOfPlayers;
        }

        public int getBetAmt()
        {
            return betAmt;
        }

        public int getBetNum()
        {
            return betNum;
        }

        public int getNumOfDice(int i)
        {
            return players[i].getNumOfDice();
        }

        public Player getPlayer(int i)
        {
            return players[i];
        }

        public int getTurn()
        {
            return turn;
        }

        public int getTotalDice()
        {
            return totalDice;
        }

        public bool isHuman(int i)
        {
            return players[i].isHuman();
        }
        public void removePlayer(int spot)
        {
            numOfPlayers--;
            if (spot == 0)
            {
                for (int i = 0; i < numOfPlayers; i++)
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
                for (int i = spot; i < numOfPlayers; i++)
                {
                    players[i] = players[i + 1];
                }
                players[numOfPlayers - 1] = null;
            }
        }

    }
}
---