using Nodify.Interactivity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify
{
    internal class ConnectionContainer : ContentPresenter
    {
        #region Dependency properties

        public static readonly DependencyProperty IsSelectableProperty = DependencyProperty.Register(nameof(IsSelectable), typeof(bool), typeof(ConnectionContainer), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty IsSelectedProperty = System.Windows.Controls.Primitives.Selector.IsSelectedProperty.AddOwner(typeof(ConnectionContainer), new FrameworkPropertyMetadata(BoxValue.False, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsSelectedChanged));

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elem = (ConnectionContainer)d;
            bool result = elem.IsSelectable && (bool)e.NewValue;
            elem.IsSelected = result;
            elem.OnSelectedChanged(result);
        }

        /// <summary>
        /// Gets or sets whether this <see cref="ConnectionContainer"/> can be selected.
        /// </summary>
        public bool IsSelectable
        {
            get => BaseConnection.GetIsSelectable(Connection ?? this);
            set => BaseConnection.SetIsSelectable(Connection ?? this, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="ConnectionContainer"/> is selected.
        /// Can only be set if <see cref="IsSelectable"/> is true.
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        #endregion

        #region Routed events

        public static readonly RoutedEvent SelectedEvent = System.Windows.Controls.Primitives.Selector.SelectedEvent.AddOwner(typeof(ConnectionContainer));
        public static readonly RoutedEvent UnselectedEvent = System.Windows.Controls.Primitives.Selector.UnselectedEvent.AddOwner(typeof(ConnectionContainer));

        /// <summary>
        /// Occurs when this <see cref="ConnectionContainer"/> is selected.
        /// </summary>
        public event RoutedEventHandler Selected
        {
            add => AddHandler(SelectedEvent, value);
            remove => RemoveHandler(SelectedEvent, value);
        }

        /// <summary>
        /// Occurs when this <see cref="ConnectionContainer"/> is unselected.
        /// </summary>
        public event RoutedEventHandler Unselected
        {
            add => AddHandler(UnselectedEvent, value);
            remove => RemoveHandler(UnselectedEvent, value);
        }

        #endregion

        private ConnectionsMultiSelector Selector { get; }

        private UIElement? _connection;
        private UIElement? Connection => _connection ??= BaseConnection.PrioritizeBaseConnectionForSelection
            ? this.GetChildOfType<BaseConnection>() ?? this.GetChildOfType<UIElement>()
            : this.GetChildOfType<UIElement>();

        internal ConnectionContainer(ConnectionsMultiSelector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Raises the <see cref="SelectedEvent"/> or <see cref="UnselectedEvent"/> based on <paramref name="newValue"/>.
        /// Called when the <see cref="IsSelected"/> value is changed.
        /// </summary>
        /// <param name="newValue">True if selected, false otherwise.</param>
        protected void OnSelectedChanged(bool newValue)
        {
            BaseConnection.SetIsSelected(Connection, newValue);

            RaiseEvent(new RoutedEventArgs(newValue ? SelectedEvent : UnselectedEvent, this));
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (IsSelectable && EditorGestures.Mappings.Connection.Selection.Select.Matches(e.Source, e))
            {
                e.Handled = true;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            EditorGestures.ConnectionGestures gestures = EditorGestures.Mappings.Connection;
            if (gestures.Selection.Select.Matches(e.Source, e))
            {
                var selectionType = gestures.Selection.GetSelectionType(e);
                bool allowContextMenu = e.ChangedButton == MouseButton.Right && IsSelected;
                if (!(selectionType == SelectionType.Replace && allowContextMenu))
                {
                    Select(selectionType);
                }
            }
        }

        /// <summary>
        /// Modifies the selection state of the current item based on the specified selection type.
        /// </summary>
        /// <param name="type">The type of selection to perform.</param>
        private void Select(SelectionType type)
        {
            switch (type)
            {
                case SelectionType.Append:
                    IsSelected = true;
                    break;
                case SelectionType.Remove:
                    IsSelected = false;
                    break;
                case SelectionType.Invert:
                    IsSelected = !IsSelected;
                    break;
                case SelectionType.Replace:
                    Selector.UnselectAll();
                    IsSelected = true;
                    break;
            }
        }
    }
}
