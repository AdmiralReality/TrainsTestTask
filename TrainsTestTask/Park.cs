using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TrainsTestTask
{
    class Park
    {
        public string Name { get; set; }

        List<Track> tracks = new();

        Filling.Polygon polygon = null;

        public void AddTrack(Track track)
        {
            tracks.Add(track);
            // TODO remove polygon if added here???
        }

        // TODO broken logic and naming.
        public void DrawPolygon(Canvas canvas)
        {
            if (polygon is null)
                polygon = BuildPolygon(this.tracks);

            var points = polygon.GetPolygonJoints();

            Polygon renderPolygon = new Polygon();
            renderPolygon.Stroke = Brushes.LightGreen;
            renderPolygon.Fill = Brushes.LightGreen;
            renderPolygon.Points = new PointCollection(points.Select(point => new System.Windows.Point(point.X, point.Y)));
            renderPolygon.Opacity = 0.2;

            canvas.Children.Insert(0, renderPolygon);
        }

        public void Fill()
        {
            // TODO separate rendering and logic handling???
            throw new NotImplementedException();
        }

        private Filling.Polygon BuildPolygon(List<Track> tracks)
        {
            List<Point> points = tracks.SelectMany(x => x.Points).ToList();

            var polygon = new Filling.Polygon();

            points.ForEach(point => polygon.Add(point.X, point.Y));

            return polygon;
        }
    }
}
