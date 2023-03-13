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
    public class Park
    {
        public string Name { get; set; }

        List<Track> tracks = new();

        Filling.Polygon polygon = null;

        public void AddTrack(Track track)
        {
            tracks.Add(track);
            polygon = null;
        }

        public List<System.Windows.Point> GetPolygonPoints()
        {
            if (polygon is null)
                polygon = new Filling.Polygon();

            polygon = BuildPolygon(this.tracks);
            var points = polygon.GetPolygonJoints();

            return points.Select(point => new System.Windows.Point(point.X, point.Y)).ToList();
        }

        private Filling.Polygon BuildPolygon(List<Track> tracks)
        {
            List<Point> points = tracks.SelectMany(x => x.Points).ToList();

            var polygon = new Filling.Polygon();

            points.ForEach(point => polygon.Add(point.X, point.Y));

            return polygon;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
