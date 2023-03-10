using System.Diagnostics;

namespace TrainsTestTask.Filling;

[DebuggerDisplay("[{Start}, {End}, {InnerSideDirection}]")]
public struct PolygonEdge
{
    public PolygonPoint Start;
    public PolygonPoint End;
    public Direction InnerSideDirection;

    public bool DoesBelongToInnerSide(int x, int y)
    {
        var direction = GetDirection(x, y);

        return direction == InnerSideDirection;
    }

    public Direction GetDirection(double x, double y)
    {
        // horizontal line
        if (Start.Y == End.Y)
        {
            if (y == Start.Y) // return opposite if lies on side (in order to add and highlight)
                return InnerSideDirection ^ Direction.Up | InnerSideDirection ^ Direction.Up;

            return y > Start.Y ? Direction.Down : Direction.Up;
        }

        // vertical line
        if (Start.X == End.X)
        {
            if (x == Start.X) // return opposite if lies on side
                return InnerSideDirection ^ Direction.Left | InnerSideDirection ^ Direction.Right;

            return x > Start.X ? Direction.Right : Direction.Left;
        }

        // diagonal line, a bit of simple math coming.
        // y = kx + b
        var k = ((double)(End.Y - Start.Y)) / ((double)(End.X - Start.X));
        var b = Start.Y - k * Start.X;

        var xProjection = (y - b) / k;
        var yProjection = k * x + b;

        if (xProjection == x || yProjection == y)
            return ~InnerSideDirection;

        var result = x > xProjection ? Direction.Right : Direction.Left;
        result |= y > yProjection ? Direction.Down : Direction.Up;
        return result;
    }
}
