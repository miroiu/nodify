using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public partial class EditorView : UserControl
    {
        public EditorView()
        {
            InitializeComponent();

            EventManager.RegisterClassHandler(typeof(NodifyEditor), MouseLeftButtonDownEvent, new MouseButtonEventHandler(CloseOperationsMenu));
            EventManager.RegisterClassHandler(typeof(NodifyEditor), MouseRightButtonUpEvent, new MouseButtonEventHandler(OpenOperationsMenu));
        }

        private void OpenOperationsMenu(object sender, MouseButtonEventArgs e)
        {
            if (!e.Handled && e.OriginalSource is NodifyEditor editor && !editor.IsPanning && editor.DataContext is CalculatorViewModel calculator)
            {
                e.Handled = true;
                calculator.OperationsMenu.OpenAt(editor.MouseLocation);
            }
        }

        private void CloseOperationsMenu(object sender, RoutedEventArgs e)
        {
            ItemContainer? itemContainer = sender as ItemContainer;
            NodifyEditor? editor = sender as NodifyEditor ?? itemContainer?.Editor;

            if (!e.Handled && editor?.DataContext is CalculatorViewModel calculator)
            {
                calculator.OperationsMenu.Close();
            }
        }

        private void OnDropNode(object sender, DragEventArgs e)
        {
            if(e.Source is NodifyEditor editor && editor.DataContext is CalculatorViewModel calculator
                && e.Data.GetData(typeof(OperationInfoViewModel)) is OperationInfoViewModel operation)
            {
                OperationViewModel op = OperationFactory.GetOperation(operation);
                op.Location = editor.GetLocationInsideEditor(e);
                calculator.Operations.Add(op);

                e.Handled = true;
            }
        }

        private void OnNodeDrag(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && ((FrameworkElement)sender).DataContext is OperationInfoViewModel operation)
            { 
                var data = new DataObject(typeof(OperationInfoViewModel), operation);
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
            }
        }
    }
}
