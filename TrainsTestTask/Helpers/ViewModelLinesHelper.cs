using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TrainsTestTask.Helpers
{
    public static class ViewModelLinesHelper
    {
        public static (List<Shape>, Dictionary<(Point, Point), Line>) BuildLinesForRendering(Station station)
        {
            var lines = new List<Shape>();
            var lineBetweenPointsDict = new Dictionary<(Point, Point), Line>();

            foreach (var point in station.Points)
            {
                foreach (var collection in new List<List<Point>> { point.OutgoingPoints, point.IncomingPoints })
                {
                    foreach (var nextPoint in collection)
                    {
                        if (lineBetweenPointsDict.ContainsKey((point, nextPoint)) || lineBetweenPointsDict.ContainsKey((nextPoint, point)))
                            continue;

                        var line = ViewModelLinesHelper.GetLine(point, nextPoint);
                        lines.Add(line);
                        lineBetweenPointsDict.Add((point, nextPoint), line);
                        lineBetweenPointsDict.Add((nextPoint, point), line);
                    }
                }
            }

            return (lines, lineBetweenPointsDict);
        }

        public static Line GetLine(Point point1, Point point2, Brush stroke = null)
        {
            stroke ??= Brushes.Black;

            return new Line()
            {
                X1 = point1.X,
                X2 = point2.X,
                Y1 = point1.Y,
                Y2 = point2.Y,
                Stroke = stroke,
                StrokeThickness = 3
            };
        }
    }
}
