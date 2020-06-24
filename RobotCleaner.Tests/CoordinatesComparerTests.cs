using Xunit;

namespace RobotCleaner.Tests
{
    public class CoordinatesComparerTests
    {
        [Theory]
        [InlineData(0, 3, 0, 3)]
        [InlineData(4, -22, 4, -22)]
        [InlineData(1231, -4000, 1231, -4000)]
        public void CoordinatesEqualityCompare_SameCoordinates_ReturnTrue(int x1, int y1, int x2, int y2)
        {
            var coordinates1 = new Coordinates(x1, y1);
            var coordinates2 = new Coordinates(x2, y2);

            Assert.True(new CoordinatesComparer().Equals(coordinates1, coordinates2));
        }

        [Theory]
        [InlineData(3, 0, 13, 0)]
        [InlineData(-123, -22, 144, -22)]
        [InlineData(44, 133, 1330, 133)]
        public void CoordinatesEqualityCompare_DifferentCoordinates_ReturnFalse(int x1, int y1, int x2, int y2)
        {
            var coordinates1 = new Coordinates(x1, y1);
            var coordinates2 = new Coordinates(x2, y2);

            Assert.False(new CoordinatesComparer().Equals(coordinates1, coordinates2));
        }
    }
}
