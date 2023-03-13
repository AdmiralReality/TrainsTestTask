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
