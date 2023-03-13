using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TrainsTestTask
{
    public class Station
    {
        public List<Point> Points = new();
        public List<Track> SavedTracks = new();
        public List<Park> Parks = new();
    }
}
