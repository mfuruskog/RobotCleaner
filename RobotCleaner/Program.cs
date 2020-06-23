using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = new List<RobotCommand>();

            var numberOfCommands = Int32.Parse(Console.ReadLine());

            var initialPosition = Console.ReadLine().Split(' ').Select(i => Int32.Parse(i)).ToArray();

            for (int i = 0; i < numberOfCommands; i++)
            {
                var command = Console.ReadLine().Split(' ');
                commands.Add(new RobotCommand(command[0].ToCharArray()[0], Int32.Parse(command[1])));
            }

            var robot = new Robot(new Coordinates(initialPosition[0], initialPosition[1]));
            robot.InitiateCleaning(commands);

            Console.WriteLine($"=> Cleaned: {robot.NrOfCleaningPlaces}");

        }
    }
}
