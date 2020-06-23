using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotCleaner
{
    public class CleaningService
    {
        private List<Line> _cleaningLines;

        public Robot Robot { get; private set; }
        public int NrOfCleaningPlaces { get; private set; }

        public CleaningService(Robot robot)
        {
            _cleaningLines = new List<Line>();
            Robot = robot;
            NrOfCleaningPlaces = 0;
        }
        public void InitiateCleaning(List<RobotCommand> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                var startPosition = new Coordinates(Robot.Location.X, Robot.Location.Y);

                Robot.Move(commands[i]);

                var endPosition = new Coordinates(Robot.Location.X, Robot.Location.Y);
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
    }
}
