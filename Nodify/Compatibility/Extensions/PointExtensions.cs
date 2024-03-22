using System;
using Avalonia;

namespace Nodify.Compatibility;

internal static class PointExtensions
{
    public static double LengthSquared(this Point point)
    {
        return point.X * point.X + point.Y * point.Y;
    }
        
    public static double Length(this Point point)
    {
        return Math.Sqrt(point.LengthSquared());
    }
}