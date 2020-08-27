using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Nodify
{
    public delegate void PreviewLocationChangedDelegate(Point newLocation);

    public class ItemContainer : ContentControl
    {
        #region Attached Properties

        public static readonly DependencyProperty LocationOverrideProperty = DependencyProperty.RegisterAttached("LocationOverride", typeof(Point), typeof(ItemContainer), new FrameworkPropertyMetadata(BoxValue.Point, OnLocationOverrideChanged));

        public static Point GetLocationOverride(UIElement elem)
        {
            return (Point)elem.GetValue(LocationOverrideProperty);
        }

        public static void SetLocationOverride(UIElement elem, Point value)
        {
            elem.SetValue(LocationOverrideProperty, value);
        }

        // TODO: Check if this causes memory leak
        private static void OnLocationOverrideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement elem)
            {
                elem.Loaded += OnLocationOverridenElementLoaded;
                //elem.Unloaded += OnLocationOverrideElementUnloaded;
            }
        }

        //private static void OnLocationOverrideElementUnloaded(object sender, RoutedEventArgs e)
        //{
        //    var elem = (FrameworkElement)sender;

        //    elem.Loaded -= OnLocationOverridenElementLoaded;
        //    elem.Unloaded -= OnLocationOverrideElementUnloaded;
        //}

        private static void OnLocationOverridenElementLoaded(object sender, RoutedEventArgs e)
        {
            var elem = (FrameworkElement)sender;

            var container = elem.GetParentOfType<ItemContainer>();
            if (container != null)
            {
                container.Location = GetLocationOverride(elem);
            }
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.Register(nameof(SelectedBrush), typeof(Brush), typeof(ItemContainer));
        public static readonly DependencyProperty IsSelectableProperty = DependencyProperty.Register(nameof(IsSelectable), typeof(bool), typeof(ItemContainer), new FrameworkPropertyMetadata(BoxValue.True));
        public static readonly DependencyProperty IsSelectedProperty = Selector.IsSelectedProperty.AddOwner(typeof(ItemContainer), new FrameworkPropertyMetadata(BoxValue.False, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsSelectedChanged));
        public static readonly DependencyProperty LocationProperty = DependencyProperty.Register(nameof(Location), typeof(Point), typeof(ItemContainer), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnLocationChanged));
        public static readonly DependencyProperty DesiredSizeForSelectionProperty = DependencyProperty.Register(nameof(DesiredSizeForSelection), typeof(Size?), typeof(ItemContainer), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.NotDataBindable));
        public static readonly DependencyPropertyKey IsPreviewingLocationPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsPreviewingLocation), typeof(bool), typeof(ItemContainer), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty IsPreviewingLocationProperty = IsPreviewingLocationPropertyKey.DependencyProperty;

        public Brush SelectedBrush
        {
            get => (Brush)GetValue(SelectedBrushProperty);
            set => SetValue(SelectedBrushProperty, value);
        }

        public Point Location
        {
            get => (Point)GetValue(LocationProperty);
            set => SetValue(LocationProperty, value);
        }

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public bool IsSelectable
        {
            get => (bool)GetValue(IsSelectableProperty);
            set => SetValue(IsSelectableProperty, value);
        }

        public bool IsPreviewingLocation
        {
            get => (bool)GetValue(IsPreviewingLocationProperty);
            private set => SetValue(IsPreviewingLocationPropertyKey, value);
        }

        public Size? DesiredSizeForSelection
        {
            get => (Size?)GetValue(DesiredSizeForSelectionProperty);
            set => SetValue(DesiredSizeForSelectionProperty, value);
        }

        private static void OnLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = (ItemContainer)d;
            item.OnLocationChanged();

            var host = item.ParentHost;

            if (!host?.IsBulkUpdatingItems ?? true)
            {
                host?.ItemsHost?.InvalidateArrange();
            }
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent SelectedEvent = Selector.SelectedEvent.AddOwner(typeof(ItemContainer));
        public static readonly RoutedEvent UnselectedEvent = Selector.UnselectedEvent.AddOwner(typeof(ItemContainer));
        public static readonly RoutedEvent LocationChangedEvent = EventManager.RegisterRoutedEvent(nameof(LocationChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ItemContainer));

        public event RoutedEventHandler LocationChanged
        {
            add => AddHandler(LocationChangedEvent, value);
            remove => RemoveHandler(LocationChangedEvent, value);
        }

        public event DragStartedEventHandler DragStarted
        {
            add => AddHandler(DragBehavior.DragStartedEvent, value);
            remove => RemoveHandler(DragBehavior.DragStartedEvent, value);
        }

        public event DragCompletedEventHandler DragCompleted
        {
            add => AddHandler(DragBehavior.DragCompletedEvent, value);
            remove => RemoveHandler(DragBehavior.DragCompletedEvent, value);
        }

        public event RoutedEventHandler Selected
        {
            add => AddHandler(SelectedEvent, value);
            remove => RemoveHandler(SelectedEvent, value);
        }

        public event RoutedEventHandler Unselected
        {
            add => AddHandler(UnselectedEvent, value);
            remove => RemoveHandler(UnselectedEvent, value);
        }

        protected void OnLocationChanged()
        {
            IsPreviewingLocation = false;
            RaiseEvent(new RoutedEventArgs(LocationChangedEvent, this));
        }

        protected virtual void OnSelectedChanged(bool newValue)
            => RaiseEvent(new RoutedEventArgs(newValue ? SelectedEvent : UnselectedEvent, this));

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elem = (ItemContainer)d;

            var result = elem.IsSelectable && (bool)e.NewValue;
            elem.OnSelectedChanged(result);
            elem.IsSelected = result;
        }

        #endregion

        public event PreviewLocationChangedDelegate? PreviewLocationChanged;

        protected internal void OnPreviewLocationChanged(Point newLocation)
        {
            IsPreviewingLocation = newLocation != Location;
            PreviewLocationChanged?.Invoke(newLocation);
        }

        protected NodifyEditor? ParentHost => ItemsControl.ItemsControlFromItemContainer(this) as NodifyEditor;

        static ItemContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemContainer), new FrameworkPropertyMetadata(typeof(ItemContainer)));
            FocusableProperty.OverrideMetadata(typeof(ItemContainer), new FrameworkPropertyMetadata(BoxValue.True));
        }

        public bool IsInsideContainer(Point clickPosition)
        {
            Size size = DesiredSizeForSelection ?? RenderSize;
            return clickPosition.X <= size.Width && clickPosition.Y <= size.Height;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (IsInsideContainer(e.GetPosition(this)))
            {
                e.Handled = true;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsInsideContainer(e.GetPosition(this)))
            {
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    IsSelected = !IsSelected;
                }
                else if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    IsSelected = true;
                }
                else
                {
                    ParentHost?.UnselectAll();
                    IsSelected = true;
                }

                e.Handled = true;
            }
        }
    }
}
