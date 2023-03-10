using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static TrainsTestTask.Helpers.MathHelper;

namespace TrainsTestTask.PathfinderRestrictions
{
    class ForbidSharpCornerRestriction : IPathfinderRestriction
    {
        public bool IsValid(Point? prev, Point current, Point next)
        {
            if (prev == null || current == null || next == null)
                return true;

            // allow reverse
            if (prev == next)
                return true;

            // source: https://www.mathsisfun.com/algebra/trig-cosine-law.html

            var angle = GetAngleBetweenThreePoints(prev, current, next);
            var desired = Math.PI / 2;

            return angle >= desired;
        }

        private double GetAngleBetweenThreePoints(Point p1, Point p2, Point p3)
        {
            var p12 = GetLineSegmentLength(p1, p2);
            var p23 = GetLineSegmentLength(p2, p3);
            var p13 = GetLineSegmentLength(p1, p3);

            return Math.Acos((Sq(p12) + Sq(p23) - Sq(p13))/(2 * p12 * p23));
        }
    }
}
