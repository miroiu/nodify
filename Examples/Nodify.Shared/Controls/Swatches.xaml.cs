using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Nodify.Shared;

namespace Nodify
{
    public partial class Swatches : TemplatedControl
    {
        public static readonly AvaloniaProperty<Color> SelectedColorProperty
            = AvaloniaProperty.Register<Swatches, Color>(nameof(SelectedColor), defaultBindingMode: BindingMode.TwoWay);

        public static readonly AvaloniaProperty<IEnumerable<Color>> ColorsProperty
            = AvaloniaProperty.Register<Swatches, IEnumerable<Color>>(nameof(Colors),  defaultValue: Array.Empty<Color>());

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
            FocusableProperty.OverrideDefaultValue<Swatches>(true);
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            var color = e.Source is Control fe && fe.DataContext is Color c ? c : (Color?)null;
            if (color.HasValue)
            {
                SelectedColor = color.Value;
            }

            e.Handled = true;
        }
    }
}
