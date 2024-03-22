namespace Nodify.Compatibility;

internal static class StreamGeometryContextExtensions
{
    public static void LineTo(this StreamGeometryContext context, Point point, bool isStroked, bool isSmoothJoin)
    {
        context.LineTo(point);
    }

    public static void BezierTo(this StreamGeometryContext context, 
        Point point1, Point point2,
        Point point3, bool isStroked, bool isSmoothJoin)
    {
        context.CubicBezierTo(point1, point2, point3);
    }

    public static System.IDisposable BeginFigure(this StreamGeometryContext context, Point startPoint, bool isFilled, bool isClosed)
    {
        context.BeginFigure(startPoint, isFilled);
        return new EndFigureOnDispose(context, isClosed);
    }
        
    private struct EndFigureOnDispose : System.IDisposable
    {
        private readonly StreamGeometryContext context;
        private readonly bool isClosed;

        public EndFigureOnDispose(StreamGeometryContext context, bool isClosed)
        {
            this.context = context;
            this.isClosed = isClosed;
        }

        public void Dispose()
        {
            context.EndFigure(isClosed);
        }
    }
}