using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Nodify.Playground
{
    public static class CompositionTargetEx
    {
        private static Stopwatch sw = new Stopwatch();
        private static event Action<double>? FrameUpdating;

        public static event Action<double> Rendering
        {
            add
            {
                if (FrameUpdating == null)
                {
                    sw.Start();
                }
                FrameUpdating += value;
            }
            remove
            {
                FrameUpdating -= value;
                if (FrameUpdating == null)
                {
                    sw.Stop();
                }
            }
        }

        public static void OnRendering(object? sender, EventArgs e)
        {
            var took = sw.Elapsed;
            sw.Restart();

            double fps = 1000 / took.TotalMilliseconds;
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
        }

        private void OnRendering(double fps)
        {
            Dispatcher.UIThread.Post(() =>
            {
                FPSText.Text = fps.ToString("###");
            });
        }

        private void BringIntoView_Click(object? sender, RoutedEventArgs e)
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

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            CompositionTargetEx.OnRendering(null, default!);
            Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Send);
        }
    }
}
