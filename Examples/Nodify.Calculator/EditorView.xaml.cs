using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public partial class EditorView : UserControl
    {
        // MP! Fix: Context menu handling.
        private Size _calcSize;
        private Point _openPressPosition;
        private Point _closePressPosition;

        public EditorView()
        {
            InitializeComponent();

            PointerPressedEvent.AddClassHandler<NodifyEditor>(CloseOperationsMenuPointerPressed, RoutingStrategies.Tunnel);
            ItemContainer.DragStartedEvent.AddClassHandler<ItemContainer>(CloseOperationsMenu);
            PointerReleasedEvent.AddClassHandler<NodifyEditor>(OpenOperationsMenu);
            Editor.AddHandler(DragDrop.DropEvent, OnDropNode);
        }

        private void OpenOperationsMenu(object? sender, PointerReleasedEventArgs e)
        {
            if (!e.Handled && e.Source is NodifyEditor editor && !editor.IsPanning && editor.DataContext is CalculatorViewModel calculator &&
                e.InitialPressMouseButton == MouseButton.Right)
            {
                e.Handled = true;

                // MP! Fix: Context menu handling.
                _openPressPosition = e.GetPosition(this);

                calculator.OperationsMenu.OpenAt(editor.MouseLocation);
            }
        }

        private void CloseOperationsMenuPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
            {
                _closePressPosition = e.GetPosition(this);
                CloseOperationsMenu(sender, e);
            }
        }

        private void CloseOperationsMenu(object? sender, RoutedEventArgs e)
        {
            ItemContainer? itemContainer = sender as ItemContainer;
            NodifyEditor? editor = sender as NodifyEditor ?? itemContainer?.Editor;

            if (!e.Handled && editor?.DataContext is CalculatorViewModel calculator)
            {
                _calcSize = _calcSize == default ? calculator.OperationsMenu.Bounds.Size : _calcSize;
                var calcRectangle = new Rect(_openPressPosition, _calcSize);

                //MP! Fixed: Only call Close() if lower layer didn't do it, which only occurs when we click outside the popup menu.
                if (!calcRectangle.Contains(_closePressPosition))
                {
                    calculator.OperationsMenu.Close();
                }
            }
        }

        private void OnDropNode(object? sender, DragEventArgs e)
        {
            NodifyEditor? editor = (e.Source as NodifyEditor) ?? (e.Source as Control)?.GetLogicalParent() as NodifyEditor;
            if (editor != null && editor.DataContext is CalculatorViewModel calculator
                && e.Data.Get(typeof(OperationInfoViewModel).FullName) is OperationInfoViewModel operation)
            {
                OperationViewModel op = OperationFactory.GetOperation(operation);
                op.Location = editor.GetLocationInsideEditor(e);
                calculator.Operations.Add(op);

                e.Handled = true;
            }
        }

        private void OnNodeDrag(object? sender, MouseEventArgs e)
        {
            if (leftButtonPressed && ((Control)sender).DataContext is OperationInfoViewModel operation)
            {
                var data = new DataObject();
                data.Set(typeof(OperationInfoViewModel).FullName, operation);
                DragDrop.DoDragDrop(e, data, DragDropEffects.Copy);
            }
        }

        private void OnNodePressed(object? sender, PointerPressedEventArgs e)
        {
            leftButtonPressed = e.GetCurrentPoint(this).Properties.PointerUpdateKind ==
                                PointerUpdateKind.LeftButtonPressed;
        }

        private void OnNodeExited(object? sender, PointerEventArgs e)
        {
            leftButtonPressed = false;
        }

        private bool leftButtonPressed;
    }
}
