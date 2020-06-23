using System;
using Xunit;

namespace RobotCleaner.Tests
{
    public class RobotTests
    {
        [Theory]
        [InlineData(0, 3, 'E', 3, 3, 3)]
        [InlineData(44, 23, 'N', 25, 44, 48)]
        [InlineData(-40, 0, 'W', 430, -470, 0)]
        [InlineData(-40, 40, 'S', 70, -40, -30)]
        public void Move_OneCommand_ReturnTrue(int startX, int startY, char direction, int steps, int endX, int endY)
        {
            var initialPosition = new Coordinates(startX, startY);
            var robot = new Robot(initialPosition);

            robot.Move(new RobotCommand(direction, steps));

            Assert.True(new CoordinatesComparer().Equals(initialPosition, new Coordinates(endX, endY)));
        }

    }
}
