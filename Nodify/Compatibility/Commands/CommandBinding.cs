namespace Nodify.Compatibility;

internal class CommandBinding
{
    public CommandBinding(RoutedCommand command,
        EventHandler<ExecutedRoutedEventArgs> executed = null,
        EventHandler<CanExecuteRoutedEventArgs> canExecute = null)
    {
        Command = command;
        if (executed != null) Executed += executed;
        if (canExecute != null) CanExecute += canExecute;
    }

    public RoutedCommand Command { get; }

    public event EventHandler<CanExecuteRoutedEventArgs> CanExecute;

    public event EventHandler<ExecutedRoutedEventArgs> Executed;

    internal bool DoCanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        if (e.Handled) return true;

        var canExecute = CanExecute;
        if (canExecute == null)
        {
            if (Executed != null)
            {
                e.Handled = true;
                e.CanExecute = true;
            }
        }
        else
        {
            canExecute(sender, e);

            if (e.CanExecute)
            {
                e.Handled = true;
            }
        }

        return e.CanExecute;
    }

    internal bool DoExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        if (!e.Handled)
        {
            var executed = Executed;

            if (executed != null)
            {
                if (DoCanExecute(sender, new CanExecuteRoutedEventArgs(e.Command, e.Parameter)))
                {
                    executed(sender, e);
                    e.Handled = true;
                    return true;
                }
            }
        }

        return false;
    }
}