using System.Windows;

namespace Nodify.Playground
{
    public class Viewport : ObservableObject
    {
        private Point _offset;
        public Point Offset
        {
            get => _offset;
            set => SetProperty(ref _offset, value);
        }

        private double _scale = 1;
        public double Scale
        {
            get => _scale;
            set => SetProperty(ref _scale, value);
        }

        private int _gridSpacing = 15;
        public int GridSpacing
        {
            get => _gridSpacing;
            set => SetProperty(ref _gridSpacing, value);
        }

        private bool _realtimeSelection = true;
        public bool RealtimeSelection
        {
            get => _realtimeSelection;
            set => SetProperty(ref _realtimeSelection, value);
        }
    }
}
