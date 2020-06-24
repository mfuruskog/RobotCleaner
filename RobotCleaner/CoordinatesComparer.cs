using System.Collections.Generic;

namespace RobotCleaner
{
    public class CoordinatesComparer : IEqualityComparer<Coordinates>
    {
        public bool Equals(Coordinates c1, Coordinates c2)
        {
            if (ReferenceEquals(c1, c2)) return true;

            if (c1 is null || c2 is null)
                return false;

            return c1.X == c2.X && c1.Y == c2.Y;
        }

        public int GetHashCode(Coordinates coordinates)
        {
            int hashCoordinateX = coordinates.X.GetHashCode();

            int hashCoordinateY = coordinates.Y.GetHashCode();

            return hashCoordinateX ^ hashCoordinateY;
        }
    }
}
