using System.Windows;

namespace Nodify
{
    public static class BoxValue
    {
        public static readonly Point Point = default(Point);
        public static readonly Size Size = default(Size);
        public static readonly Rect Rect = default(Rect);
        public static readonly bool False = false;
        public static readonly bool True = true;
        public static readonly double DoubleHalf = 0.5d;
        public static readonly double Double0 = 0d;
        public static readonly double Double1 = 1d;
        public static readonly double Double2 = 2d;
        public static readonly double Double5 = 5d;
        public static readonly double Double45 = 45d;
        public static readonly double Double1000 = 1000d;
        public static readonly int Int0 = 0;
        public static readonly int Int1 = 1;
        public static readonly uint UInt1 = 1u;
        public static readonly uint UInt0 = 0u;

        public static readonly Thickness Thickness2 = new Thickness(2);
        public static readonly Size ArrowSize = new Size(8, 8);
        public static readonly Size ConnectionOffset = new Size(14, 0);
    }
}
