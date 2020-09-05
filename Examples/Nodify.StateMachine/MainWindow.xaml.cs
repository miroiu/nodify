using System.Windows;
using System.Windows.Input;

namespace Nodify.StateMachine
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            EventManager.RegisterClassHandler(typeof(BaseConnection), MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnConnectionInteraction));
        }

        private void OnConnectionInteraction(object sender, MouseButtonEventArgs e)
        {
            if (sender is BaseConnection ctrl && ctrl.DataContext is TransitionViewModel transition)
            {
                if (Keyboard.Modifiers == ModifierKeys.Alt)
                {
                    transition.Source.Graph.Transitions.Remove(transition);
                }
            }
        }
    }
}
