using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            List<RobotCommand> commands = new List<RobotCommand>();

            int numberOfCommands = Int32.Parse(Console.ReadLine());

            int[] initialPosition = Console.ReadLine().Split(' ').Select(i => Int32.Parse(i)).ToArray();

            for (int i = 0; i < numberOfCommands; i++)
            {
                string[] command = Console.ReadLine().Split(' ');
                commands.Add(new RobotCommand(command[0].ToCharArray()[0], Int32.Parse(command[1])));
            }

            Robot robot = new Robot(new Coordinates(initialPosition[0], initialPosition[1]));
            CleaningService cleaning = new CleaningService(robot);

            cleaning.InitiateCleaning(commands);

            Console.WriteLine($"=> Cleaned: {cleaning.NrOfCleaningPlaces}");

        }
    }
}
