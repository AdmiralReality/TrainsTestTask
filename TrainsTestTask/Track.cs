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
    class Track
    {
        public List<Point> Points { get; set; } = new();

        public bool IsHighlighted { get; set; } = false; // TODO remove???
    }
}
