using ObservableCollections;
using R3;
using System.Windows;

namespace Nodify.Workflow.Designer;

internal sealed class ClampedViewportProperty(WorkflowDesignerViewModel designer)
    : BindableReactiveProperty<Point>()
{
    private const int _edgePadding = 200;

    protected override void OnValueChanging(ref Point value)
    {
        var viewportSize = designer.ViewportSize.Value;
        if (viewportSize.Width == 0 || viewportSize.Height == 0)
        {
            return;
        }

        var (min, max) = GetViewportBounds(designer.Steps, viewportSize);
        value = new Point(Math.Clamp(value.X, min.X, max.X), Math.Clamp(value.Y, min.Y, max.Y));
    }

    private static (Point, Point) GetViewportBounds(ObservableList<WorkflowStepViewModel> steps, Size viewportSize)
    {
        if (steps.Count == 0)
        {
            return (new Point(double.MinValue, double.MinValue), new Point(double.MaxValue, double.MaxValue));
        }

        double minX = double.MaxValue;
        double minY = double.MaxValue;
        double maxX = double.MinValue;
        double maxY = double.MinValue;

        foreach (var step in steps)
        {
            var pos = step.Position.Value;
            var size = step.Size.Value;

            minX = Math.Min(minX, pos.X);
            minY = Math.Min(minY, pos.Y);
            maxX = Math.Max(maxX, pos.X + size.Width);
            maxY = Math.Max(maxY, pos.Y + size.Height);
        }

        double minBoundX = minX - _edgePadding;
        double minBoundY = minY - _edgePadding;
        double maxBoundX = maxX + _edgePadding - viewportSize.Width;
        double maxBoundY = maxY + _edgePadding - viewportSize.Height;

        return (
            new Point(Math.Min(minBoundX, maxBoundX), Math.Min(minBoundY, maxBoundY)),
            new Point(Math.Max(minBoundX, maxBoundX), Math.Max(minBoundY, maxBoundY))
        );
    }
}
