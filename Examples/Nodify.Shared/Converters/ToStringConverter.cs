using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace Nodify
{
    public class ToStringConverter : MarkupExtension, IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Point p)
            {
                return $"{p.X:0.0}, {p.Y:0.0}";
            }

            if (value is Size s)
            {
                return $"{s.Width:0.0}, {s.Height:0.0}";
            }

            if (value is double d)
            {
                return d.ToString("0.00");
            }

            if (value is Key key)
            {
                return FormatKey(key);
            }

            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        private static string FormatKey(Key key)
        {
            return key switch
            {
                Key.CapsLock => "Caps Lock",
                Key.PageUp => "Page Up",
                Key.PageDown => "Page Down",
                Key.PrintScreen => "Print Screen",
                Key.LWin => "Left Win",
                Key.RWin => "Right Win",
                Key.Apps => "Menu",
                Key.D0 => "0",
                Key.D1 => "1",
                Key.D2 => "2",
                Key.D3 => "3",
                Key.D4 => "4",
                Key.D5 => "5",
                Key.D6 => "6",
                Key.D7 => "7",
                Key.D8 => "8",
                Key.D9 => "9",
                Key.NumPad0 => "Num 0",
                Key.NumPad1 => "Num 1",
                Key.NumPad2 => "Num 2",
                Key.NumPad3 => "Num 3",
                Key.NumPad4 => "Num 4",
                Key.NumPad5 => "Num 5",
                Key.NumPad6 => "Num 6",
                Key.NumPad7 => "Num 7",
                Key.NumPad8 => "Num 8",
                Key.NumPad9 => "Num 9",
                Key.Multiply => "Num *",
                Key.Add => "Num +",
                Key.Separator => "Num Separator",
                Key.Subtract => "Num -",
                Key.Decimal => "Num .",
                Key.Divide => "Num /",
                Key.NumLock => "Num Lock",
                Key.Scroll => "Scroll Lock",
                Key.LeftShift => "Left Shift",
                Key.RightShift => "Right Shift",
                Key.LeftCtrl => "Left Ctrl",
                Key.RightCtrl => "Right Ctrl",
                Key.LeftAlt => "Left Alt",
                Key.RightAlt => "Right Alt",
                Key.OemSemicolon => ";",
                Key.OemPlus => "=",
                Key.OemComma => ",",
                Key.OemMinus => "-",
                Key.OemPeriod => ".",
                Key.OemQuestion => "/",
                Key.OemTilde => "`",
                Key.OemOpenBrackets => "[",
                Key.OemPipe => "\\",
                Key.OemCloseBrackets => "]",
                Key.OemQuotes => "'",
                Key.OemBackslash => "\\",
                Key.Play => "Play",
                Key.Zoom => "Zoom",
                _ => key.ToString()
            };
        }
    }
}
