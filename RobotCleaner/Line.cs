using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotCleaner
{
    public class Line
    {
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

        public Line(Coordinates start, Coordinates end)
        {
            _coordinatorComparer = new CoordinatesComparer();
            Start = start;
            End = end;
            Orientation = Start.X == End.X ? OrientationEnum.Vertical : OrientationEnum.Horizontal;
        }

        public List<Coordinates> GetIntersectionOfNonParallelLines(Line line2)
        {
            var intersections = new List<Coordinates>();

            if (_coordinatorComparer.Equals(Start, line2.End))
                intersections.Add(new Coordinates(Start.X, Start.Y));
            else if (_coordinatorComparer.Equals(End, line2.Start))
                intersections.Add(new Coordinates(End.X, End.Y));
            else
            {
                var intersection = CalculateIntersection(line2);
                if (line2.IsCoordinatesOnLine(intersection) && IsCoordinatesOnLine(intersection))
                    intersections.Add(intersection);
            }

            return intersections;
        }

        public List<Coordinates> GetIntersections(Line line2)
        {
            if (Orientation == line2.Orientation)
            {
                if (HasOverlapsWithParallelLine(line2))
                    return GetCoordinates().Intersect(line2.GetCoordinates(), _coordinatorComparer).ToList();
                else
                    return new List<Coordinates>();
            }

            return GetIntersectionOfNonParallelLines(line2);
        }

        private IEnumerable<Coordinates> GetCoordinates()
        {

            if (Orientation == OrientationEnum.Vertical)
            {
                var lineY = Enumerable.Range(Math.Min(Start.Y, End.Y), Math.Abs(End.Y - Start.Y) + 1);
                return lineY.Select(y => new Coordinates(Start.X, y));
            }

            var lineX = Enumerable.Range(Math.Min(Start.X, End.X), Math.Abs(Start.X - End.X) + 1);
            return lineX.Select(x => new Coordinates(x, Start.Y));
        }
        private Coordinates CalculateIntersection(Line line2)
        {
            var determinant = (double)(A * line2.B - line2.A * B);

            return new Coordinates(
                Convert.ToInt32((line2.B * C - B * line2.C) / determinant),
                Convert.ToInt32((A * line2.C - line2.A * C) / determinant));
        }
        private bool IsCoordinatesOnLine(Coordinates c)
        {
            return Math.Min(Start.X, End.X) <= c.X && c.X <= Math.Max(Start.X, End.X) && Math.Min(Start.Y, End.Y) <= c.Y && c.Y <= Math.Max(Start.Y, End.Y);
        }
        private bool HasOverlapsWithParallelLine(Line line2)
        {
            if (Orientation == OrientationEnum.Vertical && (Start.X != line2.Start.X ||
                    (Math.Max(Start.Y, End.Y) < Math.Min(line2.Start.Y, line2.End.Y) || Math.Max(line2.Start.Y, line2.End.Y) < Math.Min(Start.Y, End.Y))))
                return false;
            else if (Orientation == OrientationEnum.Horizontal && (Start.Y != line2.Start.Y ||
                (Math.Max(Start.X, End.X) < Math.Min(line2.Start.X, line2.End.X) || Math.Max(line2.Start.X, line2.End.X) < Math.Min(Start.X, End.X))))
                return false;

            return true;
        }
    }


}
