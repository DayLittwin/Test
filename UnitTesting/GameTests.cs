using System;
using Xunit;
using LiarsDice;

namespace UnitTesting
{
    public class GameTests
    {
        /// <summary>
        /// Verifies that the correct player gets removed
        /// </summary>
        [Fact]
        public void removePlayer_Verifies_Correct_Player_Removed()
        {
            //Arrange
            Game game = new Game(4);
            int player2 = 2;
            Player[] players = new Player[4];

            //Act
            game.removePlayer(player2);

            //Assert
            Assert.True(players[3] == null);

        }
    }
}

