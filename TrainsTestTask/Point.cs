using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TrainsTestTask
{
    [DebuggerDisplay("({X}:{Y})")]
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public List<Point> IncomingPoints { get; set; } = new();
        public List<Point> OutgoingPoints { get; set; } = new();

        public Dictionary<Point, Line> Lines = new();

        // TODO make useable for re-rendering for new points???
        public void Draw(Canvas canvas, Point prevPoint = null, Line lineFromPrevPoint = null)
        {
            if (Lines.Count == (OutgoingPoints.Count + IncomingPoints.Count ) || (prevPoint is not null && Lines.ContainsKey(prevPoint)))
                return;

            if (lineFromPrevPoint is not null)
                Lines.Add(prevPoint, lineFromPrevPoint);

            foreach (var collection in new List<List<Point>> { OutgoingPoints, IncomingPoints })
            {
                foreach (var nextPoint in collection)
                {
                    if (Lines.ContainsKey(nextPoint))
                        continue;

                    var line = GetLine(this, nextPoint);
                    Lines.Add(nextPoint, line);
                    nextPoint.Lines.Add(this, line);
                    canvas.Children.Add(line);
                    nextPoint.Draw(canvas, nextPoint, line); // recursive call, stack overflow possible???
                }
            }
        }

        private Line GetLine(Point point1, Point point2)
        {
            return new Line()
            {
                X1 = point1.X,
                X2 = point2.X,
                Y1 = point1.Y,
                Y2 = point2.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };
        }

        public void AddDestination(params Point[] points)
        {
            foreach (var point in points)
            {
                if (!OutgoingPoints.Contains(point))
                    OutgoingPoints.Add(point);

                if (!point.IncomingPoints.Contains(this))
                    point.IncomingPoints.Add(this);
            }
        }

        public void AddTwoWays(params Point[] points)
        {
            foreach (var point in points)
            {
                AddDestination(point);
                point.AddDestination(this);
            }
        }

        public void SetIntervalColor(Point nextPoint, Brush brush)
        {
            if (!Lines.ContainsKey(nextPoint))
                throw new InvalidOperationException("Can't find line from current point to desired.");

            Lines[nextPoint].Stroke = brush;
        }
    }
}
