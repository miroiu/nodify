using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Nodify.Interactivity;

namespace Nodify.StateMachine
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ConnectorState.EnableToggledConnectingMode = true;
            NodifyEditor.EnableCuttingLinePreview = true;

            EditorGestures.Mappings.Connection.Disconnect.Unbind();
            EditorGestures.Mappings.Editor.ZoomModifierKey = ModifierKeys.Control;
            EditorGestures.Mappings.Editor.PanWithMouseWheel = true;

            EventManager.RegisterClassHandler(
                    typeof(UIElement),
                    Keyboard.PreviewGotKeyboardFocusEvent,
                    (KeyboardFocusChangedEventHandler)OnPreviewGotKeyboardFocus);
        }

        private void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Title = e.NewFocus.ToString();
        }

        private void ScrollViewer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers != ModifierKeys.Shift)
                return;

            var scrollViewer = (ScrollViewer)sender;

            if (e.Key == Key.PageUp)
            {
                scrollViewer.PageLeft();
                e.Handled = true;
            }
            else if (e.Key == Key.PageDown)
            {
                scrollViewer.PageRight();
                e.Handled = true;
            }
        }
    }
}
