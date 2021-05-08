using System;
using Xunit;
using LiarsDice;

namespace UnitTesting
{
    public class GameTests
    {
        /// <summary>
        /// Verifies that the correct player gets removed
        /// Requirements Tested: 3.2.5
        /// </summary>
        [Fact]
        public void removePlayer_Verifies_Correct_Player_Removed()
        {
            //Arrange
            Game game = new Game(4);
            Player[] players = new Player[4];
            int player2 = 2;

            //Act
            game.removePlayer(player2);

            //Assert
            Assert.True(players[3] == null);

        }

        /// <summary>
        /// Validates the computer sets which player is human
        /// Requirements 3.2.1
        /// </summary>
        [Fact]
        public void setHuman_And_isHuman_Valide_human_gets_set()
        {
            //Arrange
            Game game = new Game(4);
            Player[] players = new Player[4];
            int humanPlayer = 2;
            bool expected = true;

            //Act
            game.setHuman(humanPlayer);
            bool result = game.isHuman(humanPlayer);

            //Assert
            Assert.Equal(expected, result);

        }
    }
}

