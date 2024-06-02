namespace Nodify.Compatibility;

public sealed class CanExecuteRoutedEventArgs : RoutedEventArgs
{
    public ICommand Command { get; }

    public object? Parameter { get; }

    public bool CanExecute { get; set; }

    public CanExecuteRoutedEventArgs(ICommand command, object? parameter)
    {
        Command = command ?? throw new ArgumentNullException(nameof(command));
        Parameter = parameter;
        RoutedEvent = RoutedCommand.CanExecuteEvent;
    }
}