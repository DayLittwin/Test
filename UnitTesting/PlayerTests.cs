using System;
using Xunit;
using LiarsDice;

namespace UnitTesting
{
    public class PlayerTests
    {
        /// <summary>
        /// Verifies the dice are randomly set
        /// Requirements Tested: 3.1.1
        /// </summary>
        [Fact]
        public void setDice_Verifies_Randomization()
        {
            //Arrange
            Player playerOne = new Player(1);
            Player playerTwo = new Player(1);

            //Act
            playerOne.setDice();
            playerTwo.setDice();

            //Assert
            Assert.NotEqual(playerOne, playerTwo);

        }

        /// <summary>
        /// Verifies a die was successfully removed
        /// Requirements Tested: 3.1.1
        /// </summary>
        [Fact]
        public void removeDie_Verifies_Removal()
        {
            //Arrange
            Player player = new Player(1);
            int expected = 4;

            //Act
            player.removeDie();
            int result = player.getNumOfDice();

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
