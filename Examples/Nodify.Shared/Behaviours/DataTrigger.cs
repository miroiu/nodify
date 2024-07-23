using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Metadata;
using Avalonia.Reactive;
using Avalonia.Xaml.Interactivity;

[assembly: XmlnsDefinition("https://github.com/avaloniaui", "Nodify.Shared.Behaviours")]

namespace Nodify.Shared.Behaviours;

public class DataTrigger : Trigger
{
    private List<IDisposable> activeBindings = new();
    
    /// <summary>
    /// Identifies the <seealso cref="Property"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<string> PropertyProperty =
        AvaloniaProperty.Register<DataTrigger, string>(nameof(Property));
    
    public static readonly StyledProperty<object?> BoundProperty =
        AvaloniaProperty.Register<DataTrigger, object?>(nameof(Bound));

    /// <summary>
    /// Identifies the <seealso cref="ComparisonCondition"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<ComparisonConditionType> ComparisonConditionProperty =
        AvaloniaProperty.Register<DataTrigger, ComparisonConditionType>(nameof(ComparisonCondition));

    /// <summary>
    /// Identifies the <seealso cref="Value"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<object?> ValueProperty =
        AvaloniaProperty.Register<DataTrigger, object?>(nameof(Value));
    
    public string Property
    {
        get => GetValue(PropertyProperty);
        set => SetValue(PropertyProperty, value);
    }
    
    public object? Bound
    {
        get => GetValue(BoundProperty);
        set => SetValue(BoundProperty, value);
    }

    /// <summary>
    /// Gets or sets the type of comparison to be performed between <see cref="Property"/> and <see cref="DataTrigger.Value"/>. This is a avalonia property.
    /// </summary>
    public ComparisonConditionType ComparisonCondition
    {
        get => GetValue(ComparisonConditionProperty);
        set => SetValue(ComparisonConditionProperty, value);
    }

    /// <summary>
    /// Gets or sets the value to be compared with the value of <see cref="Property"/>. This is a avalonia property.
    /// </summary>
    public object? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public bool UseDataContext { get; set; } = true;
    
    public object? Source { get; set; }

    protected override void OnAttached()
    {
        base.OnAttached();
        this.Bind(BoundProperty, new Binding(Source == null && UseDataContext ? (Property == "." ? "DataContext" : $"DataContext.{Property}") : Property) { Source = Source ?? AssociatedObject });
    }

    static DataTrigger()
    {
        BoundProperty.Changed.Subscribe(
            (IObserver<AvaloniaPropertyChangedEventArgs<object>>)new AnonymousObserver<AvaloniaPropertyChangedEventArgs<object?>>(OnValueChanged));

        ComparisonConditionProperty.Changed.Subscribe(
            (IObserver<AvaloniaPropertyChangedEventArgs<ComparisonConditionType>>)new AnonymousObserver<AvaloniaPropertyChangedEventArgs<ComparisonConditionType>>(OnValueChanged));

        ValueProperty.Changed.Subscribe(
            (IObserver<AvaloniaPropertyChangedEventArgs<object>>)new AnonymousObserver<AvaloniaPropertyChangedEventArgs<object?>>(OnValueChanged));
    }

    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
    private static bool Compare(object? leftOperand, ComparisonConditionType operatorType, object? rightOperand)
    {
        if (leftOperand is not null && rightOperand is not null &&
            leftOperand.GetType() != rightOperand.GetType())
        {
            var value = rightOperand.ToString();
            var destinationType = leftOperand.GetType();
            if (value is not null)
            {
                rightOperand = TypeConverterHelper.Convert(value, destinationType);
            }
        }

        var leftComparableOperand = leftOperand as IComparable;
        var rightComparableOperand = rightOperand as IComparable;
        if (leftComparableOperand is not null && rightComparableOperand is not null)
        {
            return EvaluateComparable(leftComparableOperand, operatorType, rightComparableOperand);
        }

        switch (operatorType)
        {
            case ComparisonConditionType.Equal:
                return Equals(leftOperand, rightOperand);

            case ComparisonConditionType.NotEqual:
                return !Equals(leftOperand, rightOperand);

            case ComparisonConditionType.LessThan:
            case ComparisonConditionType.LessThanOrEqual:
            case ComparisonConditionType.GreaterThan:
            case ComparisonConditionType.GreaterThanOrEqual:
            {
                throw leftComparableOperand switch
                {
                    null when rightComparableOperand is null => new ArgumentException(string.Format(
                        CultureInfo.CurrentCulture,
                        "Binding property of type {0} and Value property of type {1} cannot be used with operator {2}.",
                        leftOperand?.GetType().Name ?? "null", rightOperand?.GetType().Name ?? "null",
                        operatorType.ToString())),
                    null => new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                        "Binding property of type {0} cannot be used with operator {1}.",
                        leftOperand?.GetType().Name ?? "null", operatorType.ToString())),
                    _ => new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                        "Value property of type {0} cannot be used with operator {1}.",
                        rightOperand?.GetType().Name ?? "null", operatorType.ToString()))
                };
            }
        }

        return false;
    }

    /// <summary>
    /// Evaluates both operands that implement the IComparable interface.
    /// </summary>
    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
    private static bool EvaluateComparable(IComparable leftOperand, ComparisonConditionType operatorType, IComparable rightOperand)
    {
        object? convertedOperand = null;
        try
        {
            convertedOperand = Convert.ChangeType(rightOperand, leftOperand.GetType(), CultureInfo.CurrentCulture);
        }
        catch (FormatException)
        {
            // FormatException: Convert.ChangeType("hello", typeof(double), ...);
        }
        catch (InvalidCastException)
        {
            // InvalidCastException: Convert.ChangeType(4.0d, typeof(Rectangle), ...);
        }

        if (convertedOperand is null)
        {
            return operatorType == ComparisonConditionType.NotEqual;
        }

        var comparison = leftOperand.CompareTo((IComparable)convertedOperand);
        return operatorType switch
        {
            ComparisonConditionType.Equal => comparison == 0,
            ComparisonConditionType.NotEqual => comparison != 0,
            ComparisonConditionType.LessThan => comparison < 0,
            ComparisonConditionType.LessThanOrEqual => comparison <= 0,
            ComparisonConditionType.GreaterThan => comparison > 0,
            ComparisonConditionType.GreaterThanOrEqual => comparison >= 0,
            _ => false
        };
    }

    private static void OnValueChanged(AvaloniaPropertyChangedEventArgs args)
    {
        if (args.Sender is not DataTrigger behavior || behavior.AssociatedObject is null)
        {
            return;
        }

        // NOTE: In UWP version binding null check is not present but Avalonia throws exception as Bindings are null when first initialized.
        var binding = behavior.Bound;
        if (binding is not null)
        {
            foreach (var b in behavior.activeBindings)
                b.Dispose();
            behavior.activeBindings.Clear();
            // Some value has changed--either the binding value, reference value, or the comparison condition. Re-evaluate the equation.
            if (Compare(behavior.Bound, behavior.ComparisonCondition, behavior.Value))
            {
                foreach (var result in Interaction.ExecuteActions(behavior.AssociatedObject, behavior.Actions, args))
                {
                    if (result is IDisposable disposable)
                        behavior.activeBindings.Add(disposable);
                }
            }
        }
    }
}
