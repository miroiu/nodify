using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Nodify.Playground
{
    public static class CompositionTargetEx
    {
        private static TimeSpan _last = TimeSpan.Zero;
        private static event Action<double>? FrameUpdating;

        public static event Action<double> Rendering
        {
            add
            {
                if (FrameUpdating == null)
                {
                    CompositionTarget.Rendering += OnRendering;
                }
                FrameUpdating += value;
            }
            remove
            {
                FrameUpdating -= value;
                if (FrameUpdating == null)
                {
                    CompositionTarget.Rendering -= OnRendering;
                }
            }
        }

        private static void OnRendering(object? sender, EventArgs e)
        {
            RenderingEventArgs args = (RenderingEventArgs)e;
            var renderingTime = args.RenderingTime;
            if (renderingTime == _last)
                return;

            double fps = 1000 / (renderingTime - _last).TotalMilliseconds;
            _last = renderingTime;
            FrameUpdating?.Invoke(fps);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random _rand = new Random();
        private bool _connectionAnimationsPlaying;

        public static string AnimateDirectionalArrowsStoryboardKey = "AnimateDirectionalArrows";

        public static DependencyProperty DirectionalArrowsOffsetProperty = BaseConnection.DirectionalArrowsOffsetProperty.AddOwner(typeof(MainWindow), new FrameworkPropertyMetadata(0d, DirectionalArrowsOffsetChanged));

        private static void DirectionalArrowsOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EditorSettings.Instance.DirectionalArrowsOffset = (double)e.NewValue;
        }

        public double DirectionalArrowsOffset
        {
            get => (double)GetValue(DirectionalArrowsOffsetProperty);
            set => SetValue(DirectionalArrowsOffsetProperty, value);
        }

        public MainWindow()
        {
            InitializeComponent();

            CompositionTargetEx.Rendering += OnRendering;
        }

        private void OnRendering(double fps)
        {
            FPSText.Text = fps.ToString("0");
        }

        private void BringIntoView_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PlaygroundViewModel model)
            {
                NodifyObservableCollection<NodeViewModel> nodes = model.GraphViewModel.Nodes;
                int index = _rand.Next(nodes.Count);

                if (nodes.Count > index)
                {
                    NodeViewModel node = nodes[index];
                    EditorCommands.BringIntoView.Execute(node.Location, EditorView.Editor);
                }
            }
        }

        private void AnimateConnections_Click(object sender, RoutedEventArgs e)
        {
            var storyboard = (Storyboard)FindResource(AnimateDirectionalArrowsStoryboardKey);

            if (_connectionAnimationsPlaying)
            {
                storyboard.Stop();
            }
            else
            {
                storyboard.Begin();
            }

            _connectionAnimationsPlaying = !_connectionAnimationsPlaying;
        }
    }
}
