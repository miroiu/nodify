using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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

        public MainWindow()
        {
            InitializeComponent();

            CompositionTargetEx.Rendering += OnRendering;

            EventManager.RegisterClassHandler(
                    typeof(UIElement),
                    Keyboard.PreviewGotKeyboardFocusEvent,
                    (KeyboardFocusChangedEventHandler)OnPreviewGotKeyboardFocus);
        }

        private void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Title = e.NewFocus.ToString();
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
            EditorSettings.Instance.IsAnimatingConnections = !EditorSettings.Instance.IsAnimatingConnections;
        }
    }
}
