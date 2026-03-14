using ObservableCollections;
using R3;
using System.Windows;

namespace Nodify.Workflow.Designer;

internal sealed class ClampedViewportProperty<T>(ObservableList<WorkflowStepViewModel> steps) : BindableReactiveProperty<Point>
    where T : IViewportSizeAware
{
    private double _minX = double.MaxValue;
    private double _minY = double.MaxValue;
    private double _maxX = double.MinValue;
    private double _maxY = double.MinValue;

    private readonly Dictionary<WorkflowStepViewModel, IDisposable> _stepSubscriptions = [];

    public int EdgePadding { get; set; } = 50;

    public void ObserveStepChanges()
    {
        // Subscribe to existing steps
        foreach (var step in steps)
        {
            SubscribeToStep(step);
        }

        // Subscribe to collection changes
        steps.ObserveAdd().Subscribe(e => SubscribeToStep(e.Value));
        steps.ObserveRemove().Subscribe(e => UnsubscribeFromStep(e.Value));
        steps.ObserveClear().Subscribe(_ => ClearSubscriptions());
    }

    private void SubscribeToStep(WorkflowStepViewModel step)
    {
        var subscription = new CompositeDisposable
        {
            step.Position.Subscribe(_ => RecalculateBounds()),
            step.Size.Subscribe(_ => RecalculateBounds())
        };
        _stepSubscriptions[step] = subscription;
        RecalculateBounds();
    }

    private void UnsubscribeFromStep(WorkflowStepViewModel step)
    {
        if (_stepSubscriptions.Remove(step, out var subscription))
        {
            subscription.Dispose();
        }
        RecalculateBounds();
    }

    private void ClearSubscriptions()
    {
        foreach (var subscription in _stepSubscriptions.Values)
        {
            subscription.Dispose();
        }
        _stepSubscriptions.Clear();
        RecalculateBounds();
    }

    private void RecalculateBounds()
    {
        if (steps.Count == 0)
        {
            _minX = double.MaxValue;
            _minY = double.MaxValue;
            _maxX = double.MinValue;
            _maxY = double.MinValue;
            return;
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

        _minX = minX;
        _minY = minY;
        _maxX = maxX;
        _maxY = maxY;
    }

    protected override void OnValueChanging(ref Point value)
    {
        var viewportSize = T.ViewportSize.Value;
        if (viewportSize.Width == 0 || viewportSize.Height == 0)
        {
            return;
        }

        var (min, max) = GetViewportBounds(viewportSize);
        value = new Point(Math.Clamp(value.X, min.X, max.X), Math.Clamp(value.Y, min.Y, max.Y));
    }

    private (Point, Point) GetViewportBounds(Size viewportSize)
    {
        if (steps.Count == 0)
        {
            return (new Point(double.MinValue, double.MinValue), new Point(double.MaxValue, double.MaxValue));
        }

        double minBoundX = _minX - EdgePadding;
        double minBoundY = _minY - EdgePadding;
        double maxBoundX = _maxX + EdgePadding - viewportSize.Width;
        double maxBoundY = _maxY + EdgePadding - viewportSize.Height;

        return (
            new Point(Math.Min(minBoundX, maxBoundX), Math.Min(minBoundY, maxBoundY)),
            new Point(Math.Max(minBoundX, maxBoundX), Math.Max(minBoundY, maxBoundY))
        );
    }
}
