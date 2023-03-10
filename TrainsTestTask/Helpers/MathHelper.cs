using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrainsTestTask.Helpers
{
    static class MathHelper
    {
        public static double GetLineSegmentLength(Point p1, Point p2)
        {
            var x = p1.X - p2.X;
            var y = p1.Y - p2.Y;
            return Math.Sqrt(Sq(x) + Sq(y));
        }

        // TOOD use generic math from .net 7.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sq(double num)
        {
            return num * num;
        }
    }
}
