using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotCleaner
{
    public class Robot
    {
        private List<Line> _cleaningLines;
        public Coordinates Location { get; private set; }

        public int NrOfCleaningPlaces { get; private set; }

        public Robot(Coordinates initialLocation)
        {
            _cleaningLines = new List<Line>();
            Location = initialLocation;
            NrOfCleaningPlaces = 0;
        }

        public void InitiateCleaning(List<RobotCommand> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                var startPosition = new Coordinates(Location.X, Location.Y);

                Move(commands[i]);

                var endPosition = new Coordinates(Location.X, Location.Y);
                var currentLine = new Line(startPosition, endPosition);
                var overlaps = new List<Coordinates>();

                foreach (var line in _cleaningLines)
                {
                    overlaps.AddRange(currentLine.GetIntersections(line));
                }

                NrOfCleaningPlaces += commands[i].Steps + 1 - overlaps.Distinct(new CoordinatesComparer()).Count();
                _cleaningLines.Add(currentLine);

            }
        }
        public void Move(RobotCommand command)
        {
            switch (command.Direction)
            {
                case 'N':
                    Location.Y += command.Steps;
                    break;
                case 'E':
                    Location.X += command.Steps;
                    break;
                case 'S':
                    Location.Y -= command.Steps;
                    break;
                case 'W':
                    Location.X -= command.Steps;
                    break;
                default:
                    break;
            }
        }
    }
}
