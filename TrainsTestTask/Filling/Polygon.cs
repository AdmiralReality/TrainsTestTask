using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;

namespace TrainsTestTask.Filling;

public class Polygon
{
    // order is critical!!!
    // 1. clockwise (start -> end)
    // 2. end point of side is the start point of next side. End of the last side (in list) is the start of the first side (in list).
    List<PolygonEdge> sides = new(); // TODO use linkedlist???
    int areaPadding;

    public Polygon(int areaPadding = 10)
    {
        this.areaPadding = areaPadding;
    }

    public List<PolygonPoint> GetPolygonJoints()
    {
        return sides.Select(x => x.Start).ToList();
    }

    public void Add(int x, int y)
    {
        if (!sides.Any())
        {
            InsertFirstPoint(x, y);
            return;
        }

        var doesLayOutsidePolygon =
            sides.Where(side => !side.DoesBelongToInnerSide(x, y))
            .Any();

        if (!doesLayOutsidePolygon)
            return;

        // Allows to simplify filling visualization of new points and straight lines
        // (adds some kind of padding).
        // TODO: fix drawback of O(n * 5) because of additional checks (bottleneck?)
        var newPoints = GetSquare(x, y);

        foreach (var point in newPoints)
            Insert(point);
    }

    void Insert(PolygonPoint newPoint)
    {
        var doesNotBelongIds = sides
            .Select((x, id) => id)
            .Where(id => !sides[id].DoesBelongToInnerSide(newPoint.X, newPoint.Y))
            .ToList();

        if (!doesNotBelongIds.Any())
            return;

        var doesNotBelong = doesNotBelongIds.Select(x => sides[x]).ToList();

        (int left, int right)? peakSidesIds = null;
        for (var i = 0; i < doesNotBelong.Count; i++)
        {
            var nextId = i + 1 == doesNotBelong.Count ? 0 : i + 1;

            if (doesNotBelong[i].End != doesNotBelong[nextId].Start)
            {
                peakSidesIds = (doesNotBelongIds[i], doesNotBelongIds[nextId]);
                break;
            }
        }

        if (peakSidesIds is null)
        {
            var sidesData = string.Join(',', sides.Select(x => $"[{x.Start}, {x.End}]"));
            throw new InvalidDataException($"Sides was not stored properly which resulted to data crash (or unexpected situation occured. {sidesData}");
        }
        // TODO: one-by-one removal and insertion vs "skip().take()" rebuilding???

        PolygonEdge BuildNewEdge(PolygonPoint start, PolygonPoint end, PolygonPoint innerSideAnchor)
        {
            var edge = new PolygonEdge()
            {
                Start = start,
                End = end
            };
            edge.InnerSideDirection = edge.GetDirection(innerSideAnchor.X, innerSideAnchor.Y);
            return edge;
        }

        var newEdges = new PolygonEdge[]
        {
            BuildNewEdge(sides[peakSidesIds.Value.right].Start, newPoint, sides[peakSidesIds.Value.left].End),
            BuildNewEdge(newPoint, sides[peakSidesIds.Value.left].End, sides[peakSidesIds.Value.right].Start),
        };

        // peakSidesIds - id of sides. Start point id == id of side, end point id = id of side + 1.
        int left = peakSidesIds.Value.left + 1;
        int right = peakSidesIds.Value.right;


        var newSides = new List<PolygonEdge>();
        if (left < right)
        {
            newSides.AddRange(sides.Skip(left).Take(right - left));
            newSides.AddRange(newEdges);
        }
        else
        {
            newSides.AddRange(sides.Take(right));
            newSides.AddRange(newEdges);
            newSides.AddRange(sides.Skip(left));
        }
        sides = newSides;
        ;

        //if (newStart < newEnd)
        //{
            
        //    sides = sides.Take(newStart + 1).Skip(newEnd - newStart + 1).Concat(newEdges).Take(sides.Count - newStart).ToList();
        //}
        //else
        //{
        //    sides = sides.Skip(newEnd + 1).Take(newStart - newEnd + 1).Concat(newEdges).ToList();
        //}
    }

    void InsertFirstPoint(int x, int y)
    {
        var startingPolygonPoints = GetSquare(x, y);
        sides = new List<PolygonEdge>()
        {
            new PolygonEdge() { Start = startingPolygonPoints[0], End = startingPolygonPoints[1], InnerSideDirection = Direction.Down },
            new PolygonEdge() { Start = startingPolygonPoints[1], End = startingPolygonPoints[2], InnerSideDirection = Direction.Left },
            new PolygonEdge() { Start = startingPolygonPoints[2], End = startingPolygonPoints[3], InnerSideDirection = Direction.Up },
            new PolygonEdge() { Start = startingPolygonPoints[3], End = startingPolygonPoints[0], InnerSideDirection = Direction.Right },
        };
    }

    PolygonPoint[] GetSquare(int x, int y)
    {
        return new PolygonPoint[] {
            new () { X = x - areaPadding, Y = y - areaPadding }, // left upper
            new () { X = x + areaPadding, Y = y - areaPadding }, // right upper
            new () { X = x + areaPadding, Y = y + areaPadding }, // right bottom
            new () { X = x - areaPadding, Y = y + areaPadding } // left bottom
        };
    }
}
