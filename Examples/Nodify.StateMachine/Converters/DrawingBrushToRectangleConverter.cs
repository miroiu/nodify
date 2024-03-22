using System;
using System.Globalization;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Nodify.StateMachine;

public class DrawingBrushToRectangleConverter : IValueConverter
{
    public static DrawingBrushToRectangleConverter Instance { get; } = new DrawingBrushToRectangleConverter();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is IBrush brush)
        {
            return new Rectangle()
            {
                Fill = brush,
                Width = 16,
                Height = 16
            };
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}