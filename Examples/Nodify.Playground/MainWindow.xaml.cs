using System;
using System.Windows;

namespace Nodify.Playground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random _rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
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
    }
}
