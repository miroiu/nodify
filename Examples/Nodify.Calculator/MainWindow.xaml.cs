using Nodify.Interactivity;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            EditorGestures.Mappings.Editor.Cutting.Unbind();

            EventManager.RegisterClassHandler(
                    typeof(UIElement),
                    Keyboard.PreviewGotKeyboardFocusEvent,
                    (KeyboardFocusChangedEventHandler)OnPreviewGotKeyboardFocus);
        }

        private void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Title = e.NewFocus.ToString();
        }
    }
}
