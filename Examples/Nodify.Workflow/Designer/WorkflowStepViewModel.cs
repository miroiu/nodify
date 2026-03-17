using FluentIcons.Common;
using R3;
using System.Windows;
using System.Windows.Media;

namespace Nodify.Workflow.Designer;

internal sealed class WorkflowStepViewModel(string name)
{
    public BindableReactiveProperty<string> Name { get; } = new(name);
    public BindableReactiveProperty<Icon?> Icon { get; } = new(null);
    public BindableReactiveProperty<Color> IconColor { get; } = new(Colors.White);

    public BindableReactiveProperty<Point> Position { get; } = new();
    public BindableReactiveProperty<Size> Size { get; } = new();
    public BindableReactiveProperty<Point> InAnchorPosition { get; } = new();
    public BindableReactiveProperty<Point> OutAnchorPosition { get; } = new();
}
