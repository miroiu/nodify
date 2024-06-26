using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Nodify.Shared;

namespace Nodify
{
    public partial class Swatches : Control
    {
        public static readonly DependencyProperty SelectedColorProperty
            = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(Swatches), new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ColorsProperty
            = DependencyProperty.Register(nameof(Colors), typeof(IEnumerable<Color>), typeof(Swatches), new PropertyMetadata(Array.Empty<Color>()));

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public IEnumerable<Color> Colors
        {
            get => (IEnumerable<Color>)GetValue(ColorsProperty);
            set => SetValue(ColorsProperty, value);
        }

        static Swatches()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Swatches), new FrameworkPropertyMetadata(typeof(Swatches)));
            FocusableProperty.OverrideMetadata(typeof(Swatches), new FrameworkPropertyMetadata(BoxValue.True));
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var color = e.OriginalSource is FrameworkElement fe && fe.DataContext is Color c ? c : (Color?)null;
            if (color.HasValue)
            {
                SelectedColor = color.Value;
            }

            e.Handled = true;
        }
    }
}
