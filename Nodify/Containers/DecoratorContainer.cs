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

        public static readonly DependencyProperty LocationProperty = ItemContainer.LocationProperty.AddOwner(typeof(DecoratorContainer), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsParentArrange, OnLocationChanged));
        public static readonly DependencyProperty ActualSizeProperty = ItemContainer.ActualSizeProperty.AddOwner(typeof(DecoratorContainer));

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

        public static readonly RoutedEvent LocationChangedEvent = EventManager.RegisterRoutedEvent(nameof(LocationChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DecoratorContainer));

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
        }

        /// <inheritdoc />
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            ActualSize = sizeInfo.NewSize;
        }
    }
}
