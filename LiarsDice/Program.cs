using System;
using System.Collections;
using System.Threading;

namespace LiarsDice
{
    public class Program
    {
        ///<summary>
        ///The main program for Liar's Dice
        ///(Requirement 3.2.2) Must contain a minimum of two players, one human and one AI.
        ///(Requirement 3.2.3) Should have up to three AI, allowing for a total of four players.
        ///(Requirement 3.2.4) Users should be allowed to choose the number of AI to play against.
        ///(Requirement 3.4.1) Ability to choose number of AI to play against
        ///(Requirement 3.4.5) Can quit the game at any time.
        ///(Requirement 3.2.1) Players will be set as Human or AI.
        ///(Requirement 3.3.2) AI is created by computer
        ///</summary>
        static void Main(string[] args)
        {
            //First, how many players?
            //Determine by Starting UI 
            //This part is for testing until we get the UI number from Unity 
            int num = 0;

            //(Requirement 3.2.2) Must contain a minimum of two players, one human and one AI.
            //(Requirement 3.2.3) Should have up to three AI, allowing for a total of four players.
            //(Requirement 3.2.4) Users should be allowed to choose the number of AI to play against.
            //(Requirement 3.4.1) Ability to choose number of AI to play against
            //(Requirement 3.4.5) Can quit the game at any time.
            do
            {
                Console.WriteLine("             Welcome to Liar's Dice!" +
                    "\r\nWe will see if you have what it takes to outsmart our AI." +
                    "\r\nTo quit the game, press control C at any time.\r\n"); 
                Console.WriteLine("*************************************************************");
                Console.WriteLine("You can choose " +
                    "to play up to 3 AI." +
                    "\r\n**Include yourself in the number ( 2 = 1 AI and 1 Human )**" +
                    "\r\nHow many players would you like to challenge?"); //Between 2-4 (2 being 1 computer, 1 human)
                string val = Console.ReadLine();
                Int32.TryParse(val, out num);

                if (num < 2 || num > 4)
                {
                    Console.WriteLine("ERROR: Please enter a valid integer between 2 and 4 inclusively.");
                }

            } while (num < 2 || num > 4);

            Game game = new Game(num);

            //(Requirement 3.2.1) Players will be set as Human or AI.
            //(Requirement 3.3.2) AI is created by computer
            //Determine who goes first by randomly assigning human.
            //Array spot 0 will go first. (EX. If human is 0, human will go first)

            Random rnd = new Random();
            int human = rnd.Next(game.getNumOfPlayers() - 1); // produce number between 0 and numOfPlayers-1, giving array spot.
            game.setHuman(human);

            Console.WriteLine("You are Player " + (human + 1));

            //StartGame
            /*Takes the array of players, starting at 0. First turn goes to index 0.
              After setting bet, calls NextTurn. The next player will be index 1. 
              Next player can then increase amt of dice or val of dice. 
              Human and AI functionality for each will be different.*/
            DataHelper.startRound(game);
            while (!DataHelper.gameEnd(game))
            {
                DataHelper.increaseTurn(game);
                DataHelper.continueGame(game);
            }
            DataHelper.endScreen(game);
        }
    }
}

