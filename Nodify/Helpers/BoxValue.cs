using System.Windows;

namespace Nodify
{
    public static class BoxValue
    {
        public static readonly object Point = default(Point);
        public static readonly object Size = default(Size);
        public static readonly object Rect = default(Rect);
        public static readonly object False = false;
        public static readonly object True = true;
        public static readonly object DoubleHalf = 0.5d;
        public static readonly object Double0 = 0d;
        public static readonly object Double1 = 1d;
        public static readonly object Double2 = 2d;
        public static readonly object Double45 = 45d;
        public static readonly object Int0 = 0;
        public static readonly object Int1 = 1;
        public static readonly object UInt1 = 1u;

        public static readonly object ArrowSize = new Size(7, 6);
    }
}
