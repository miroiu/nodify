using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    /// <summary>
    /// The container for all the items generated from the <see cref="NodifyEditor.Decorators"/> collection.
    /// </summary>
    public class DecoratorContainer : ContentControl, INodifyCanvasItem
    {
        #region Dependency Properties

        public static readonly StyledProperty<Point> LocationProperty = ItemContainer.LocationProperty.AddOwner<DecoratorContainer>();
        public static readonly StyledProperty<Size> ActualSizeProperty = ItemContainer.ActualSizeProperty.AddOwner<DecoratorContainer>();

        /// <summary>
        /// Gets or sets the location of this <see cref="DecoratorContainer"/> inside the <see cref="NodifyEditor.DecoratorsHost"/>.
        /// </summary>
        public Point Location
        {
            get => (Point)GetValue(LocationProperty);
            set => SetValue(LocationProperty, value);
        }

        /// <summary>
        /// Gets the actual size of this <see cref="DecoratorContainer"/>.
        /// </summary>
        public Size ActualSize
        {
            get => (Size)GetValue(ActualSizeProperty);
            set => SetValue(ActualSizeProperty, value);
        }

        private static void OnLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = (DecoratorContainer)d;
            item.OnLocationChanged();
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent<RoutedEventArgs> LocationChangedEvent = RoutedEvent.Register<RoutedEventArgs>(nameof(LocationChanged), RoutingStrategies.Bubble, typeof(DecoratorContainer));

        /// <summary>
        /// Occurs when the <see cref="Location"/> of this <see cref="DecoratorContainer"/> is changed.
        /// </summary>
        public event RoutedEventHandler LocationChanged
        {
            add => AddHandler(LocationChangedEvent, value);
            remove => RemoveHandler(LocationChangedEvent, value);
        }

        /// <summary>
        /// Raises the <see cref="LocationChangedEvent"/>.
        /// </summary>
        protected void OnLocationChanged()
        {
            RaiseEvent(new RoutedEventArgs(LocationChangedEvent, this));
        }

        #endregion

        static DecoratorContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DecoratorContainer), new FrameworkPropertyMetadata(typeof(DecoratorContainer)));
            LocationProperty.OverrideMetadata<DecoratorContainer>(new StyledPropertyMetadata<Point>(default, BindingMode.TwoWay));
            PanelUtilities.AffectsParentArrange<DecoratorContainer>(LocationProperty);
            LocationProperty.Changed.AddClassHandler<DecoratorContainer>(OnLocationChanged);
        }

        protected override void OnSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnSizeChanged(sizeInfo);
            SetCurrentValue(ActualSizeProperty, sizeInfo.NewSize);
        }
    }
}
