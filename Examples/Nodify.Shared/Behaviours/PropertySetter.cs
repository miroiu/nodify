using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Xaml.Interactivity;

namespace Nodify.Shared.Behaviours;


/// <summary>
/// An action that will change a specified property to a specified value when invoked.
/// </summary>
public class PropertySetter : AvaloniaObject, IAction
{
    private static readonly char[] s_trimChars = { '(', ')' };
    private static readonly char[] s_separator = { '.' };

    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
    private static Type? GetTypeByName(string name)
    {
        return
            AppDomain.CurrentDomain.GetAssemblies()
                .Reverse()
                .Select(assembly => assembly.GetType(name))
                .FirstOrDefault(t => t is not null)
            ??
            AppDomain.CurrentDomain.GetAssemblies()
                .Reverse()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(t => t.Name == name);
    }

    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
    private static AvaloniaProperty? FindAttachedProperty(object? targetObject, string propertyName)
    {
        if (targetObject is null)
        {
            return null;
        }
        
        var propertyNames = propertyName.Trim().Trim(s_trimChars).Split(s_separator);
        if (propertyNames.Length != 2)
        {
            return null;
        }
        var targetPropertyTypeName = propertyNames[0];
        var targetPropertyName = propertyNames[1];
        var targetType = GetTypeByName(targetPropertyTypeName) ?? targetObject.GetType();

        var registeredAttached = AvaloniaPropertyRegistry.Instance.GetRegisteredAttached(targetType);

        foreach (var avaloniaProperty in registeredAttached)
        {
            if (avaloniaProperty.OwnerType.Name == targetPropertyTypeName && avaloniaProperty.Name == targetPropertyName)
            {
                return avaloniaProperty;
            }
        }

        var registeredInherited = AvaloniaPropertyRegistry.Instance.GetRegisteredInherited(targetType);

        foreach (var avaloniaProperty in registeredInherited)
        {
            if (avaloniaProperty.Name == targetPropertyName)
            {
                return avaloniaProperty;
            }
        }

        return null;
    }

    /// <summary>
    /// Identifies the <seealso cref="Property"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<string> PropertyNameProperty =
        AvaloniaProperty.Register<PropertySetter, string>(nameof(Property));

    /// <summary>
    /// Identifies the <seealso cref="TargetObject"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<object?> TargetObjectProperty =
        AvaloniaProperty.Register<PropertySetter, object?>(nameof(TargetObject));

    /// <summary>
    /// Identifies the <seealso cref="Value"/> avalonia property.
    /// </summary>
    public static readonly StyledProperty<object?> ValueProperty =
        AvaloniaProperty.Register<PropertySetter, object?>(nameof(Value));

    /// <summary>
    /// Gets or sets the name of the property to change. This is a avalonia property.
    /// </summary>
    public string Property
    {
        get => GetValue(PropertyNameProperty);
        set => SetValue(PropertyNameProperty, value);
    }

    /// <summary>
    /// Gets or sets the value to set. This is a avalonia property.
    /// </summary>
    public object? Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <summary>
    /// Gets or sets the object whose property will be changed.
    /// If <seealso cref="TargetObject"/> is not set or cannot be resolved, the sender of <seealso cref="Execute"/> will be used. This is a avalonia property.
    /// </summary>
    [ResolveByName]
    public object? TargetObject
    {
        get => GetValue(TargetObjectProperty);
        set => SetValue(TargetObjectProperty, value);
    }

    /// <summary>
    /// Executes the action.
    /// </summary>
    /// <param name="sender">The <see cref="object"/> that is passed to the action by the behavior. Generally this is <seealso cref="IBehavior.AssociatedObject"/> or a target object.</param>
    /// <param name="parameter">The value of this parameter is determined by the caller.</param>
    /// <returns>True if updating the property value succeeds; else false.</returns>
    public virtual object Execute(object? sender, object? parameter)
    {
        object? targetObject;
        if (GetValue(TargetObjectProperty) is not null)
        {
            targetObject = TargetObject;
        }
        else
        {
            targetObject = sender;
        }

        if (targetObject is null)
        {
            return null!;
        }

        if (targetObject is AvaloniaObject avaloniaObject)
        {
            if (Property.Contains('.'))
            {
                var avaloniaProperty = FindAttachedProperty(targetObject, Property);
                if (avaloniaProperty is not null)
                {
                    return UpdateAvaloniaPropertyValue(avaloniaObject, avaloniaProperty)!;
                }

                return null!;
            }
            else
            {
                var avaloniaProperty = AvaloniaPropertyRegistry.Instance.FindRegistered(avaloniaObject, Property);
                if (avaloniaProperty is not null)
                {
                    return UpdateAvaloniaPropertyValue(avaloniaObject, avaloniaProperty)!;
                }
            }
        }

        UpdatePropertyValue(targetObject);
        return null!;
    }

    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
    private void UpdatePropertyValue(object targetObject)
    {
        var targetType = targetObject.GetType();
        var targetTypeName = targetType.Name;
        var propertyInfo = targetType.GetRuntimeProperty(Property);

        if (propertyInfo is null)
        {
            throw new ArgumentException(string.Format(
                CultureInfo.CurrentCulture,
                "Cannot find a property named {0} on type {1}.",
                Property,
                targetTypeName));
        }
        else if (!propertyInfo.CanWrite)
        {
            throw new ArgumentException(string.Format(
                CultureInfo.CurrentCulture,
                "Cannot find a property named {0} on type {1}.",
                Property,
                targetTypeName));
        }

        Exception? innerException = null;
        try
        {
            object? result = null;
            var propertyType = propertyInfo.PropertyType;
            var propertyTypeInfo = propertyType.GetTypeInfo();
            if (Value is null)
            {
                // The result can be null if the type is generic (nullable), or the default value of the type in question
                result = propertyTypeInfo.IsValueType ? Activator.CreateInstance(propertyType) : null;
            }
            else if (propertyTypeInfo.IsAssignableFrom(Value.GetType().GetTypeInfo()))
            {
                result = Value;
            }
            else
            {
                var valueAsString = Value.ToString();
                if (valueAsString is not null)
                {
                    result = propertyTypeInfo.IsEnum ? Enum.Parse(propertyType, valueAsString, false) :
                        TypeConverterHelper.Convert(valueAsString, propertyType);
                }
            }

            propertyInfo.SetValue(targetObject, result, Array.Empty<object>());
        }
        catch (FormatException e)
        {
            innerException = e;
        }
        catch (ArgumentException e)
        {
            innerException = e;
        }

        if (innerException is not null)
        {
            throw new ArgumentException(string.Format(
                    CultureInfo.CurrentCulture,
                    "Cannot assign value of type {0} to property {1} of type {2}. The {1} property can be assigned only values of type {2}.",
                    Value?.GetType().Name ?? "null",
                    Property,
                    propertyInfo.PropertyType.Name),
                innerException);
        }
    }

    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
    private IDisposable? UpdateAvaloniaPropertyValue(AvaloniaObject avaloniaObject, AvaloniaProperty property)
    {
        ValidateAvaloniaProperty(property);

        Exception? innerException = null;
        try
        {
            object? result = null;
            var propertyType = property.PropertyType;
            var propertyTypeInfo = propertyType.GetTypeInfo();
            if (Value is null)
            {
                // The result can be null if the type is generic (nullable), or the default value of the type in question
                result = propertyTypeInfo.IsValueType ? Activator.CreateInstance(propertyType) : null;
            }
            else if (propertyTypeInfo.IsAssignableFrom(Value.GetType().GetTypeInfo()))
            {
                result = Value;
            }
            else
            {
                var valueAsString = Value.ToString();
                if (valueAsString is not null)
                {
                    result = propertyTypeInfo.IsEnum ? Enum.Parse(propertyType, valueAsString, false) :
                        TypeConverterHelper.Convert(valueAsString, propertyType);
                }
            }

            return avaloniaObject.SetValue(property, result, BindingPriority.StyleTrigger);
        }
        catch (FormatException e)
        {
            innerException = e;
        }
        catch (ArgumentException e)
        {
            innerException = e;
        }

        if (innerException is not null)
        {
            throw new ArgumentException(string.Format(
                    CultureInfo.CurrentCulture,
                    "Cannot assign value of type {0} to property {1} of type {2}. The {1} property can be assigned only values of type {2}.",
                    Value?.GetType().Name ?? "null",
                    Property,
                    avaloniaObject.GetType().Name),
                innerException);
        }

        return null;
    }

    /// <summary>
    /// Ensures the property is not null and can be written to.
    /// </summary>
    private void ValidateAvaloniaProperty(AvaloniaProperty? property)
    {
        if (property is null)
        {
            throw new ArgumentException(string.Format(
                CultureInfo.CurrentCulture,
                "Cannot find a property named {0}.",
                Property));
        }
        else if (property.IsReadOnly)
        {
            throw new ArgumentException(string.Format(
                CultureInfo.CurrentCulture,
                "Cannot find a property named {0}.",
                Property));
        }
    }
}

internal static class TypeConverterHelper
{
    /// <summary>
    /// Converts string representation of a value to its object representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="destinationType">The destination type.</param>
    /// <returns>Object representation of the string value.</returns>
    /// <exception cref="ArgumentNullException">destinationType cannot be null.</exception>
    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
    public static object? Convert(string value, Type destinationType)
    {
        if (destinationType is null)
        {
            throw new ArgumentNullException(nameof(destinationType));
        }

        var destinationTypeFullName = destinationType.FullName;
        if (destinationTypeFullName is null)
        {
            return null;
        }

        var scope = GetScope(destinationTypeFullName);

        // Value types in the "System" namespace must be special cased due to a bug in the xaml compiler
        if (string.Equals(scope, "System", StringComparison.Ordinal))
        {
            if (string.Equals(destinationTypeFullName, typeof(string).FullName, StringComparison.Ordinal))
            {
                return value;
            }

            if (string.Equals(destinationTypeFullName, typeof(bool).FullName, StringComparison.Ordinal))
            {
                return bool.Parse(value);
            }

            if (string.Equals(destinationTypeFullName, typeof(int).FullName, StringComparison.Ordinal))
            {
                return int.Parse(value, CultureInfo.InvariantCulture);
            }

            if (string.Equals(destinationTypeFullName, typeof(double).FullName, StringComparison.Ordinal))
            {
                return double.Parse(value, CultureInfo.InvariantCulture);
            }
        }

        try
        {
            if (destinationType.BaseType == typeof(Enum))
                return Enum.Parse(destinationType, value);

            if (destinationType.GetInterfaces().Any(t => t == typeof(IConvertible)))
            {
                return (value as IConvertible).ToType(destinationType, CultureInfo.InvariantCulture);
            }

            var converter = TypeDescriptor.GetConverter(destinationType);
            return converter.ConvertFromInvariantString(value);
        }
        catch (ArgumentException)
        {
            // not an enum
        }
        catch (InvalidCastException)
        {
            // not able to convert to anything
        }

        return null;
    }

    private static string GetScope(string name)
    {
        var indexOfLastPeriod = name.LastIndexOf('.');
        return indexOfLastPeriod != name.Length - 1 ? name[..indexOfLastPeriod] : name;
    }
}
