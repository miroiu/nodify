using System;

namespace Nodify
{
    internal static class MathExtensions
    {
        /// <summary> Snaps a value down to the largest multiple of the specified grid cell size.</summary>
        public static double SnapToGrid(this double value, uint gridCellSize)
            => Math.Floor(value / gridCellSize) * gridCellSize;

        /// <summary> Wraps a value within a specified range.</summary>
        public static double WrapToRange(this double value, double min, double max)
        {
            double range = max - min;
            value = (value - min) % range;

            return value < 0 ? value + range + min : value + min;
        }

        public static double Clamp(this double value, double min, double max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
 
            return value;
        }
    }
}
