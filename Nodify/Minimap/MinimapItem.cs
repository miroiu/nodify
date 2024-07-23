using System.Windows;
using System.Windows.Controls;

namespace Nodify
{
    public class MinimapItem : ContentControl
    {
        public static readonly StyledProperty<Point> LocationProperty = ItemContainer.LocationProperty.AddOwner<MinimapItem>(new StyledPropertyMetadata<Point>(BoxValue.Point, BindingMode.TwoWay));

        /// <summary>
        /// Gets or sets the location of this <see cref="MinimapItem"/> inside the <see cref="Minimap"/>.
        /// </summary>
        public Point Location
        {
            get => (Point)GetValue(LocationProperty);
            set => SetValue(LocationProperty, value);
        }

        static MinimapItem()
        {
            PanelUtilities.AffectsParentArrange<DecoratorContainer>(LocationProperty);
        }
    }
}
