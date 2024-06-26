using System.ComponentModel;
using System.Windows;

namespace Nodify.Shapes.Canvas
{
    public class ShapeToolbarViewModel : ObservableObject, ICanvasDecorator
    {
        private ShapeViewModel? _shape;
        public ShapeViewModel? Shape
        {
            get => _shape;
            set
            {
                var prevShape = _shape;
                if (SetProperty(ref _shape, value))
                {
                    _hiddenShape = null;
                    HookLocationEvents(prevShape, value);
                }
            }
        }

        private ShapeViewModel? _hiddenShape;

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private void HookLocationEvents(ShapeViewModel? prevShape, ShapeViewModel? newShape)
        {
            if (prevShape != null)
                prevShape.PropertyChanged -= OnLocationChanged;

            if (newShape != null)
            {
                newShape.PropertyChanged += OnLocationChanged;
                Location = newShape.Location;
            }
        }

        private void OnLocationChanged(object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(ShapeViewModel.Location))
                Location = ((ShapeViewModel)sender!).Location;
        }

        public void Hide()
        {
            _hiddenShape = _shape;
            _shape = null;

            OnPropertyChanged(nameof(Shape));
        }

        public void Show()
        {
            if (_hiddenShape != null)
            {
                _shape = _hiddenShape;
                _hiddenShape = null;

                OnPropertyChanged(nameof(Shape));
            }
        }
    }
}
