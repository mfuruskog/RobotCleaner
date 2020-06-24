using System.Collections.Generic;
using System.Linq;

namespace RobotCleaner
{
    public class Robot
    {
        private readonly List<Line> _cleaningLines;
        public Coordinates Position { get; }

        public int NrOfCleaningPlaces { get; private set; }

        public Robot(Coordinates initialPosition)
        {
            _cleaningLines = new List<Line>();
            Position = initialPosition;
            NrOfCleaningPlaces = 0;
        }

        public void InitiateCleaning(List<RobotCommand> commands)
        {
            foreach (var command in commands)
            {
                var intersections = new List<Coordinates>();

                var startPosition = new Coordinates(Position.X, Position.Y);

                Move(command);

                var endPosition = new Coordinates(Position.X, Position.Y);
                var cleaningLine = new Line(startPosition, endPosition);

                foreach (var line in _cleaningLines)
                {
                    intersections.AddRange(cleaningLine.GetIntersections(line));
                }

                NrOfCleaningPlaces += command.Steps + 1 - intersections.Distinct(new CoordinatesComparer()).Count();
                _cleaningLines.Add(cleaningLine);

            }
        }
        private void Move(RobotCommand command)
        {
            switch (command.Direction)
            {
                case 'N':
                    Position.Y += command.Steps;
                    break;
                case 'E':
                    Position.X += command.Steps;
                    break;
                case 'S':
                    Position.Y -= command.Steps;
                    break;
                case 'W':
                    Position.X -= command.Steps;
                    break;
            }
        }
    }
}
