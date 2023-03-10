using System;

namespace TrainsTestTask.Filling;

[Flags]
public enum Direction
{
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8,
}
