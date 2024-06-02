namespace Nodify.Compatibility;

// Ported from https://github.com/AvaloniaUI/AvaloniaEdit/blob/master/src/AvaloniaEdit/RoutedCommand.cs
public class RoutedCommand : ICommand
{
    private static IInputElement? _focusedElement;

    public string Name { get; }
    public Type? OwnerType { get; }
    public InputGestureCollection InputGestures { get; }
        
    public RoutedCommand(string name, Type? ownerType)
        : this(name, ownerType, new InputGestureCollection())
    {
    }
        
    public RoutedCommand(string name, Type? ownerType, InputGesture inputGesture)
        : this(name, ownerType, new InputGestureCollection { inputGesture })
    {
    }

    public RoutedCommand(string name, Type? ownerType, InputGestureCollection inputGestures)
    {
        Name = name;
        OwnerType = ownerType;
        InputGestures = inputGestures;
    }

    static RoutedCommand()
    {
        CanExecuteEvent.AddClassHandler<Interactive>(CanExecuteEventHandler);
        ExecutedEvent.AddClassHandler<Interactive>(ExecutedEventHandler);
        InputElement.GotFocusEvent.AddClassHandler<Interactive>(GotFocusEventHandler);
        InputElement.LostFocusEvent.AddClassHandler<Interactive>(LostFocusEventHandler);
        Popup.IsOpenProperty.Changed.AddClassHandler<Popup>(PopupIsOpenChanged);
    }

    private static void PopupIsOpenChanged(Popup popup, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.GetNewValue<bool>())
            _focusedElement = popup;
        else if (ReferenceEquals(_focusedElement, popup) || popup.IsVisualAncestorOf(_focusedElement as Visual))
            _focusedElement = null;
        CommandManager.InvalidateRequerySuggested();
    }

    private static void GotFocusEventHandler(Interactive focused, GotFocusEventArgs e)
    {
        _focusedElement = focused as IInputElement;
    }

    private static void LostFocusEventHandler(Interactive arg1, RoutedEventArgs arg2)
    {
        if (ReferenceEquals(_focusedElement, arg1))
            _focusedElement = null;
    }

    private static void CanExecuteEventHandler(Interactive? control, CanExecuteRoutedEventArgs args)
    {
        while (control != null)
        {
            if (GetCommandBindings(control) is {} commandBindings)
            {
                var command = commandBindings.Where(c => c != null).FirstOrDefault(c => c.Command == args.Command);
                if (command != null)
                {
                    args.CanExecute = command.DoCanExecute(control, args);
                    break;
                }
            }

            if (CommandManager.staticCommandBindings.TryGetValue(control.GetType(), out var commands))
            {
                var command = commands.Where(c => c != null).FirstOrDefault(c => c.Command == args.Command);
                if (command != null)
                {
                    args.CanExecute = command.DoCanExecute(control, args);
                    break;
                }
            }

            if (control is PopupRoot popup)
                control = ((IHostedVisualTreeRoot)popup).Host as Interactive;
            else
                control = control.Parent as Interactive;
        }
    }

    private static void ExecutedEventHandler(Interactive? control, ExecutedRoutedEventArgs args)
    {
        while (control != null)
        {
            if (GetCommandBindings(control) is {} commandBindings)
            {
                var command = commandBindings.Where(c => c != null).FirstOrDefault(c => c.Command == args.Command);
                if (command != null)
                {
                    command.DoExecuted(control, args);
                    break;
                }
            }

            if (CommandManager.staticCommandBindings.TryGetValue(control.GetType(), out var commands))
            {
                var command = commands.Where(c => c != null).FirstOrDefault(c => c.Command == args.Command);
                if (command != null)
                {
                    command.DoExecuted(control, args);
                    break;
                }
            }

            if (control is PopupRoot popup)
                control = ((IHostedVisualTreeRoot)popup).Host as Interactive;
            else
                control = control.Parent as Interactive;
        }
    }

    public static RoutedEvent<CanExecuteRoutedEventArgs> CanExecuteEvent { get; }
        = RoutedEvent.Register<CanExecuteRoutedEventArgs>(nameof(CanExecuteEvent), RoutingStrategies.Bubble, typeof(RoutedCommand));

    public bool CanExecute(object? parameter, IInputElement? target)
    {
        if (target == null) return false;

        var args = new CanExecuteRoutedEventArgs(this, parameter);
        target.RaiseEvent(args);

        return args.CanExecute;
    }

    bool ICommand.CanExecute(object? parameter)
    {
        return CanExecute(parameter, _focusedElement);
    }

    public static RoutedEvent<ExecutedRoutedEventArgs> ExecutedEvent { get; }
        = RoutedEvent.Register<ExecutedRoutedEventArgs>(nameof(ExecutedEvent), RoutingStrategies.Bubble, typeof(RoutedCommand));

    public void Execute(object? parameter, IInputElement? target)
    {
        if (target == null) 
            return;

        var args = new ExecutedRoutedEventArgs(this, parameter);
        target.RaiseEvent(args);
    }

    void ICommand.Execute(object? parameter)
    {
        Execute(parameter, _focusedElement);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
        
    private static readonly AttachedProperty<IList<CommandBinding>?> CommandBindingsProperty 
        = AvaloniaProperty.RegisterAttached<RoutedCommand, Interactive, IList<CommandBinding>?>("CommandBinding", null);
        
    internal static IList<CommandBinding>? GetCommandBindings(Interactive elem)
        => elem.GetValue(CommandBindingsProperty);

    internal static void SetCommandBindings(Interactive elem, IList<CommandBinding>? value)
        => elem.SetValue(CommandBindingsProperty, value);
}