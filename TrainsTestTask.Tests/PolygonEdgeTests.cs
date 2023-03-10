using System.Windows.Input;
using TrainsTestTask.Filling;

namespace TrainsTestTask.Tests
{
    public class PolygonEdgeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1, 10, 2, Direction.Up, 5, 1, true)]
        [TestCase(1, 10, 2, Direction.Up, 5, 2, false)]
        [TestCase(1, 10, 2, Direction.Up, 5, 3, false)]
        [TestCase(1, 10, 2, Direction.Down, 5, 1, false)]
        [TestCase(1, 10, 2, Direction.Down, 5, 2, false)]
        [TestCase(1, 10, 2, Direction.Down, 5, 3, true)]
        public void TestHorizontalLine(int x1, int x2, int y, Direction innerDir, int xNew, int yNew, bool expected)
        {
            // arrange
            var polygonEdge = new PolygonEdge()
            {
                Start = new PolygonPoint() { X = x1, Y = y },
                End = new PolygonPoint() { X = x2, Y = y },
                InnerSideDirection = innerDir,
            };

            // act
            var result = polygonEdge.DoesBelongToInnerSide(xNew, yNew);

            // assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(5, 1, 10, Direction.Left, 4, 5, true)]
        [TestCase(5, 1, 10, Direction.Left, 5, 5, false)]
        [TestCase(5, 1, 10, Direction.Left, 6, 5, false)]
        [TestCase(5, 1, 10, Direction.Right, 4, 5, false)]
        [TestCase(5, 1, 10, Direction.Right, 5, 5, false)]
        [TestCase(5, 1, 10, Direction.Right, 6, 5, true)]
        public void TestVerticalLine(int x, int y1, int y2, Direction innerDir, int xNew, int yNew, bool expected)
        {
            // arrange
            var polygonEdge = new PolygonEdge()
            {
                Start = new PolygonPoint() { X = x, Y = y1 },
                End = new PolygonPoint() { X = x, Y = y2 },
                InnerSideDirection = innerDir,
            };

            // act
            var result = polygonEdge.DoesBelongToInnerSide(xNew, yNew);

            // assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(2, 4, 6, 2, Direction.Left | Direction.Up, 3, 3, true)]
        [TestCase(2, 4, 6, 2, Direction.Left | Direction.Up, 4, 3, false)]
        [TestCase(2, 4, 6, 2, Direction.Left | Direction.Up, 5, 3, false)]

        [TestCase(2, 4, 6, 2, Direction.Right | Direction.Down, 3, 3, false)]
        [TestCase(2, 4, 6, 2, Direction.Right | Direction.Down, 4, 3, false)]
        [TestCase(2, 4, 6, 2, Direction.Right | Direction.Down, 5, 3, true)]

        [TestCase(2, 1, 6, 3, Direction.Right | Direction.Up, 3, 2, false)]
        [TestCase(2, 1, 6, 3, Direction.Right | Direction.Up, 4, 2, false)]
        [TestCase(2, 1, 6, 3, Direction.Right | Direction.Up, 5, 2, true)]

        [TestCase(2, 1, 6, 3, Direction.Left | Direction.Down, 3, 2, true)]
        [TestCase(2, 1, 6, 3, Direction.Left | Direction.Down, 4, 2, false)]
        [TestCase(2, 1, 6, 3, Direction.Left | Direction.Down, 5, 2, false)]
        public void TestDiagonalLine(int x1, int y1, int x2, int y2, Direction innerDir, int xNew, int yNew, bool expected)
        {
            // arrange
            var polygonEdge = new PolygonEdge()
            {
                Start = new PolygonPoint() { X = x1, Y = y1 },
                End = new PolygonPoint() { X = x2, Y = y2 },
                InnerSideDirection = innerDir,
            };

            // act
            var result = polygonEdge.DoesBelongToInnerSide(xNew, yNew);

            // assert
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}