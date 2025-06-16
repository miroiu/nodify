using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.Calculator
{
    public partial class OperationsMenuView : UserControl
    {
        private readonly WeakReference<UIElement?> _focusToRestore = new WeakReference<UIElement?>(null!);

        public OperationsMenuView()
        {
            InitializeComponent();

            IsVisibleChanged += OperationsMenuView_IsVisibleChanged;
        }

        private void OperationsMenuView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsLoaded)
            {
                return;
            }

            if (e.NewValue is true)
            {
                _focusToRestore.SetTarget(Keyboard.FocusedElement as UIElement);
                Dispatcher.BeginInvoke(new Action(() => OperationsList.Focus()), System.Windows.Threading.DispatcherPriority.Input);
            }
            else if (e.NewValue is false)
            {
                if (_focusToRestore.TryGetTarget(out var elementToFocus))
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        elementToFocus!.Focus();
                    }), System.Windows.Threading.DispatcherPriority.Input);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                SetCurrentValue(VisibilityProperty, Visibility.Collapsed);
            }
        }
    }
}
