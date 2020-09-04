using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.Playground
{
    public partial class NodifyEditorView : UserControl
    {
        public NodifyEditorView()
        {
            InitializeComponent();

            EventManager.RegisterClassHandler(typeof(BaseConnection), MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnConnectionInteraction));
        }

        private void OnConnectionInteraction(object sender, MouseButtonEventArgs e)
        {
            if (sender is BaseConnection ctrl && ctrl.DataContext is ConnectionViewModel connection)
            {
                if (Keyboard.Modifiers == ModifierKeys.Alt)
                {
                    connection.Remove();
                }
                else if (e.ClickCount > 1)
                {
                    connection.Split(e.GetPosition(ctrl) - new Vector(30, 15));
                }
            }
        }
    }
}
