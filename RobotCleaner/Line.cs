using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace RobotCleaner
{
    public class Line
    {
        public enum OrientationEnum
        {
            Horizontal,
            Vertical

        }
        public Coordinates Start { get; }
        public Coordinates End { get; }
        public OrientationEnum Orientation { get; }
        public int A => End.Y - Start.Y;
        public int B => Start.X - End.X;
        public int C => A * Start.X + B * Start.Y;

        public Line(Coordinates start, Coordinates end)
        {
            Start = start;
            End = end;
            Orientation = Start.X == End.X ? OrientationEnum.Vertical : OrientationEnum.Horizontal;
        }


        public List<Coordinates> GetIntersectionOfNonParallelLines(Line line2)
        {
            var coordinatesComparer = new CoordinatesComparer();
            var intersections = new List<Coordinates>();
            if (coordinatesComparer.Equals(Start, line2.End))
                intersections.Add(new Coordinates(Start.X, End.Y));
            else if (coordinatesComparer.Equals(End, line2.Start))
                intersections.Add(new Coordinates(Start.X, End.Y));
            else
            {
                var det = (double)(A * line2.B - line2.A * B);
                if (det != 0)
                {
                    var x = Convert.ToInt32((line2.B * C - B * line2.C) / det);
                    var y = Convert.ToInt32((A * line2.C - line2.A * C) / det);

                    if (Math.Min(Start.X, End.X) <= x && x <= Math.Max(Start.X, End.X) && Math.Min(Start.Y, End.Y) <= y && y <= Math.Max(Start.Y, End.Y))
                    {
                        intersections.Add(new Coordinates(x, y));
                    }
                }
            }           

            return intersections;
        }

        public List<Coordinates> GetIntersections(Line line2)
        {
            var intersections = new List<Coordinates>();

            if (Orientation == line2.Orientation)
            {
                if (Orientation == OrientationEnum.Vertical && (Start.X != line2.Start.X || 
                    (Math.Max(Start.Y, End.Y) < Math.Min(line2.Start.Y, line2.End.Y) || Math.Max(line2.Start.Y, line2.End.Y) < Math.Min(Start.Y, End.Y))))
                    return intersections;
                if (Orientation == OrientationEnum.Horizontal && (Start.Y != line2.Start.Y ||
                    (Math.Max(Start.X, End.X) < Math.Min(line2.Start.X, line2.End.X) || Math.Max(line2.Start.X, line2.End.X) < Math.Min(Start.X, End.X))))
                    return intersections;

                return GetCoordinates().Intersect(line2.GetCoordinates(), new CoordinatesComparer()).ToList();

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
    }


}
