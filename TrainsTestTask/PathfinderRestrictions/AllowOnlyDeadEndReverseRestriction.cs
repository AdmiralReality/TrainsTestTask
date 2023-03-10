using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainsTestTask.PathfinderRestrictions
{
    class AllowOnlyDeadEndReverseRestriction : IPathfinderRestriction
    {
        public bool IsValid(Point? prev, Point current, Point next)
        {
            if (prev == null || current == null || next == null)
                return true;

            // accept all non-reversal cases.
            if (prev != next)
                return true;

            // check if we try to go back and there are no other ways to go.
            return prev == next && current.OutgoingPoints.Count == 1;
        }
    }
}
