using System;
using System.Collections.Generic;
using Xunit;

namespace RobotCleaner.Tests
{
    public class RobotTests
    {
        private readonly CoordinatesComparer _coordinatesComparer;

        public RobotTests()
        {
            _coordinatesComparer = new CoordinatesComparer();
        }

        [Theory]
        [InlineData(44, 23, 'N', 25, 44, 48)]
        [InlineData(0, 3, 'E', 3, 3, 3)]
        [InlineData(-40, 40, 'S', 70, -40, -30)]
        [InlineData(-40, 0, 'W', 430, -470, 0)]
        public void InitiateCleaning_OneCommand_MoveRobot(int startX, int startY, char direction, int steps, int endX, int endY)
        {
            var robot = new Robot(new Coordinates(startX, startY));
            var commands = new List<RobotCommand> { new RobotCommand(direction, steps) };
            robot.InitiateCleaning(commands);

            Assert.True(_coordinatesComparer.Equals(robot.Position, new Coordinates(endX, endY)));
        }

        [Theory]
        [InlineData(0, 0, "E 5 W 5 E 2", 2, 0)]
        [InlineData(-5, 2, "N 3 W 7 N 2 E 10 S 2", -2, 5)]
        public void InitiateCleaning_MultipleCommands_MoveRobot(int startX, int startY, string commandString, int endX, int endY)
        {
            var robot = new Robot(new Coordinates(startX, startY));
            var commands = new List<RobotCommand>();
            var commandsInput = commandString.Split(" ");
            for (int i = 0; i < commandsInput.Length - 1; i += 2)
            {
                commands.Add(new RobotCommand(commandsInput[i].ToCharArray()[0], Int32.Parse(commandsInput[i + 1])));
            }

            robot.InitiateCleaning(commands);

            Assert.True(_coordinatesComparer.Equals(robot.Position, new Coordinates(endX, endY)));
        }

        [Theory]
        [InlineData(0, 0, "E 5 W 5 E 2", 6)]
        [InlineData(1, 1, "E 3 N 2 W 1 S 3 W 1 N 3", 12)]
        [InlineData(-320, 300, "N 100 E 50 S 25 W 30 N 100 W 20 S 150", 399)]
        public void InitiateCleaning_MultipleCommands_Clean(int startX, int startY, string commandString, int expected)
        {
            var robot = new Robot(new Coordinates(startX, startY));
            var commands = new List<RobotCommand>();
            var commandsInput = commandString.Split(" ");
            for (int i = 0; i < commandsInput.Length - 1; i += 2)
            {
                commands.Add(new RobotCommand(commandsInput[i].ToCharArray()[0], Int32.Parse(commandsInput[i + 1])));
            }
            robot.InitiateCleaning(commands);

            Assert.Equal(expected, robot.NrOfCleaningPlaces);
        }

        [Theory]
        [InlineData(0, 0, "E 5 N 5 W 5 S 5 E 5 N 5 W 5 S 5 E 5 N 5 W 5 S 5", 20)]
        [InlineData(-40, -100, "E 2500 N 3000 W 2500 S 3000 E 2500 N 3000 W 2500 S 3000 E 2500 N 3000 W 2500 S 3000", 11000)]
        public void InitiateCleaning_Circling_Clean(int startX, int startY, string commandString, int expected)
        {
            var robot = new Robot(new Coordinates(startX, startY));
            var commands = new List<RobotCommand>();
            var commandsInput = commandString.Split(" ");
            for (int i = 0; i < commandsInput.Length - 1; i += 2)
            {
                commands.Add(new RobotCommand(commandsInput[i].ToCharArray()[0], Int32.Parse(commandsInput[i + 1])));
            }
            robot.InitiateCleaning(commands);

            Assert.Equal(expected, robot.NrOfCleaningPlaces);
        }
       
        [Fact]
        public void InitiateCleaning_MaxCommandsWithIntersections_Clean()
        {
            var robot = new Robot(new Coordinates(0, 5000));
            var commands = new List<RobotCommand>();
            for (int i = 0; i < 9999; i += 4)
            {
                commands.Add(new RobotCommand('E', 5000));
                commands.Add(new RobotCommand('N', 1 + i));
                commands.Add(new RobotCommand('W', 4999));
                commands.Add(new RobotCommand('S', 2 + i));
            }

            robot.InitiateCleaning(commands);
            Assert.Equal(49994544, robot.NrOfCleaningPlaces);

        }

        [Fact]
        public void InitiateCleaning_MaxCommandsNoIntersections_Clean()
        {
            var robot = new Robot(new Coordinates(0, -100000));
            var commands = new List<RobotCommand>();
            for (int i = 0; i < 9999; i += 4)
            {
                commands.Add(new RobotCommand('E', 50000));
                commands.Add(new RobotCommand('N', 10));
                commands.Add(new RobotCommand('W', 50000));
                commands.Add(new RobotCommand('N', 10));
            }

            robot.InitiateCleaning(commands);
            Assert.Equal(250050001, robot.NrOfCleaningPlaces);
        }
    }
}
