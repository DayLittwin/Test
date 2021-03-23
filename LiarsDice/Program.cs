using System;
using System.Collections;

namespace LiarsDice
{
    class Program
    {
        static void Main(string[] args)
        {

            Game game;
            //First, how many players?
            //Determine by Starting UI 
            //This part is for testing until we get the UI number from Unity 
            string val;
            Console.WriteLine("How many players?"); //Between 2-4 (2 being 1 computer, 1 human)
            val = Console.ReadLine();
            game.setNumOfPlayers(Convert.ToInt32(val));

            //Determine who goes first by randomly assigning human.
            //Array spot 0 will go first. (EX. If human is 0, human will go first)

            Random rnd = new Random();
            int human = rnd.Next(game.getNumOfPlayers() - 1); // produce number between 0 and numOfPlayers-1, giving array spot.
            game.setHuman(human);

            //StartGame
            /*Takes the array of players, starting at 0. First turn goes to index 0.
              After setting bet, calls NextTurn. The next player will be index 1. 
              Next player can then increase amt of dice or val of dice. 
              Human and AI functionality for each will be different.*/
            startRound(game);
            //endScreen(game);
        }

        static public void rollDice(Game game)
        {
            for (int i = 0; i < game.getNumOfPlayers(); i++)
            {
                game.getPlayer(i).setDice();
            }
        }

        static public void startRound(Game game)
        {
            rollDice(game);
            if (game.isHuman(game.getTurn()))
            {
                //humanStart();
            }
            else
            {
                computerStart(game, game.getPlayer(game.getTurn()));
            }
            continueGame(game);
        }

        static public void computerStart(Game game, Player player)
        {
            //Choose the highest numbers with the most amount of instances
            int[] numOfOccurences = new int[7];
            int maxNumOfOccurences = player.getDice(0);

            for (int i = 0; i < player.getNumOfDice(); i++)
            {
                numOfOccurences[player.getDice(i)]++;
                if (numOfOccurences[player.getDice(i)] >= maxNumOfOccurences)
                {
                    maxNumOfOccurences = player.getDice(i);
                }
            }
            game.setBetNum(maxNumOfOccurences);
            game.setBetAmt(numOfOccurences[maxNumOfOccurences]);

            increaseTurn(game);
        }

        static public void increaseTurn(Game game)
        {
            if (game.getTurn() < game.getNumOfPlayers() - 1)
            {
                game.setTurn(game.getTurn() + 1);
            }
            else
            {
                game.setTurn(game.getTurn() - game.getNumOfPlayers());
            }
        }

        static public void continueGame(Game game)
        {
            if (game.isHuman(game.getTurn()))
            {
                // program human 
            }
            else
            {
                ai(game);
            }
            increaseTurn(game);
        }

        static void ai(Game game)
        {
            ai(game);
            //check the probablility of the current bet. Is it reasonable?
            if (probCheck(game, game.getBetAmt(), game.getBetNum()) > 65)
            {
                //higher quantity of the same face (like 9 “3’s” instead of 7 “3’s”) 
                //same quantity with a higher face (like 7 “4’s” instead of 7 “3’s”).
                //how many of betNum does current player have?
                int betAmtFnd = 0;
                for (int i = 0; i < game.getPlayer(game.getTurn()).getNumOfDice(); i++)
                {
                    if (game.getPlayer(game.getTurn()).getDice(i) == game.getBetNum())
                    {
                        betAmtFnd++;
                    }
                }

                if (game.getBetAmt() < betAmtFnd && game.getBetAmt() + 1 < game.getTotalDice())
                {
                    game.setBetAmt(game.getBetAmt() + 1);
                }
                else
                {
                    if (game.getBetAmt() > game.getTotalDice())
                    {
                        //challenge();
                    }
                    else if (probCheck(game, game.getBetAmt() + 1, game.getBetNum()) < probCheck(game, game.getBetAmt(), game.getBetNum() + 1) && game.getBetNum() != 6)
                    {
                        game.setBetNum(game.getBetNum() + 1);
                    }
                    else
                    {
                        game.setBetAmt(game.getBetAmt() + 1);
                    }
                }
            }
            else
            {
                //challenge(game);
            }
        }

        static double probCheck(Game game, int k, int findDie)
        {
            //k will be edited depending on how many die current player has.
            //how many of findDie does current player have?
            int numFound = 0;
            for (int i = 0; i < game.getPlayer(game.getTurn()).getNumOfDice(); i++)
            {
                if (game.getPlayer(game.getTurn()).getDice(i) == findDie)
                {
                    numFound++;
                }
            }

            k -= numFound;

            if (k <= 0)
            {
                return 100;
            }


            int n = game.getTotalDice(); //rolling n dice
            int dice = n - k; //obtaining exactly k that are of a certain value

            double probUnsure = 5 / 6;
            double probFound = 1 / 6;
            double probability = 0;

            //Checks the probability of having "exactly" the amount of k in totalDice
            //Then, increases k and adds the next "exactly"
            //adding like this finds the probability of having "at least" the amount of k in totalDice
            while (k < game.getTotalDice())
            {
                probability = +((factorial(n)) / (factorial(k) * factorial(n - k))) * (Math.Pow(probFound, (k * Math.Pow(probUnsure, (dice)))));
                k++;
            }

            return probability;
        }

        static public int factorial(int num)
        {
            int result = 1;
            while (num != 1)
            {
                result *= num;
                num--;
            }

            return result;
        }

        /*static public void continueGame(Player[] players, int i)
        {
            Player currentPlayer;
            while (!endGame(players))
            {
                currentPlayer = players[i];
                if (currentPlayer.isHuman())
                {
                    //run human method/function
                    humanMove();
                }
                else
                {
                    computerMove(currentPlayer);
                }

                i = nextPlayer(players, i);
            }
        }
        static public bool endGame(Player[] players)
        {
            if (players[1] == null)
            {
                return true;
            }
            return false;
        }

        static public int nextPlayer(Player[] players, int i)
        {
            if (players[i+1]  == null)
            {
                if (players[0] == players[i])
                {
                    //end game
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return i + 1;
            }
            
        }*/

        static public void challenge(Game game)
        {
            //count each instance of game.betNum in each player.
            int betNumFnd = 0;
            for (int i = 0; i < game.getNumOfPlayers(); i++)
            {
                for (int j = 0; j < game.getPlayer(i).getDice(j); j++)
                {
                    if (game.getPlayer(i).getDice(j) == game.getBetNum())
                    {
                        betNumFnd++;
                    }
                }
            }

            if (betNumFnd > game.getBetAmt())
            {
                game.getPlayer(game.getTurn()).removeDie();
                if (game.getPlayer(game.getTurn()).getNumOfDice() == 0)
                {
                    game.removePlayer(game.getTurn());
                    game.setTurn(game.getTurn() + 1);
                }
            }
            else
            {
                int lastPlayer = game.getTurn() - 1;
                if (lastPlayer < 0)
                {
                    lastPlayer = game.getNumOfPlayers() - 1;
                }
                game.getPlayer(lastPlayer).removeDie();
                if (game.getPlayer(game.getTurn()).getNumOfDice() == 0)
                {
                    game.removePlayer(lastPlayer);
                    game.setTurn(game.getTurn()); 
                }
            }
            if (!gameEnd(game))
            {
                startRound(game);
            }
        }

        static bool gameEnd(Game game)
        {
            if (game.getNumOfPlayers() > 1)
            {
                return false;
            }
            return true;
        }
    }
}

