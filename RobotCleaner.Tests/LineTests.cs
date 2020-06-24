using System;
using Xunit;

namespace RobotCleaner.Tests
{
    public class LineTests
    {
        [Theory]
        [InlineData(0, 3, 0, 5)]
        [InlineData(4, 3, 4, -22)]
        [InlineData(1231, -125, 1231, 4000)]
        public void LineOrientation_VerticalLines_ReturnTrue(int startX, int startY, int endX, int endY)
        {
            var line = new Line(new Coordinates(startX, startY), new Coordinates(endX, endY));

            Assert.Equal(Line.OrientationEnum.Vertical, line.Orientation);
        }

        [Theory]
        [InlineData(3, 0, 13, 0)]
        [InlineData(-123, -22, 144, -22)]
        [InlineData(44, 133, 1330, 133)]
        public void LineOrientation_HorizontalLines_ReturnFalse(int startX, int startY, int endX, int endY)
        {
            var line = new Line(new Coordinates(startX, startY), new Coordinates(endX, endY));

            Assert.NotEqual(Line.OrientationEnum.Vertical, line.Orientation);
        }

        [Theory]
        [InlineData(3, 0, 13, 0)]
        [InlineData(-123, -22, 144, -22)]
        [InlineData(44, 133, 1330, 133)]
        public void LineOrientation_HorizontalLines_ReturnTrue(int startX, int startY, int endX, int endY)
        {
            var line = new Line(new Coordinates(startX, startY), new Coordinates(endX, endY));

            Assert.Equal(Line.OrientationEnum.Horizontal, line.Orientation);
        }

        [Theory]
        [InlineData(0, 3, 0, 5)]
        [InlineData(4, 3, 4, -22)]
        [InlineData(1231, -125, 1231, 4000)]
        public void LineOrientation_VerticalLines_ReturnFalse(int startX, int startY, int endX, int endY)
        {
            var line = new Line(new Coordinates(startX, startY), new Coordinates(endX, endY));

            Assert.NotEqual(Line.OrientationEnum.Horizontal, line.Orientation);
        }

        [Theory]
        [InlineData(3, 0, 13, 0, -22, 45, 30, 45)]
        [InlineData(5, 124, 5, 137, 5, 138, 5, 1043)]
        [InlineData(3, 124, 3, 137, 5, 124, 5, 137)]
        public void GetIntersections_NoIntersections_ReturnTrue(int line1StartX, int line1StartY, int line1EndX, int line1EndY,
            int line2StartX, int line2StartY, int line2EndX, int line2EndY)
        {
            var line1 = new Line(new Coordinates(line1StartX, line1StartY), new Coordinates(line1EndX, line1EndY));
            var line2 = new Line(new Coordinates(line2StartX, line2StartY), new Coordinates(line2EndX, line2EndY));

            Assert.Empty(line1.GetIntersections(line2));
        }

        [Theory]
        [InlineData(3, 0, 13, 0, 5, 0, 7, 0, 3)]
        [InlineData(-22, 0, 154, 0, 145, 0, 1045, 0, 10)]
        [InlineData(153, -4, 153, 150, 153, 30, 153, 41, 12)]
        public void GetIntersections_ParallelLines_ReturnTrue(int line1StartX, int line1StartY, int line1EndX, int line1EndY,
            int line2StartX, int line2StartY, int line2EndX, int line2EndY, int nrOfOverlaps)
        {
            var line1 = new Line(new Coordinates(line1StartX, line1StartY), new Coordinates(line1EndX, line1EndY));
            var line2 = new Line(new Coordinates(line2StartX, line2StartY), new Coordinates(line2EndX, line2EndY));

            Assert.Equal(nrOfOverlaps, line1.GetIntersections(line2).Count);
        }
    }
}
