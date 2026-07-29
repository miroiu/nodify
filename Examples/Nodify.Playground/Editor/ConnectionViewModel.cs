using System;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Playground
{
    public class ConnectionViewModel : ObservableObject
    {
        private static class Commands
        {
            public sealed class DisconnectCommand : ICommand
            {
                public static readonly DisconnectCommand Instance = new DisconnectCommand();

                private DisconnectCommand() { }

                public event EventHandler? CanExecuteChanged
                {
                    add { }
                    remove { }
                }

                public bool CanExecute(object? parameter) => parameter is ConnectionViewModel;

                public void Execute(object? parameter) => ((ConnectionViewModel)parameter!).Remove();
            }
        }

        private NodifyEditorViewModel _graph = default!;
        public NodifyEditorViewModel Graph
        {
            get => _graph;
            internal set => SetProperty(ref _graph, value);
        }

        private ConnectorViewModel _input = default!;
        public ConnectorViewModel Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        private ConnectorViewModel _output = default!;
        public ConnectorViewModel Output
        {
            get => _output;
            set => SetProperty(ref _output, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public ICommand SplitCommand { get; }
        public ICommand DisconnectCommand => Commands.DisconnectCommand.Instance;

        public ConnectionViewModel()
        {
            SplitCommand = new DelegateCommand<Point>(Split);
        }

        public void Split(Point point)
            => Graph.Schema.SplitConnection(this, point);

        public void Remove()
            => Graph.Connections.Remove(this);
    }
}
