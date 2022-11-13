using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Nodifier
{
    /// <summary>
    /// Base class for things which can raise PropertyChanged events
    /// </summary>
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the dispatcher to use to dispatch PropertyChanged events. Defaults to UI thread.
        /// </summary>
        public virtual Action<Action> PropertyChangedDispatcher { get; set; } = action => Application.Current.Dispatcher.Invoke(action);

        /// <summary>
        /// Occurs when a property value changes
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Fires the PropertyChanged notification.
        /// </summary>
        /// <remarks>Specially named so that Fody.PropertyChanged calls it</remarks>
        /// <param name="propertyName">Name of the property to raise the notification for</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedDispatcher(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));
        }

        /// <summary>
        /// Takes, by reference, a field, and its new value. If field != value, will set field = value and raise a PropertyChanged notification
        /// </summary>
        /// <typeparam name="T">Type of field being set and notified</typeparam>
        /// <param name="field">Field to assign</param>
        /// <param name="value">Value to assign to the field, if it differs</param>
        /// <param name="propertyName">Name of the property to notify for. Defaults to the calling property</param>
        /// <returns>True if field != value and a notification was raised; false otherwise</returns>
        protected virtual bool SetAndNotify<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }

            return false;
        }
    }
}
