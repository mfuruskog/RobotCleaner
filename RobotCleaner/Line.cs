using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace RobotCleaner
{
    public class Line
    {
        public Coordinates Start { get; private set; }
        public Coordinates End { get; private set; }

        public bool IsVertical => Start.X == End.X;

        public Line(Coordinates start, Coordinates end)
        {
            Start = start;
            End = end;
        }

        public List<Coordinates> GetIntersections(Line line2)
        {
            return this.GetCoordinates().Intersect(line2.GetCoordinates(), new CoordinatesComparer()).ToList();
        }

        private IEnumerable<Coordinates> GetCoordinates()
        {
            if (this.IsVertical)
            {
                var lineY = Enumerable.Range(Math.Min(this.Start.Y, this.End.Y), Math.Abs(this.End.Y - this.Start.Y) + 1);
                return lineY.Select(y => new Coordinates(this.Start.X, y));
            }

            var lineX = Enumerable.Range(Math.Min(this.Start.X, this.End.X), Math.Abs(this.Start.X - this.End.X) + 1);
            return lineX.Select(x => new Coordinates(x, this.Start.Y));
        }
    }


}
