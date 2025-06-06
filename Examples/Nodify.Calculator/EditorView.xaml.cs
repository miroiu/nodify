using Nodify.Interactivity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public class OperationsMenuHandler : InputElementState<NodifyEditor>
    {
        private static InputGesture OpenGesture { get; } = new Interactivity.MouseGesture(MouseAction.RightClick);
        private static InputGesture CloseGesture { get; } = new Interactivity.MouseGesture(MouseAction.LeftClick);

        private OperationsMenuViewModel ViewModel => ((CalculatorViewModel)Element.DataContext).OperationsMenu;

        public OperationsMenuHandler(NodifyEditor element) : base(element)
        {
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (OpenGesture.Matches(e.Source, e))
            {
                ViewModel.OpenAt(Element.MouseLocation);
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (CloseGesture.Matches(e.Source, e))
            {
                ViewModel.Close();
            }
        }
    }

    public partial class EditorView : UserControl
    {
        public EditorView()
        {
            InitializeComponent();
        }

        static EditorView()
        {
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(editor => new OperationsMenuHandler(editor));

            // Ensure the selecting handler is executed after the OperationsMenuHandler, otherwise left click events will not be received.
            InputProcessor.Shared<NodifyEditor>.RemoveHandlerFactory<EditorState.Selecting>();
            InputProcessor.Shared<NodifyEditor>.RegisterHandlerFactory(editor => new EditorState.Selecting(editor));
        }

        private void OnDropNode(object sender, DragEventArgs e)
        {
            if (e.Source is NodifyEditor editor && editor.DataContext is CalculatorViewModel calculator
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
            if (e.LeftButton == MouseButtonState.Pressed && ((FrameworkElement)sender).DataContext is OperationInfoViewModel operation)
            {
                var data = new DataObject(typeof(OperationInfoViewModel), operation);
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
            }
        }
    }
}
