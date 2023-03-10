using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainsTestTask.PathfinderRestrictions
{
    interface IPathfinderRestriction
    {
        bool IsValid(Point? prev, Point current, Point next);
    }
}
