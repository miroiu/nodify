using System.Windows;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            EventManager.RegisterClassHandler(typeof(NodifyEditor), MouseLeftButtonDownEvent, new MouseButtonEventHandler(CloseOperationsMenu));
            EventManager.RegisterClassHandler(typeof(NodifyEditor), MouseRightButtonUpEvent, new MouseButtonEventHandler(OpenOperationsMenu));
        }

        private void OpenOperationsMenu(object sender, MouseButtonEventArgs e)
        {
            if (!e.Handled && e.OriginalSource is NodifyEditor editor && !editor.IsPanning)
            {
                var calculator = editor.DataContext as CalculatorViewModel;
                if (calculator != null)
                {
                    e.Handled = true;
                    calculator.OperationsMenu.OpenAt(editor.MouseLocation);
                }
            }
        }

        private void CloseOperationsMenu(object sender, MouseButtonEventArgs e)
        {
            if (!e.Handled && sender is NodifyEditor editor)
            {
                var calculator = editor.DataContext as CalculatorViewModel;
                if (calculator != null)
                {
                    calculator.OperationsMenu.Close();
                }
            }
        }
    }
}
