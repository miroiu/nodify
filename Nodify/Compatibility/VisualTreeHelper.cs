namespace Nodify.Compatibility;

internal static class VisualTreeHelper
{
    public static DependencyObject? GetParent(DependencyObject? x)
    {
        return (x as Visual)?.Parent;
    }

    public static void HitTest(UIElement element,
        Func<DependencyObject, HitTestFilterBehavior> filter, 
        Func<HitTestResult, HitTestResultBehavior> test,
        PointHitTestParameters parameters)
    {
        DependencyObject? topMost = element.InputHitTest(parameters.Position) as DependencyObject;
        while (topMost != null)
        {
            if (filter(topMost) == HitTestFilterBehavior.Stop)
            {
                break;
            }
            topMost = GetParent(topMost);
        }
    }
}

internal class PointHitTestParameters
{
    public PointHitTestParameters(Point position)
    {
        Position = position;
    }

    public Point Position { get; }
}

internal enum HitTestFilterBehavior
{
    Continue,
    ContinueSkipSelfAndChildren,
    Stop
}

internal enum HitTestResultBehavior
{
    Continue,
    Stop
}

internal class HitTestResult
{
    public HitTestResult(Visual visualHit)
    {
        VisualHit = visualHit;
    }

    public Visual VisualHit { get; }
}