using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrainsTestTask
{
    class Station
    {
        public List<Point> Points = new();
        public List<Track> SavedTracks = new();
        public List<Park> Parks = new();

        public void Draw(Canvas canvas)
        {
            if (!Points.Any())
                throw new InvalidOperationException();

            Points.First().Draw(canvas);
        }

        public void HighlightPark()
        {
            throw new NotImplementedException();
        }
    }
}
