using System.Windows;

namespace Nodify.Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            EditorGestures.Mappings.Editor.Cutting.Value = MultiGesture.None;
        }
    }
}
