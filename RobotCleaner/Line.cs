using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotCleaner
{
    public class Line
    {
        public Line(Coordinates start, Coordinates end)
        {
            _coordinatorComparer = new CoordinatesComparer();
            Start = start;
            End = end;
            Orientation = Start.X == End.X ? OrientationEnum.Vertical : OrientationEnum.Horizontal;
        }

        private readonly CoordinatesComparer _coordinatorComparer;
        public Coordinates Start { get; }
        public Coordinates End { get; }
        public OrientationEnum Orientation { get; }

        #region For general equation of a line
        public int A => End.Y - Start.Y;
        public int B => Start.X - End.X;
        public int C => A * Start.X + B * Start.Y;
        #endregion

        public enum OrientationEnum
        {
            Horizontal,
            Vertical
        }

        public List<Coordinates> GetIntersections(Line line)
        {
            if (Orientation == line.Orientation)
            {
                if (IsOverlappingWithParallelLine(line))
                    return GetIntersectionsWithOverlappingLine(line);
                else
                    return new List<Coordinates>();
            }

            return GetIntersectionWithNonParallelLine(line);
        }

        private List<Coordinates> GetIntersectionsWithOverlappingLine(Line line)
        {
            return GetCoordinates().Intersect(line.GetCoordinates(), _coordinatorComparer).ToList();
        }

        private List<Coordinates> GetIntersectionWithNonParallelLine(Line line)
        {
            var intersections = new List<Coordinates>();

            if (_coordinatorComparer.Equals(Start, line.End))
                intersections.Add(new Coordinates(Start.X, Start.Y));
            else if (_coordinatorComparer.Equals(End, line.Start))
                intersections.Add(new Coordinates(End.X, End.Y));
            else
            {
                var intersection = CalculateIntersection(line);
                if (line.IsCoordinatesOnLine(intersection) && IsCoordinatesOnLine(intersection))
                    intersections.Add(intersection);
            }

            return intersections;
        }

        private IEnumerable<Coordinates> GetCoordinates()
        {
            if (Orientation == OrientationEnum.Vertical)
                return Enumerable.Range(Math.Min(Start.Y, End.Y), Math.Abs(End.Y - Start.Y) + 1).Select(y => new Coordinates(Start.X, y));
            return Enumerable.Range(Math.Min(Start.X, End.X), Math.Abs(Start.X - End.X) + 1).Select(x => new Coordinates(x, Start.Y));
        }
        private Coordinates CalculateIntersection(Line line)
        {
            var determinant = (double)(A * line.B - line.A * B);

            return new Coordinates(
                Convert.ToInt32((line.B * C - B * line.C) / determinant),
                Convert.ToInt32((A * line.C - line.A * C) / determinant));
        }
        private bool IsCoordinatesOnLine(Coordinates c)
        {
            return Math.Min(Start.X, End.X) <= c.X && c.X <= Math.Max(Start.X, End.X) && Math.Min(Start.Y, End.Y) <= c.Y && c.Y <= Math.Max(Start.Y, End.Y);
        }
        private bool IsOverlappingWithParallelLine(Line line)
        {
            if (Orientation == OrientationEnum.Vertical && (Start.X != line.Start.X ||
                    (Math.Max(Start.Y, End.Y) < Math.Min(line.Start.Y, line.End.Y) || Math.Max(line.Start.Y, line.End.Y) < Math.Min(Start.Y, End.Y))))
                return false;
            else if (Orientation == OrientationEnum.Horizontal && (Start.Y != line.Start.Y ||
                (Math.Max(Start.X, End.X) < Math.Min(line.Start.X, line.End.X) || Math.Max(line.Start.X, line.End.X) < Math.Min(Start.X, End.X))))
                return false;

            return true;
        }
    }


}
