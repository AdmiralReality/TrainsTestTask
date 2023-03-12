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

        #region Graph Building Functions
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
        #endregion Graph Building Functions
    }
}
