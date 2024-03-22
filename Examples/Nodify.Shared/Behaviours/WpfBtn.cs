using System;
using Avalonia.LogicalTree;

namespace Nodify.Shared.Behaviours;

/// <summary>
/// Button with CommandTarget for WPF compatibility.
/// </summary>
public class WpfBtn : Button
{
    public static readonly StyledProperty<IInputElement?> CommandTargetProperty = AvaloniaProperty.Register<WpfBtn, IInputElement?>(nameof(CommandTarget));

    protected override Type StyleKeyOverride => typeof(Button);

    public IInputElement? CommandTarget
    {
        get => (IInputElement?)GetValue(CommandTargetProperty);
        set => SetValue(CommandTargetProperty, value);
    }
    
    protected override void OnClick()
    {
        if (Command is RoutedCommand routedCommand)
        {
            var target = CommandTarget ?? this;
            
            if (routedCommand.CanExecute(CommandParameter, target))
                routedCommand.Execute(CommandParameter, target);
        }
        else
            base.OnClick();
    }

    protected override bool IsEnabledCore => IsEnabled;

    private void CanExecuteChanged(object? sender, EventArgs e)
    {
        if (Command is RoutedCommand routedCommand)
        {
            IsEnabled = routedCommand.CanExecute(CommandParameter, CommandTarget ?? this);
        }
        else
        {
            IsEnabled = Command?.CanExecute(CommandParameter) ?? false;
        }
        UpdateIsEffectivelyEnabled();
    }
    
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        if (change.Property == CommandProperty)
        {
            if (((ILogical)this).IsAttachedToLogicalTree)
            {
                var (oldValue, newValue) = change.GetOldAndNewValue<ICommand?>();
                if (oldValue is ICommand oldCommand)
                {
                    oldCommand.CanExecuteChanged -= CanExecuteChanged;
                }

                if (newValue is ICommand newCommand)
                {
                    newCommand.CanExecuteChanged += CanExecuteChanged;
                }
            }

            CanExecuteChanged(this, EventArgs.Empty);
        }
        else if (change.Property == CommandParameterProperty)
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
        else
            base.OnPropertyChanged(change);
    }
    
    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnAttachedToLogicalTree(e);

        if (Command != null)
        {
            Command.CanExecuteChanged += CanExecuteChanged;
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromLogicalTree(e);

        if (Command != null)
        {
            Command.CanExecuteChanged -= CanExecuteChanged;
        }
    }
}