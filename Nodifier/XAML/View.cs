using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Nodifier.XAML
{
    /// Source: https://github.com/canton7/Stylet
    /// <summary>
    /// Holds attached properties relating to various bits of the View
    /// </summary>
    public static class View
    {
        public static IViewManager? ViewManager { get; set; }

        /// <summary>
        /// Initial value of the ActionTarget property.
        /// This can be used as a marker - if the property has this value, it hasn't yet been assigned to anything else.
        /// </summary>
        public static readonly object InitialActionTarget = new object();

        /// <summary>
        /// Get the ActionTarget associated with the given object
        /// </summary>
        /// <param name="obj">Object to fetch the ActionTarget for</param>
        /// <returns>ActionTarget associated with the given object</returns>
        public static object GetActionTarget(DependencyObject obj)
        {
            return obj.GetValue(ActionTargetProperty);
        }

        /// <summary>
        /// Set the ActionTarget associated with the given object
        /// </summary>
        /// <param name="obj">Object to set the ActionTarget for</param>
        /// <param name="value">Value to set the ActionTarget to</param>
        public static void SetActionTarget(DependencyObject obj, object value)
        {
            obj.SetValue(ActionTargetProperty, value);
        }

        /// <summary>
        /// The object's ActionTarget. This is used to determine what object to call Actions on by the ActionExtension markup extension.
        /// </summary>
        public static readonly DependencyProperty ActionTargetProperty =
            DependencyProperty.RegisterAttached("ActionTarget", typeof(object), typeof(View), new FrameworkPropertyMetadata(InitialActionTarget, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Fetch the ViewModel currently associated with a given object
        /// </summary>
        /// <param name="obj">Object to fetch the ViewModel for</param>
        /// <returns>ViewModel currently associated with the given object</returns>
        public static object GetModel(DependencyObject obj)
        {
            return obj.GetValue(ModelProperty);
        }

        /// <summary>
        /// Set the ViewModel currently associated with a given object
        /// </summary>
        /// <param name="obj">Object to set the ViewModel for</param>
        /// <param name="value">ViewModel to set</param>
        public static void SetModel(DependencyObject obj, object value)
        {
            obj.SetValue(ModelProperty, value);
        }

        private static readonly object defaultModelValue = new object();

        /// <summary>
        /// Property specifying the ViewModel currently associated with a given object
        /// </summary>
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.RegisterAttached("Model", typeof(object), typeof(View), new PropertyMetadata(defaultModelValue, (d, e) =>
            {
                if (ViewManager != null)
                {
                    object? newValue = e.NewValue == defaultModelValue ? null : e.NewValue;
                    ViewManager.OnModelChanged(d, e.OldValue, newValue);
                }
                else
                {
                    throw new InvalidOperationException($"View.{nameof(ViewManager)} must be set before using View.Model");
                }
            }));

        /// <summary>
        /// Helper to set the Content property of a given object to a particular View
        /// </summary>
        /// <param name="targetLocation">Object to set the Content property on</param>
        /// <param name="view">View to set as the object's Content</param>
        public static void SetContentProperty(DependencyObject targetLocation, UIElement? view)
        {
            var type = targetLocation.GetType();
            if (targetLocation is ContentControl contentControl)
            {
                contentControl.Content = view;
            }
            else
            {
                var attribute = type.GetCustomAttribute<ContentPropertyAttribute>();

                if (attribute != null)
                {
                    var property = type.GetProperty(attribute.Name);
                    if (property == null)
                        throw new InvalidOperationException($"Unable to find a Content property on type {type.Name}. Make sure you're using 's:View.Model' on a suitable container, e.g. a ContentControl");
                    property.SetValue(targetLocation, view);
                }
                else
                {
                    throw new InvalidOperationException($"Unable to find a Content property on type {type.Name}. Make sure you're using 's:View.Model' on a suitable container, e.g. a ContentControl");
                }
            }
        }
    }
}
