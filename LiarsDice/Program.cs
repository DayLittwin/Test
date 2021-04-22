using System;
using System.Collections;
using System.Threading;

namespace LiarsDice
{
    class Program
    {
        ///<summary>
        ///The main program for Liar's Dice.
        ///</summary>
        static void Main(string[] args)
        {
            int num = 0;

            //Determine quantity of players between 2 and 4.
            //Make sure the user does not enter something invalid.
            do
            {
                Console.WriteLine("How many players?");
                string val = Console.ReadLine();
                Int32.TryParse(val, out num);

                if (num < 2 || num > 4)
                {
                    Console.WriteLine("ERROR: Please enter a valid integer between 2 and 4 inclusively.");
                }

            } while (num < 2 || num > 4);

            Game game = new Game(num);

            //Determine who goes first by randomly assigning human.
            //Array spot 0 will go first. (EX. If human is 0, human will go first)

            Random rnd = new Random();
            int human = rnd.Next(game.getNumOfPlayers() - 1); // produce number between 0 and numOfPlayers-1, giving array spot.
            game.setHuman(human);

            Console.WriteLine("You are Player " + (human + 1));

            //StartGame
            startRound(game);
            while (!gameEnd(game))
            {
                increaseTurn(game);
                continueGame(game);
            }
            endScreen(game);
        }

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

                //If the player had earlier decided to leave the game without seeing the outcome, it will not show who won.
                if (game.getNumOfPlayers() != -1)
                {
                    Console.WriteLine("Player " + game.getPlayer(0).getPlayerNumber() + " wins!");
                }
            }

            game.sleep();
        }

        ///<summary>
        ///Rolls the dice for each player.
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
        ///Starts the round of the game, specifying a new bet.
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
            }
        }

        ///<summary>
        ///The human starts the round by making a bet.
        ///</summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void humanStart(Game game)
        {
            //First, display the player's dice.
            Console.WriteLine("\nYour dice.");
            showDice(game.getPlayer(game.getTurn()));

            String inputbetQuantity = String.Empty, inputbetFace = String.Empty;
            int convertedbetQuantity = 0, convertedbetFace = 0;

            Console.WriteLine("Please enter your new bet.");

            //Get the user's new bet. 
            //Make sure the user does not input an invalid entry.
            do
            {
                Console.Write("The quantity of dice in your bet: ");
                inputbetQuantity = Console.ReadLine();
                Int32.TryParse(inputbetQuantity, out convertedbetQuantity);

                if (convertedbetQuantity <= 0)
                {
                    Console.WriteLine("ERROR: Please enter a valid integer greater than 0.");
                }

            } while (convertedbetQuantity <= 0);

            do
            {
                Console.Write("The face of the dice in your bet: ");
                inputbetFace = Console.ReadLine();
                Int32.TryParse(inputbetFace, out convertedbetFace);
                
                if (convertedbetFace < 1 || convertedbetFace > 6)
                {
                    Console.WriteLine("ERROR: Please enter a valid integer between 1 and 6 inclusively.");
                }

            } while (convertedbetFace < 1 || convertedbetFace > 6);

            //Set the new bet.
            game.setbetFace(convertedbetFace);
            game.setbetQuantity(convertedbetQuantity);

            //Display what the bet is.
            System.Console.WriteLine("You made the bet of at least " + game.getbetQuantity() + " \"" + game.getbetFace() + "\"'s.");

        }

        /// <summary>
        /// The computer starts the round by making a bet.
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

            //Set the new bet.
            game.setbetFace(maxNumOfOccurences);
            game.setbetQuantity(numOfOccurences[maxNumOfOccurences]);

            //Display the new bet.
            System.Console.WriteLine("Player " + game.getPlayer(game.getTurn()).getPlayerNumber() + " made the bet of at least " + game.getbetQuantity() + " \"" + game.getbetFace() + "\"'s.");
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
            //Display the current user's dice.
            Console.WriteLine("\nYour dice.");
            showDice(game.getPlayer(game.getTurn()));
            String answer = String.Empty;

            //Get answer from user.
            //Make sure the user does not input something invalid.
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
                String inputbetQuantity = String.Empty, inputbetFace = String.Empty;
                int convertedbetQuantity = 0, convertedbetFace = 0;

                Console.WriteLine("Please enter your new bet.");

                //Get the new bet from the user.
                //Make sure the user does not input an invalid entry.
                do
                {
                    Console.Write("The quantity of dice in your bet: ");
                    inputbetQuantity = Console.ReadLine();
                    Int32.TryParse(inputbetQuantity, out convertedbetQuantity);

                    Console.Write("The face of the dice in your bet: ");
                    inputbetFace = Console.ReadLine();
                    Int32.TryParse(inputbetFace, out convertedbetFace);

                    if (convertedbetFace < 1 || convertedbetFace > 6)
                    {
                        Console.WriteLine("ERROR: Please enter a valid integer between 1 and 6 inclusively.");
                    }
                    else if (convertedbetFace <= game.getbetFace() && convertedbetQuantity <= game.getbetQuantity())
                    {
                        Console.WriteLine("ERROR: Increase bet num, bet amount, or both.");
                    }

                } while (convertedbetFace < 1 || convertedbetFace > 6 || (convertedbetFace <= game.getbetFace() && convertedbetQuantity <= game.getbetQuantity()));

                //Set new bet.
                game.setbetFace(convertedbetFace);
                game.setbetQuantity(convertedbetQuantity);

                //Display new bet.
                System.Console.WriteLine("You made the bet of at least " + game.getbetQuantity() + " \"" + game.getbetFace() + "\"'s.");
            }

        }

        /// <summary>
        /// The ai decides whether to increase bet or challenge the current bet using probability checks.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static void ai(Game game)
        {
            //If the probability is greater than 60%, then the ai will make a new bet.
            if (probCheck(game, game.getbetQuantity(), game.getbetFace()) > 60)
            {
                //Find which face current player has the most of.
                //If the current player has equal amounts, increase the current bet?
                //Choose the highest numbers with the most amount of instances
                int[] numOfOccurences = new int[7];
                int maxNumOfOccurences = game.getPlayer(game.getTurn()).getDice(0);

                for (int i = 0; i < game.getPlayer(game.getTurn()).getNumOfDice(); i++)
                {
                    numOfOccurences[game.getPlayer(game.getTurn()).getDice(i)]++;
                    if (numOfOccurences[game.getPlayer(game.getTurn()).getDice(i)] >= maxNumOfOccurences)
                    {
                        maxNumOfOccurences = game.getPlayer(game.getTurn()).getDice(i);
                    }
                }

                //The maxNumOfOccurences is equal to the current betFace
                if (maxNumOfOccurences == game.getbetFace() && game.getbetQuantity() + 1 < game.getTotalDice())
                {
                    game.setbetQuantity(game.getbetQuantity() + 1);
                }
                //The maxNumOfOccurences is less than the current betFace 
                else if (maxNumOfOccurences < game.getbetFace() && game.getbetQuantity() + 1 < game.getTotalDice())
                {
                    game.setbetFace(maxNumOfOccurences);
                    game.setbetQuantity(game.getbetQuantity() + 1);
                }
                //The maxNumOfOccurences is greater than the current betFace
                else if (game.getbetFace() + 1 <= 6)
                {
                    game.setbetFace(game.getbetFace() + 1);
                }

                //Display new bet.
                System.Console.WriteLine("Player " + game.getPlayer(game.getTurn()).getPlayerNumber() + " made the bet of at least " + game.getbetQuantity() + " \"" + game.getbetFace() + "\"'s.");
            }

            //If the probabililty is less than 60%, then the ai will challenge.
            else
            {
                challenge(game);
            }
        }

        /// <summary>
        /// Determines the probability of the current bet.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        /// <param name="r">The current betQuantity.</param>
        /// <param name="findDie">The current betFace.</param>
        /// <returns>The probability as a percent.</returns>
        static double probCheck(Game game, int r, int findDie)
        {
            //Find the number of dice our current player has that match the betFace.
            int numFound = 0;
            for (int i = 0; i < game.getPlayer(game.getTurn()).getNumOfDice(); i++)
            {

                if (game.getPlayer(game.getTurn()).getDice(i) == findDie)
                {
                    numFound++;
                }
            }

            //Remove the numFound from our betQuantity. 
            r -= numFound;

            //If the player has more of the betFace than bet, then the probability percent can be returned as 100%.
            if (r <= 0)
            {
                return 100;
            }

            //Remove the numFound from out totalDice.
            int n = game.getTotalDice() - numFound;

            //Multiplies the binomialDistribution by 100 to get the percent.
            return 100 * binomialDistribution(Convert.ToDouble(r), Convert.ToDouble(n));
        }

        /// <summary>
        /// Takes the binomialDistribution of r and n to find the probability (as a decimal) for the current bet.
        /// </summary>
        /// <param name="r">Number of events to obtain. This is equal to the total number of dice in the game minus the amount of said die our
        /// current player has. </param>
        /// <param name="n">Numbers of trials. This is equal to the quantity of dice that needs to be found, minus the amount of said die our
        /// current player has. </param>
        /// <returns>A decimal probability.</returns>
        static public double binomialDistribution(double r, double n)
        {
            double answer = 0;
            double p = 0.16666666666;
            double q = 1 - p;
            while (r < n)
            {
                answer += C(n,r) * Math.Pow(p, r) * Math.Pow(q, (n - r));
                r++;
            }

            return answer;
        }

        /// <summary>
        /// Calculates the number of combinations.
        /// </summary>
        /// <param name="n">Number of items in the set.</param>
        /// <param name="r">Number of items selected from the set.</param>
        /// <returns></returns>
        static public double C(double n, double r)
        {
            return (factorial(n) / (factorial(r) * (factorial(n - r))));
        }

        /// <summary>
        /// Calculates the factorial
        /// </summary>
        /// <param name="num">The number that needs it's factorial calculated</param>
        /// <returns>Returns the factorial</returns>
        static public double factorial(double num)
        {
            double result = 1;
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
            String input = String.Empty;

            //Display who is challenging.
            if (game.getPlayer(game.getTurn()).isHuman())
            {
                Console.WriteLine("\nYOU ARE CHALLENGING!");
            }
            else
            {
                Console.WriteLine("\nPlayer " + game.getPlayer(game.getTurn()).getPlayerNumber() + " IS CHALLENGING!");
            }

            //Display every player's dice. Include a nap delay to allow the user to understand what is happening.
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

            Console.WriteLine();

            //Count each instance of game.betFace in each player.
            int betFaceFnd = 0;
            for (int i = 0; i < game.getNumOfPlayers(); i++)
            {
                for (int j = 0; j < game.getPlayer(i).getNumOfDice(); j++)
                {
                    if (game.getPlayer(i).getDice(j) == game.getbetFace())
                    {
                        betFaceFnd++;
                    }
                }
            }

            //If there is more than or an equal amount of the face than the current betQuantity, then the challenger loses a die.
            if (betFaceFnd >= game.getbetQuantity())
            {
                //Removes the die.
                game.getPlayer(game.getTurn()).removeDie();

                //Displays who lost the die.
                if (game.getPlayer(game.getTurn()).isHuman())
                {
                    Console.WriteLine("You lose a die!\n");
                }
                else
                {
                    Console.WriteLine("Player " + game.getPlayer(game.getTurn()).getPlayerNumber() + " loses a die!\n");
                }

                //If the current player has no more die, they are out of the game.
                if (game.getPlayer(game.getTurn()).getNumOfDice() == 0)
                {
                    //Displays the user who is out of the game.
                    if (game.getPlayer(game.getTurn()).isHuman())
                    {
                        Console.WriteLine("You are out of the game!\n");

                        //If there is more than 1 player left after removing human player, ask if the player wants to watch 
                        //the ai finish the game.
                        if (game.getNumOfPlayers() - 1 > 1)
                        {
                            do
                            {
                                Console.WriteLine("Do you want to watch the rest of the game? (Y/N): ");
                                input = Console.ReadLine().ToLower();
                                if (input != "y" && input != "n")
                                {
                                    Console.WriteLine("ERROR: Input must be either Y or N, Non-case sensitive.");
                                }
                            } while (input != "y" && input != "n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Player " + game.getPlayer(game.getTurn()).getPlayerNumber() + " is out of the game!\n");
                    }

                    //Removes player who is out of the game.
                    game.removePlayer(game.getTurn());
                }
            }

            //There is less than the amount of betQuantity within everyone's dice, so the person who just made a bet loses a die.
            else
            {
                int lastPlayer = game.getTurn() - 1;
                if (lastPlayer < 0)
                {
                    lastPlayer = game.getNumOfPlayers() - 1;
                }

                //Display who lost a die.
                if (game.getPlayer(lastPlayer).isHuman())
                {
                    Console.WriteLine("You lose a die!\n");
                }
                else
                {
                    Console.WriteLine("Player " + game.getPlayer(lastPlayer).getPlayerNumber() + " loses a die!\n");
                }

                //Remove the die.
                game.getPlayer(lastPlayer).removeDie();

                //If the player no longer has any die, they are out of the game.
                if (game.getPlayer(lastPlayer).getNumOfDice() == 0)
                {
                    //Display who is out of the game.
                    if (game.getPlayer(lastPlayer).isHuman())
                    {
                        Console.WriteLine("You are out of the game!\n");

                        //If there is more than 1 player left after removing human player, ask if the player wants to watch 
                        //the ai finish the game.
                        if (game.getNumOfPlayers() - 1 > 1)
                        {
                            do
                            {
                                Console.WriteLine("Do you want to watch the rest of the game? (Y/N): ");
                                input = Console.ReadLine().ToLower();
                                if (input != "y" && input != "n")
                                {
                                    Console.WriteLine("ERROR: Input must be either Y or N, Non-case sensitive.");
                                }
                            } while (input != "y" && input != "n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Player " + game.getPlayer(lastPlayer).getPlayerNumber() + " is out of the game!\n");
                    }

                    //Remove the player.
                    game.removePlayer(lastPlayer);
                }
            }

            if (input == "n")
            {
                forceEnd(game);
            }

            //If we're not at the end of the game, we reset the betQuantity and betFace before starting a new round.
            if (!gameEnd(game))
            {
                game.setbetQuantity(0);
                game.setbetFace(0);
                if (game.getTurn() >= game.getNumOfPlayers())
                {
                    game.setTurn(game.getTurn() - 1);
                }
                startRound(game);
            }
        }

        /// <summary>
        /// If the player doesn't want to watch the rest of the game after losing, this will make the game immediately end without
        /// seeing the outcome.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void forceEnd(Game game)
        {
            game.setNumOfPlayers(-1);
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

