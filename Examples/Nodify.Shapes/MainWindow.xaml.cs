using System.Windows;
using System.Windows.Input;

namespace Nodify.Shapes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PendingConnection.HotKeysDisplayMode = HotKeysDisplayMode.All;

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