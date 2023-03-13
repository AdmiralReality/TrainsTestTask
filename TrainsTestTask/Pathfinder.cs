using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainsTestTask.Helpers;
using TrainsTestTask.PathfinderRestrictions;

namespace TrainsTestTask
{
    class Pathfinder
    {
        IPathfinderRestriction[] restrictions;

        public Pathfinder(params IPathfinderRestriction[] restrictions)
        {
            this.restrictions = restrictions;
        }

        public Track? Find(Point from, Point to)
        {
            var rootNode = new PathNode() { Point = from };
            var nodesToCheck = new List<PathNode>() { rootNode };
            // dict to check if path was already walked and how effectivly it was walked.
            var edges = new Dictionary<(Point from, Point to), double>();

            PathNode targetNode = null;
            while (true)
            {
                if (!nodesToCheck.Any())
                    break;

                // for each node
                var nextNodesToCheck = new List<PathNode>();
                foreach (var currentCheckingNode in nodesToCheck)
                {
                    var prevNode = currentCheckingNode.Parent;
                    var currentNode = currentCheckingNode;
                    var nextPoints = currentCheckingNode.Point.OutgoingPoints;

                    if (currentNode.Point == to)
                    {
                        if (targetNode is null || targetNode.PathLength < currentNode.PathLength)
                        {
                            targetNode = currentNode;
                        }
                        continue;
                    }

                    if (targetNode is not null && targetNode.PathLength < currentNode.PathLength)
                        continue;

                    var validPoints = nextPoints.Where(next =>
                        restrictions.All(restriction => restriction.IsValid(prevNode?.Point, currentNode.Point, next))
                    );

                    // looking forward for new points to check.
                    foreach (var point in validPoints)
                    {
                        var pathLength = MathHelper.GetLineSegmentLength(currentNode.Point, point);
                        var edge = (currentNode.Point, point);
                        var shouldUse = true;
                        
                        if (!edges.ContainsKey(edge)) // walking for the first time
                            edges.Add(edge, pathLength);
                        else if (pathLength < edges[edge]) // found more effective way
                            edges[edge] = pathLength;
                        else
                            shouldUse = false; // if neither (point already there and no more effective) - no need to check.

                        if (shouldUse)
                            nextNodesToCheck.Add(new PathNode() { Point = point, Parent = currentCheckingNode, PathLength = currentCheckingNode.PathLength + 1 });
                    }
                }
                nodesToCheck = nextNodesToCheck;
            }

            if (targetNode is null)
                return null; // return empty track instead?

            var points = new List<Point>();
            while (targetNode is not null)
            {
                points.Add(targetNode.Point);
                targetNode = targetNode.Parent;
            }

            points.Reverse();

            var resultingTrack = new Track() { Points = points };
            return resultingTrack;
        }

        [DebuggerDisplay("Point={Point},PathLength={PathLength},Parent={Parent?.Point}")]
        class PathNode
        {
            public Point Point;
            public PathNode? Parent = null;
            public int PathLength = 0;
        }
    }
}
