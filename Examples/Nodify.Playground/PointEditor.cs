using System.Windows;

namespace Nodify.Playground
{
    public class PointEditor : ObservableObject
    {
        public double X
        {
            get => Value.X;
            set
            {
                Value = new Point(value, Value.Y);
                if (value >= 0)
                {
                    Size = new Size(value, Size.Height);
                }
            }
        }

        public double Y
        {
            get => Value.Y;
            set
            {
                Value = new Point(Value.X, value);
                if (value >= 0)
                {
                    Size = new Size(Size.Width, value);
                }
            }
        }

        private Point _value;
        public Point Value
        {
            get => _value;
            set => SetProperty(ref _value, value)
                .Then(() =>
                {
                    OnPropertyChanged(nameof(X));
                    OnPropertyChanged(nameof(Y));
                });
        }

        private Size _size;
        public Size Size
        {
            get => _size;
            set => SetProperty(ref _size, value)
                .Then(() =>
                {
                    OnPropertyChanged(nameof(X));
                    OnPropertyChanged(nameof(Y));
                });
        }

        public string XLabel { get; set; } = "x";
        public string YLabel { get; set; } = "y";

        public static implicit operator PointEditor(Point point)
        {
            return new PointEditor
            {
                X = point.X,
                Y = point.Y
            };
        }

        public static implicit operator PointEditor(Size size)
        {
            return new PointEditor
            {
                X = size.Width,
                Y = size.Height,
                XLabel = "w",
                YLabel = "h"
            };
        }
    }
}
