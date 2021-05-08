using System;
using System.Collections.Generic;
using System.Text;

namespace LiarsDice
{
    public class DataHelper
    {

        /// <summary>
        /// Outputs "You win!" or "You lose!" depending on if the last player is the human player.
        /// If the player had earlier decided to leave the game early without seeing the outcome, it will not display which AI won.
        /// However, if the player decided to watch the game to completion, it will display which Player (AI) won.
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
        ///Randomizes each player's dice as a face between 1 and 6, inclusively.
        ///(Requirement 3.1.3/3.1.3.1)
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
        ///Starts the round of the game which specifies a new bet.
        ///First, it will roll the dice of each player, calling rollDice.
        ///If the current player's human value is set to true, it will continue to humanStart.
        ///Otherwise, it will continue to computerStart.
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
        ///The human starts the round by making a new bet.
        ///First, it will display the player's dice.
        ///Next, it will ask for the user's new bet as the quantity of dice in the bet, and the face of the bet. 
        ///It will bug check to make sure the entry is valid, only numerals greater than zeroand nothing outside of 6 faces.
        ///Then, it will set the current betQuantity and betFace. 
        ///Lastly, it displays the new bet.
        ///(Requirement 3.1.1.2)
        ///</summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void humanStart(Game game)
        {
            ///(Requirement 3.1.1.2)
            //First, display the player's dice.
            Console.WriteLine("\nYour dice.");
            showDice(game.getPlayer(game.getTurn()));

            String inputbetQuantity = String.Empty, inputbetFace = String.Empty;
            int convertedBetQuantity = 0, convertedBetFace = 0;

            Console.WriteLine("Please enter your new bet.");

            //Get the user's new bet. 
            //Make sure the user does not input an invalid entry.
            do
            {
                Console.Write("The quantity of dice in your bet: ");
                inputbetQuantity = Console.ReadLine();
                Int32.TryParse(inputbetQuantity, out convertedBetQuantity);

                if (convertedBetQuantity <= 0)
                {
                    Console.WriteLine("ERROR: Please enter a valid integer greater than 0.");
                }

            } while (convertedBetQuantity <= 0);

            do
            {
                Console.Write("The face of the dice in your bet: ");
                inputbetFace = Console.ReadLine();
                Int32.TryParse(inputbetFace, out convertedBetFace);
                
                if (convertedBetFace < 1 || convertedBetFace > 6)
                {
                    Console.WriteLine("ERROR: Please enter a valid integer between 1 and 6 inclusively.");
                }

            } while (convertedBetFace < 1 || convertedBetFace > 6);

            setAndDisplay(game, convertedBetFace, convertedBetQuantity);

        }

        /// <summary>
        /// Sets the new bet face and bet quantity, then calls display to show the new bet.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        /// <param name="newBetFace">The new bet face to be set.</param>
        /// <param name="newBetQuantity">The new bet quantity to be set.</param>
        public static void setAndDisplay(Game game, int newBetFace, int newBetQuantity)
        {
            //Set the new bet.
            game.setbetFace(newBetFace);
            game.setbetQuantity(newBetQuantity);

            //Display what the bet is.
            display(game);
        }

        /// <summary>
        /// If the current player is human, it will output "You" first.
        /// Otherwise, it will output "Player" and it's playerNumber.
        /// It will then output " made the bet of at least " with the betQuantity and betFace.
        /// Lastly, it will output either a ""." if the betQuantity is only 1.
        /// Or ""'s." if the betQuantity is greater than 1.
        /// And writes a newLine.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void display(Game game)
        {
            // Display what the bet is.
            if (game.getPlayer(game.getTurn()).isHuman())
            {
                System.Console.Write("You");
            }
            else
            {
                System.Console.Write("Player " + game.getPlayer(game.getTurn()).getPlayerNumber());
            }

            System.Console.Write(" made the bet of at least " + game.getbetQuantity() + " \"" + game.getbetFace());

            if (game.getbetQuantity() == 1)
            {
                System.Console.Write("\".");
            }
            else
            {
                System.Console.Write("\"'s.");
            }

            System.Console.Write("\n");
        }

        /// <summary>
        /// The computer starts the round by making a bet.
        /// First, it will determine which face has the most number of occurences within the current player's dice, preferring higher faces.
        /// The variable maxNumOfOccurences is the highest face of the most occurences.
        /// It will then set the new betFace to maxNumOfOccurences, and betQuantity to how many of that face were found in the player's dice.
        /// Lastly, it will display the new bet.
        /// (Requirement 3.3.1)
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

            setAndDisplay(game, maxNumOfOccurences, numOfOccurences[maxNumOfOccurences]);
        }

        ///<summary>
        ///Increments the turn.
        ///If it reachess the end of the array, it will reset the turn count to 0.
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
        ///If the current player's human value is set to true, it will continue to the human function.
        ///Otherwise, it will continue to the ai function.
        ///After calling ai, it will call sleep.
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

        /// <summary>
        /// First, display's the current user's dice.
        /// Next, it will ask the user if it wants to challenge the current bet.
        /// If something other than y/Y/n/N is entered, it will output an error ask the user again.
        /// If the user's answer is "y" or "Y", it will call challenge.
        /// Otherwise, it asks the user to set a new bet with either a higher face, higher quantity, or both.
        /// Error checking makes sure that the new bet is a higher face, higher quantity, or both. 
        /// Error checking will also make sure the new face is between 1 and 6.
        /// The function will then set the new betFace and betQuantity.
        /// Then, displays the new bet.
        /// (Requirement 3.4.2)
        /// (Requirement 3.4.3)
        /// (Requirement 3.4.4)
        /// </summary>
        /// <param name="game"></param>
        static public void human(Game game)
        {
            //Display the current user's dice.
            Console.WriteLine("\nYour dice.");
            showDice(game.getPlayer(game.getTurn()));
            String answer = String.Empty;

            ///(Requirement 3.4.2)
            ///(Requirement 3.4.4)
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

            if (answer.Equals("y"))
            {
                challenge(game);
            }
            else
            {
                String inputbetQuantity = String.Empty, inputbetFace = String.Empty;
                int convertedBetQuantity = 0, convertedBetFace = 0;

                Console.WriteLine("Please enter your new bet.");

                ///(Requirement 3.4.3)
                //Get the new bet from the user.
                //Make sure the user does not input an invalid entry.
                do
                {
                    Console.Write("The quantity of dice in your bet: ");
                    inputbetQuantity = Console.ReadLine();
                    Int32.TryParse(inputbetQuantity, out convertedBetQuantity);

                    Console.Write("The face of the dice in your bet: ");
                    inputbetFace = Console.ReadLine();
                    Int32.TryParse(inputbetFace, out convertedBetFace);

                    if (convertedBetFace < 1 || convertedBetFace > 6)
                    {
                        Console.WriteLine("ERROR: Please enter a valid integer between 1 and 6 inclusively.");
                    }
                    else if (convertedBetFace <= game.getbetFace() && convertedBetQuantity <= game.getbetQuantity())
                    {
                        Console.WriteLine("ERROR: Increase bet num, bet amount, or both.");
                    }

                } while (convertedBetFace < 1 || convertedBetFace > 6 || (convertedBetFace <= game.getbetFace() && convertedBetQuantity <= game.getbetQuantity()));

                setAndDisplay(game, convertedBetFace, convertedBetQuantity);
            }

        }

        /// <summary>
        /// The ai decides whether to increase bet or challenge the current bet using probability checks.
        /// First, it calls probCheck with game, betQuantity, and betFace. 
        /// If probCheck returns a number greater than or equal to 60%, it will not challenge. 
        /// Instead, it will find which face the current player has the most of. 
        /// If the face that it has the most of is equal to the current betFace, it will increase the current betQuantity.
        /// If the face that it has the most of is less than the current betFace, it will set the betFace to maxNumOfOccurences, 
        /// and will increase the betQuantity by 1.
        /// If the face that it has the most of is greater than the current betFace, it will simply set betFace to maxNumOfOccurences.
        /// Finally, it will display the new bet.
        /// (Requirement 3.3.1)
        /// (Requirement 3.3.4)
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        public static void ai(Game game)
        {
            //If the probability is greater than 60%, then the ai will make a new bet.
            if (probCheck(game, game.getbetQuantity(), game.getbetFace()) >= 60) ///(Requirement 3.3.4)
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
                display(game);
            }

            //If the probabililty is less than 60%, then the ai will challenge.
            else
            {
                challenge(game);
            }
        }

        /// <summary>
        /// Determines the probability of the current bet.
        /// First finds the number of betFace our current player has as the variable numFound.
        /// Then, it will remove the numFound from our placeholder for betQuantity, r.
        /// If the player has more of the betFace than bet, then the probability percent can be returned as 100%.
        /// Otherwise, it will also remove the numFound from our totalDice and put it into n.
        /// Lastly, it will call binomialDistribution with r and n converted to doubles as it's variables. 
        /// It will return the value of binomialDistribution multiplied by 100 to get the percent.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        /// <param name="r">The current betQuantity.</param>
        /// <param name="findDie">The current betFace.</param>
        /// <returns>The probability as a percent.</returns>
        public static double probCheck(Game game, int r, int findDie)
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
        /// The binomial distribution formula is: b(r; n, P) = nCr * Pr * (1 – P)^n – r
        /// Where b = binomial probability
        /// r = total number of "successes"
        /// P = probability of success on an individual trail
        /// n = number of trials.
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
                answer += combination(n,r) * Math.Pow(p, r) * Math.Pow(q, (n - r));
                r++;
            }

            return answer;
        }

        /// <summary>
        /// Calculates the number of combinations.
        /// The combination formula is: C(n,r)=n!/(r!(n−r)!)
        /// Where n = objects.
        /// and r = sample.
        /// </summary>
        /// <param name="n">Number of items in the set.</param>
        /// <param name="r">Number of items selected from the set.</param>
        /// <returns>The number of combinations between two variables.</returns>
        static public double combination(double n, double r)
        {
            return (factorial(n) / (factorial(r) * (factorial(n - r))));
        }

        /// <summary>
        /// Calculates the factorial
        /// The factorial formula is: n! = n * (n-1) * ... * 1
        /// Where n = the value given.
        /// </summary>
        /// <param name="num">The number that needs it's factorial calculated</param>
        /// <returns>The factorial</returns>
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
        /// First, it displays who is challenging.
        /// Next, it will show all of the dice. After showing one player's dice, it will include a call to nap for better viewing purposes.
        /// It will then count each instance of betFace in each player's dice.
        /// If there are more than or an equal amount of betFace as the betQuantity, then the one who challenged loses a die.
        /// Otherwise, then the person who made the bet loses a dice.
        /// Calls removeDice when someone loses a die, and displays who lost the die.
        /// If the person who loses a dice loses all of their dice, they are out of the game and calls removePlayer.
        /// If remove player is called, it will display that a player is out of the game.
        /// If the player removed is the human, and there are more than one other players, it will call humanOutOfGame.
        /// Lastly, it will calle gameEnd. If gameEnd returns true, it will stop the game completely.
        /// Otherwise, we reset betQuantity and betFace, as well as increasing the turn. It will then call startRound.
        /// (Requirement 3.1.1.4)
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void challenge(Game game)
        {
            //Display who is challenging.
            if (game.getPlayer(game.getTurn()).isHuman())
            {
                Console.WriteLine("\nYOU ARE CHALLENGING!");
            }
            else
            {
                Console.WriteLine("\nPlayer " + game.getPlayer(game.getTurn()).getPlayerNumber() + " IS CHALLENGING!");
            }

            ///(Requirement 3.1.1.4)
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
                        humanOutOfGame(game);
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
                        humanOutOfGame(game);
                    }
                    else
                    {
                        Console.WriteLine("Player " + game.getPlayer(lastPlayer).getPlayerNumber() + " is out of the game!\n");
                    }

                    //Remove the player.
                    game.removePlayer(lastPlayer);
                }
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
        /// seeing the outcome by setting the numOfPlayers to -1.
        /// (Requirement 3.5.4)
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void forceEnd(Game game)
        {
            game.setNumOfPlayers(-1);
        }

        /// <summary>
        /// Displays "You are out of the game." 
        /// If there are more than 1 player left after removing the human player, it will ask if the player wants to watch the ai finish
        /// the game.
        /// Error checking makes sure only y/Y/n/N are entered. 
        /// If "n" or "N" is entered, it will call forceEnd.
        /// </summary>
        /// <param name="game">Contains all information about the game. Is part of class Game.</param>
        static public void humanOutOfGame(Game game)
        {
            String input = String.Empty;
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

                if (input == "n")
                {
                    forceEnd(game);
                }
            }
        }

        /// <summary>
        /// Prints out the current player's dice side by side.
        /// It does this by adding each die line by line, adding to line by adding dieEmpty, dieOneEye, dieEdge, dieTwoEyes, or dieThreeEyes.
        /// At the end, it will print each line one by one.
        /// (Requirement 3.1.1)
        /// (Requirement 3.1.1.1)
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

            ///(Requirements 3.1.1.1)
            Console.WriteLine(lineOne);
            Console.WriteLine(lineTwo);
            Console.WriteLine(lineThree);
            Console.WriteLine(lineFour);
            Console.WriteLine(lineFive);
        }

        /// <summary>
        /// Determines if the game has ended by checking how many players are left.
        /// If there are more than 1 player, the game has not ended so it will return false.
        /// If there is only 1 player left, it will return true.
        /// (Requirement 3.5.3)
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
