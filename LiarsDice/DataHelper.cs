using System;
using System.Collections.Generic;
using System.Text;

namespace LiarsDice
{
    public class DataHelper
    {
        /// <summary>
        /// Outputs "You win!" or "You lose!" depending on if the last player is the human player.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void endScreen(Game game)
        {
            if (game.getPlayer(0).isHuman())
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lose!");
            }

            game.sleep();
        }

        ///<summary>
        ///Rolls the dice for each player
        ///</summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void rollDice(Game game)
        {
            for (int i = 0; i < game.getNumOfPlayers(); i++)
            {
                game.getPlayer(i).setDice();
            }
        }

        ///<summary>
        ///Starts the round of the game, specifying a new bet
        ///</summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void startRound(Game game)
        {
            rollDice(game);
            if (game.isHuman(game.getTurn()))
            {
                humanStart(game);
            }
            else
            {
                computerStart(game, game.getPlayer(game.getTurn()));
                game.sleep();
            }
        }

        ///<summary>
        /// Shows the users dice on the console then asks for their bet amount and bet face number. Once bet is received it sets
        /// the bet number and bet amount and shows on the console the users bet.
        ///</summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void humanStart(Game game)
        {
            Console.WriteLine("\nYour dice.");
            showDice(game.getPlayer(game.getTurn()));

            String inputBetAmt = String.Empty, inputBetNum = String.Empty;
            int convertedBetAmt = 0, convertedBetNum = 0;

            Console.WriteLine("Please enter the face of the dice you're betting and the amount of that face.");

            do
            {
                Console.Write("Bet Amount: ");
                inputBetAmt = Console.ReadLine();
                Int32.TryParse(inputBetAmt, out convertedBetAmt);

                if (convertedBetAmt <= 0)
                {
                    Console.WriteLine("ERROR: Please enter a valid integer greater than 0.");
                }

            } while (convertedBetAmt <= 0);

            do
            {
                Console.Write("Bet Number (face): ");
                inputBetNum = Console.ReadLine();
                Int32.TryParse(inputBetNum, out convertedBetNum);
                
                if (convertedBetNum < 1 || convertedBetNum > 6)
                {
                    Console.WriteLine("ERROR: Please enter a valid integer between 1 and 6 inclusively.");
                }

            } while (convertedBetNum < 1 || convertedBetNum > 6);

            game.setBetNum(convertedBetNum);
            game.setBetAmt(convertedBetAmt);

            System.Console.WriteLine("You made the bet of at least " + game.getBetAmt() + " \"" + game.getBetNum() + "\"'s.");

        }

        /// <summary>
        /// The computer starts the round by making a bet
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        /// <param name="player">Contains all information about the current player. Is part of class Player.</param>
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

            System.Console.WriteLine("Player " + game.getPlayer(game.getTurn()).getPlayerNumber() + " made the bet of at least " + game.getBetAmt() + " \"" + game.getBetNum() + "\"'s.");
        }

        ///<summary>
        ///Increments the turn.
        ///If at the end of the array, it will reset the turn count to 0.
        ///</summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void increaseTurn(Game game)
        {
            if (game.getTurn() < game.getNumOfPlayers() - 1)
            {
                game.setTurn(game.getTurn() + 1);
            }
            else
            {
                game.setTurn(game.getTurn() - game.getNumOfPlayers() + 1);
            }
        }

        ///<summary>
        ///Continues the round after the bet has been made.
        ///</summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void continueGame(Game game)
        {
            if (game.isHuman(game.getTurn()))
            { 
                human(game);
            }
            else
            {
                ai(game);
                game.sleep();
            }
        }

        static public void human(Game game)
        {
            Console.WriteLine("\nYour dice.");
            showDice(game.getPlayer(game.getTurn()));
            String answer = String.Empty;

            do
            {
                Console.WriteLine("Do you wish to Challenge? (Y/N)");
                answer = Console.ReadLine().ToLower();
                if (answer != "n" && answer != "y")
                {
                    Console.WriteLine("ERROR: Input must be either Y or N, Non-case sensitive.");
                }
            } while (answer != "n" && answer != "y");

            if (answer.Equals("Y") || answer.Equals("y"))
            {
                challenge(game);
            }
            else
            {
                String inputBetAmt = String.Empty, inputBetNum = String.Empty;
                int convertedBetAmt = 0, convertedBetNum = 0;

                Console.WriteLine("Please enter a new bet with a higher face or higher amount.");

                do
                {
                    Console.Write("Bet Amount: ");
                    inputBetAmt = Console.ReadLine();
                    Int32.TryParse(inputBetAmt, out convertedBetAmt);

                    Console.Write("Bet Number (face): ");
                    inputBetNum = Console.ReadLine();
                    Int32.TryParse(inputBetNum, out convertedBetNum);

                    if (convertedBetNum < 1 || convertedBetNum > 6)
                    {
                        Console.WriteLine("ERROR: Please enter a valid integer between 1 and 6 inclusively.");
                    }
                    else if (convertedBetNum <= game.getBetNum() && convertedBetAmt <= game.getBetAmt())
                    {
                        Console.WriteLine("ERROR: Increase bet num, bet amount, or both.");
                    }

                } while (convertedBetNum < 1 || convertedBetNum > 6 || (convertedBetNum <= game.getBetNum() && convertedBetAmt <= game.getBetAmt()));

                game.setBetNum(convertedBetNum);
                game.setBetAmt(convertedBetAmt);

                System.Console.WriteLine("You made the bet of at least " + game.getBetAmt() + " \"" + game.getBetNum() + "\"'s.");
            }

        }

        /// <summary>
        /// The ai decides whether to increase bet or challenge the current bet using probability checks.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static void ai(Game game)
        {
            //ai(game);
            //check the probability of the current bet. Is it reasonable?
            if (probCheck(game, game.getBetAmt(), game.getBetNum()) > 50)
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
                        challenge(game);
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
                System.Console.WriteLine("Player " + game.getPlayer(game.getTurn()).getPlayerNumber() + " made the bet of at least " + game.getBetAmt() + " \"" + game.getBetNum() + "\"'s.");
            }
            else
            {
                challenge(game);
            }
        }

        /// <summary>
        /// Determines the probability of the current bet.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        /// <param name="k">The current betAmt</param>
        /// <param name="findDie">The current betNum</param>
        /// <returns></returns>
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
                probability += ((factorial(n)) / (factorial(k) * factorial(dice))) * (Math.Pow(probFound, (k * Math.Pow(probUnsure, (dice)))));
                k++;
            }

            return probability;
        }

        /// <summary>
        /// Calculates the factorial
        /// </summary>
        /// <param name="num">The number that needs it's factorial calculated</param>
        /// <returns>Returns the factorial</returns>
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

        /// <summary>
        /// Challenges the current bet. 
        /// If the current bet is true, then the one who challenges loses a dice.
        /// If the current bet is false, then the person who made the bet loses a dice.
        /// If the person who loses a dice loses all of their dice, they are out of the game.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void challenge(Game game)
        {
            if (game.getPlayer(game.getTurn()).isHuman())
            {
                Console.WriteLine("\nYOU ARE CHALLENGING!");
            }
            else
            {
                Console.WriteLine("\nPlayer " + game.getPlayer(game.getTurn()).getPlayerNumber() + " IS CHALLENGING!");
            }

             for (int i = 0; i < game.getNumOfPlayers(); i++) {
                if (game.getPlayer(i).isHuman())
                {
                    Console.WriteLine("Your dice.");
                }
                else
                {
                    Console.WriteLine("Player " + game.getPlayer(i).getPlayerNumber() + "\'s dice.");
                }
                showDice(game.getPlayer(i));
                game.nap();
             }

            //count each instance of game.betNum in each player.
            int betNumFnd = 0;
            for (int i = 0; i < game.getNumOfPlayers(); i++)
            {
                for (int j = 0; j < game.getPlayer(i).getNumOfDice(); j++)
                {
                    if (game.getPlayer(i).getDice(j) == game.getBetNum())
                    {
                        betNumFnd++;
                    }
                }
            }

            if (betNumFnd >= game.getBetAmt())
            {
                game.getPlayer(game.getTurn()).removeDie();
                if (game.getPlayer(game.getTurn()).isHuman())
                {
                    Console.WriteLine("You lose a die!\n");
                }
                else
                {
                    Console.WriteLine("Player " + game.getPlayer(game.getTurn()).getPlayerNumber() + " loses a die!\n");
                }
                if (game.getPlayer(game.getTurn()).getNumOfDice() == 0)
                {
                    if (game.getPlayer(game.getTurn()).isHuman())
                    {
                        Console.WriteLine("You are out of the game!");
                    }
                    else
                    {
                        Console.WriteLine("Player " + game.getPlayer(game.getTurn()).getPlayerNumber() + " is out of the game!");
                    }
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
                if (game.getPlayer(lastPlayer).isHuman())
                {
                    Console.WriteLine("You lose a die!\n");
                }
                else
                {
                    Console.WriteLine("Player " + game.getPlayer(lastPlayer).getPlayerNumber() + " loses a die!\n");
                }
                game.getPlayer(lastPlayer).removeDie();
                if (game.getPlayer(lastPlayer).getNumOfDice() == 0)
                {
                    if (game.getPlayer(lastPlayer).isHuman())
                    {
                        Console.WriteLine("You are out of the game!");
                    }
                    else
                    {
                        Console.WriteLine("Player " + game.getPlayer(lastPlayer).getPlayerNumber() + " is out of the game!");
                    }
                    game.removePlayer(lastPlayer);
                    game.setTurn(game.getTurn()+1); 
                }
            }
            if (!gameEnd(game))
            {
                game.setBetAmt(0);
                game.setBetNum(0);
                startRound(game);
            }
        }

        /// <summary>
        /// Prints out the current player's dice side by side
        /// </summary>
        /// <param name="player">Contains all information about the current player. Is part of class Player.</param>
        static public void showDice(Player player)
        {
            String dieEmpty = "|   | ";
            String dieOneEye = "| * | ";
            String dieEdge = "+---+ ";
            String dieTwoEyes = "|* *| ";
            String dieThreeEyes = "|***| ";

            String lineOne = "";
            String lineTwo = "";
            String lineThree = "";
            String lineFour = "";
            String lineFive = "";

            for (int i = 0; i < player.getNumOfDice(); i++)
            {
                lineOne += dieEdge;
                lineFive += dieEdge;
                switch (player.getDice(i))
                {
                    case 1:
                        lineTwo += dieEmpty;
                        lineThree += dieOneEye;
                        lineFour += dieEmpty;
                        break;
                    case 2:
                        lineTwo += dieEmpty;
                        lineThree += dieTwoEyes;
                        lineFour += dieEmpty;
                        break;
                    case 3:
                        lineTwo += dieEmpty;
                        lineThree += dieThreeEyes;
                        lineFour += dieEmpty;
                        break;
                    case 4:
                        lineTwo += dieTwoEyes;
                        lineThree += dieEmpty;
                        lineFour += dieTwoEyes;
                        break;
                    case 5:
                        lineTwo += dieTwoEyes;
                        lineThree += dieOneEye;
                        lineFour += dieTwoEyes;
                        break;
                    case 6:
                        lineTwo += dieThreeEyes;
                        lineThree += dieEmpty;
                        lineFour += dieThreeEyes;
                        break;
                    default:
                        Console.WriteLine("Error encountered!");
                        break;
                }
            }
            Console.WriteLine(lineOne);
            Console.WriteLine(lineTwo);
            Console.WriteLine(lineThree);
            Console.WriteLine(lineFour);
            Console.WriteLine(lineFive);
        }

        /// <summary>
        /// Determines if the game has ended by checking how many players are left.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        /// <returns>True if there is only one player left. False if there is more than one player left.</returns>
        public static bool gameEnd(Game game)
        {
            if (game.getNumOfPlayers() > 1)
            {
                return false;
            }
            return true;
        }
    }
}
