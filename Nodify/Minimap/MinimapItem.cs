using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    public class MinimapItem : ContentControl
    {
        static MinimapItem()
        {
            FocusableProperty.OverrideMetadata(typeof(MinimapItem), new FrameworkPropertyMetadata(BoxValue.False));
        }

        public static readonly DependencyProperty LocationProperty = ItemContainer.LocationProperty.AddOwner(typeof(MinimapItem), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        /// <summary>
        /// Gets or sets the location of this <see cref="MinimapItem"/> inside the <see cref="Minimap"/>.
        /// </summary>
        public Point Location
        {
            get => (Point)GetValue(LocationProperty);
            set => SetValue(LocationProperty, value);
        }
    }
}
