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

        public bool IsHighlighted { get; set; } = false;

        public void Highlight()
        {
            SetLineColor(Brushes.Red);
            IsHighlighted = true;
        }

        public void RemoveHighlight()
        {
            SetLineColor(Brushes.Black);
            IsHighlighted = false;
        }

        private void SetLineColor(Brush brush)
        {
            for (var i = 0; i < Points.Count - 1; i++)
                Points[i].SetIntervalColor(Points[i + 1], brush);
        }
    }
}
