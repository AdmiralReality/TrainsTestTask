using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TrainsTestTask
{
    class ApplicationViewModel
    {
        // TODO: provide parser/StationBuilder/etc?
        // TODO: add selecting file for loading?

        // TODO add station renderer from graph made of points.

        public Station Station { get; set; } = new();

        // TODO selected track

        public List<Line> RenderedLines { get; set; } = new();
        public Dictionary<(Point, Point), Line> LineBetweenPointsDict { get; set; } = new();

        public ApplicationViewModel(StationParser stationParser)
        {
            Station = stationParser.Parse("");
            RenderStationPathes();
        }

        public void RenderStationPathes()
        {
            RenderedLines = new();
            LineBetweenPointsDict = new();

            foreach (var point in Station.Points)
            {
                foreach (var collection in new List<List<Point>> { point.OutgoingPoints, point.IncomingPoints })
                {
                    foreach (var nextPoint in collection)
                    {
                        if (LineBetweenPointsDict.ContainsKey((point, nextPoint)) || LineBetweenPointsDict.ContainsKey((nextPoint, point)))
                            continue;

                        var line = GetLine(point, nextPoint);
                        RenderedLines.Add(line);
                        LineBetweenPointsDict.Add((point, nextPoint), line);
                        LineBetweenPointsDict.Add((nextPoint, point), line);
                    }
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

        #region TrackHighlight
        public void HighlightTrack(Track track)
        {
            var brush = Brushes.Red;
            SetTrackColor(track, brush);
            track.IsHighlighted = true;
        }

        public void LowlightTrack(Track track)
        {
            var brush = Brushes.Black;
            SetTrackColor(track, brush);
            track.IsHighlighted = false;
        }

        private void SetTrackColor(Track track, Brush brush)
        {
            for (var i = 0; i < track.Points.Count - 1; i++)
            {
                var line = LineBetweenPointsDict[(track.Points[i], track.Points[i + 1])];
                line.Stroke = brush;
            }
                
        }
        #endregion TrackHighlight
    }
}
