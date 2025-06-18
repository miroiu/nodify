using Nodify.Interactivity;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Nodify
{
    public class ConnectionContainer : ContentPresenter, IKeyboardFocusTarget<ConnectionContainer>
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

        private FrameworkElement? _connection;
        private SelectionType? _selectionType;

        public Rect Bounds => ConnectionFocusTarget.Bounds;
        ConnectionContainer IKeyboardFocusTarget<ConnectionContainer>.Element => this;

        private IKeyboardFocusTarget<FrameworkElement> ConnectionFocusTarget => Connection as IKeyboardFocusTarget<FrameworkElement>
            ?? throw new NotSupportedException($"Custom connections must implement {nameof(IKeyboardFocusTarget<FrameworkElement>)} for keyboard navigation. Or disable keyboard navigation for the connections layer.");

        public FrameworkElement? Connection => _connection ??= BaseConnection.PrioritizeBaseConnectionForSelection
            ? this.GetChildOfType<BaseConnection>() ?? this.GetChildOfType<FrameworkElement>()
            : this.GetChildOfType<FrameworkElement>();

        public ConnectionsMultiSelector Selector { get; }

        static ConnectionContainer()
        {
            FocusableProperty.OverrideMetadata(typeof(ConnectionContainer), new FrameworkPropertyMetadata(BoxValue.True));
            FocusVisualStyleProperty.OverrideMetadata(typeof(ConnectionContainer), new FrameworkPropertyMetadata(new Style()));

            KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(ConnectionContainer), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(ConnectionContainer), new FrameworkPropertyMetadata(KeyboardNavigationMode.Cycle));
        }

        public ConnectionContainer(ConnectionsMultiSelector selector)
        {
            Selector = selector;
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            if (VisualTreeHelper.GetParent(this) == null && IsKeyboardFocusWithin)
            {
                base.OnVisualParentChanged(oldParent);

                Selector.Editor?.Focus();
            }
            else
            {
                base.OnVisualParentChanged(oldParent);
            }
        }

        protected override void OnIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Connection is BaseConnection baseConnection)
            {
                baseConnection.UpdateFocusVisual();
            }
            else
            {
                Connection?.InvalidateVisual();
            }
        }

        /// <summary>
        /// Raises the <see cref="SelectedEvent"/> or <see cref="UnselectedEvent"/> based on <paramref name="newValue"/>.
        /// Called when the <see cref="IsSelected"/> value is changed.
        /// </summary>
        /// <param name="newValue">True if selected, false otherwise.</param>
        private void OnSelectedChanged(bool newValue)
        {
            BaseConnection.SetIsSelected(Connection, newValue);

            RaiseEvent(new RoutedEventArgs(newValue ? SelectedEvent : UnselectedEvent, this));
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            EditorGestures.ConnectionGestures gestures = EditorGestures.Mappings.Connection;
            if (IsSelectable && gestures.Selection.Select.Matches(e.Source, e))
            {
                _selectionType = gestures.Selection.GetSelectionType(e);
            }
            // Replaces the current selection when right-clicking on an element that has a context menu and is not selected.
            // Applies only when the select gesture is not right click.
            else if (e.ChangedButton == MouseButton.Right && Connection?.ContextMenu != null)
            {
                _selectionType = IsSelected ? SelectionType.Append : SelectionType.Replace;
            }

            if (_selectionType.HasValue)
            {
                Focus();
                e.Handled = true;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (_selectionType.HasValue)
            {
                // Determine whether the current selection should remain intact or be replaced by the clicked item. 
                // If the right mouse button is pressed on an already selected item, and the item either has an 
                // explicit context menu, the selection remains unchanged.
                // This ensures that the context menu applies to the entire selection rather than only the clicked item.
                bool allowContextMenu = e.ChangedButton == MouseButton.Right && IsSelected && Connection?.ContextMenu != null;
                if (!allowContextMenu)
                {
                    Select(_selectionType.Value);
                }

                _selectionType = null;
            }
        }

        /// <summary>
        /// Modifies the selection state of the current item based on the specified selection type.
        /// </summary>
        /// <param name="type">The type of selection to perform.</param>
        public void Select(SelectionType type)
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
                    Selector.Select(this);
                    break;
            }
        }
    }
}
