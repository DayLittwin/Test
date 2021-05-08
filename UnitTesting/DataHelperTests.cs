using System;
using LiarsDice;
using Xunit;

namespace UnitTesting
{
    public class DataHelperTests
    {
        /// <summary>
        /// Validates the algorithm that is being used returns the expected result
        /// Requirements Tested: 3.3.4 - Probability used to challenge
        /// </summary>
        [Fact]
        public static void binomialDistribution_ValidateAlgorithm()
        {
            //Arrange
            double r = 13;
            double n = 4;
            double expected = 0;

            //Act
            double result = DataHelper.binomialDistribution(r, n);

            //Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Validates the logic behind the probability and checks if probability is higher than 60% it bets.
        /// Requirements Tested: 3.3.4 - Probability used to challenge
        /// </summary>
        [Fact]
        public static void probabilityCheck_Validate_Over60()
        {
            //Arrange
            Game game = new Game(4);
            int r = 2;
            int findDie = 6;
            double expected = 86.95797334611967;

            //Act
            double result = DataHelper.probCheck(game, r, findDie);

            //Assert
            Assert.Equal(expected, result);
        }
        /// <summary>
        /// Validates the logic behind the probability and checks if probability is lower than 60% it does not bet.
        /// Requirements Tested: 3.3.2 - Probability used to challenge
        /// </summary>
        [Fact]
        public static void probabilityCheck_Validate_Under60()
        {
            //Arrange
            Game game = new Game(4);
            int r = 4;
            int findDie = 3;
            double expected = 43.34543621919781;

            //Act
            double result = DataHelper.probCheck(game, r, findDie);

            //Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Validates the computer shuts down the game properly based on number of players
        /// Requirements Tested: 3.5.3 - Game will end when there are no more players
        /// </summary>
        [Fact]
        public static void endGame_Validate_EndofGame()
        {
            //Arrange
            Game game = new Game(4);
            Game endGame = new Game(0);
            bool expected = false;
            bool endExpected = true;

            //Act
            bool result = DataHelper.gameEnd(game);
            bool endResult = DataHelper.gameEnd(endGame);

            //Assert
            Assert.Equal(expected, result);
            Assert.Equal(endExpected, endResult);
        }
    }
}
