using System;
using System.Windows;

namespace Nodify
{
    public interface ILinePoints<TPoints> where TPoints : struct, IPointTuple
    {
        TPoints GetLinePoints(in Point source, in Point target);
    }

    public interface IPointTuple
    {
        int Length { get; }
        Point this[int index] { get; }
        ReadOnlySpan<Point> AsSpan();
    }

    public readonly struct Point2 : IPointTuple
    {
        public Point P0 { get; }
        public Point P1 { get; }

        public Point2(Point p0, Point p1) => (P0, P1) = (p0, p1);

        public void Deconstruct(out Point p0, out Point p1) => (p0, p1) = (P0, P1);

        public int Length => 2;
        public Point this[int i] => i switch { 0 => P0, 1 => P1, _ => throw new IndexOutOfRangeException() };
        public ReadOnlySpan<Point> AsSpan() => new[] { P0, P1 };

        public static implicit operator (Point, Point)(Point2 p) => (p.P0, p.P1);
        public static implicit operator Point2((Point, Point) t) => new(t.Item1, t.Item2);
    }

    public readonly struct Point3 : IPointTuple
    {
        public Point P0 { get; }
        public Point P1 { get; }
        public Point P2 { get; }

        public Point3(Point p0, Point p1, Point p2) => (P0, P1, P2) = (p0, p1, p2);

        public void Deconstruct(out Point p0, out Point p1, out Point p2) => (p0, p1, p2) = (P0, P1, P2);

        public int Length => 3;
        public Point this[int i] => i switch { 0 => P0, 1 => P1, 2 => P2, _ => throw new IndexOutOfRangeException() };
        public ReadOnlySpan<Point> AsSpan() => new[] { P0, P1, P2 };

        public static implicit operator (Point, Point, Point)(Point3 p) => (p.P0, p.P1, p.P2);
        public static implicit operator Point3((Point, Point, Point) t) => new(t.Item1, t.Item2, t.Item3);
    }

    public readonly struct Point4 : IPointTuple
    {
        public Point P0 { get; }
        public Point P1 { get; }
        public Point P2 { get; }
        public Point P3 { get; }

        public Point4(Point p0, Point p1, Point p2, Point p3) => (P0, P1, P2, P3) = (p0, p1, p2, p3);

        public void Deconstruct(out Point p0, out Point p1, out Point p2, out Point p3) => (p0, p1, p2, p3) = (P0, P1, P2, P3);

        public int Length => 4;
        public Point this[int i] => i switch { 0 => P0, 1 => P1, 2 => P2, 3 => P3, _ => throw new IndexOutOfRangeException() };
        public ReadOnlySpan<Point> AsSpan() => new[] { P0, P1, P2, P3 };

        public static implicit operator (Point, Point, Point, Point)(Point4 p) => (p.P0, p.P1, p.P2, p.P3);
        public static implicit operator Point4((Point, Point, Point, Point) t) => new(t.Item1, t.Item2, t.Item3, t.Item4);
    }
}
