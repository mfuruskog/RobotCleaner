using System;
using System.Collections.Generic;
using System.Text;

namespace RobotCleaner
{
    public class CoordinatesComparer : IEqualityComparer<Coordinates>
    {
        public bool Equals(Coordinates c1, Coordinates c2)
        {
            if (Object.ReferenceEquals(c1, c2)) return true;

            if (Object.ReferenceEquals(c1, null) || Object.ReferenceEquals(c2, null))
                return false;

            return c1.X == c2.X && c1.Y == c2.Y;
        }

        public int GetHashCode(Coordinates coordinates)
        {
            if (Object.ReferenceEquals(coordinates, null)) return 0;

            int hashCoordinateX = coordinates.X.GetHashCode();

            int hashCoordinateY = coordinates.Y.GetHashCode();

            return hashCoordinateX ^ hashCoordinateY;
        }
    }
}
