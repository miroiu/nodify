using Nodify.UndoRedo;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Nodify.Shapes.Canvas
{
    public abstract class ShapeViewModel : Undoable
    {
        public ShapeViewModel(IActionsHistory history) : base(history)
        {
            RecordProperty<ShapeViewModel>(x => x.Color);
            RecordProperty<ShapeViewModel>(x => x.Text);
        }

        public ShapeViewModel() : this(ActionsHistory.Global)
        {
        }

        public static readonly IReadOnlyList<Color> Colors = new Color[]
        {
            Color.FromRgb(207, 76, 44),
            Color.FromRgb(234, 156, 65),
            Color.FromRgb(235, 195, 71),
            Color.FromRgb(67, 141, 87),
            Color.FromRgb(63, 138, 226),
            Color.FromRgb(128, 61, 236),
        };

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private double _width;
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _height;
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private Color _color;
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value).Then(x => OnPropertyChanged(nameof(BorderColor)));
        }

        public Color BorderColor => Color * 1.5f;

        private string? _text;
        public string? Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ConnectorViewModel LeftConnector { get; } = new ConnectorViewModel(ConnectorPosition.Left);
        public ConnectorViewModel RightConnector { get; } = new ConnectorViewModel(ConnectorPosition.Right);
        public ConnectorViewModel TopConnector { get; } = new ConnectorViewModel(ConnectorPosition.Top);
        public ConnectorViewModel BottomConnector { get; } = new ConnectorViewModel(ConnectorPosition.Bottom);
    }
}
