namespace TrainsTestTask.Filling;

public struct PolygonPoint // excessive???
{
    public int X;
    public int Y;

    public static bool operator ==(PolygonPoint p1, PolygonPoint p2)
    {
        return p1.X == p2.X && p1.Y == p2.Y;
    }

    public static bool operator !=(PolygonPoint p1, PolygonPoint p2)
    {
        return p1.X != p2.X || p1.Y != p2.Y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
