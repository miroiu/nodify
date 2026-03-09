using FluentIcons.Common;
using R3;
using System.Windows.Media;

namespace Nodify.Workflow.Common
{
    internal class CommandViewModel(ReactiveCommand command)
    {
        public ReactiveCommand Command { get; } = command;
        public BindableReactiveProperty<string?> Label { get; } = new(null);
        public BindableReactiveProperty<Icon?> Icon { get; } = new(null);
        public BindableReactiveProperty<IconVariant> IconVariant { get; } = new();
        public BindableReactiveProperty<Color?> IconColor { get; } = new();
    }
}
