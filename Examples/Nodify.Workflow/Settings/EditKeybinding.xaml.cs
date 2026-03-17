using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.Workflow.Settings
{
    /// <summary>
    /// Interaction logic for EditKeybinding.xaml
    /// </summary>
    public partial class EditKeybinding : UserControl
    {
        private bool _isEditing;
        private bool _isRecording;

        public EditKeybinding()
        {
            InitializeComponent();

            LostFocus += EditKeybinding_LostFocus;
        }

        private void EditKeybinding_LostFocus(object sender, RoutedEventArgs e)
        {
            StopEditing();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleEditing();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_isEditing)
            {
                var dataContext = (KeyGestureSelectorViewModel)DataContext;

                if (!_isRecording)
                {
                    dataContext.Modifier.Value = ModifierKeys.None;
                    dataContext.Key.Value = Key.None;
                    _isRecording = true;
                }

                var key = (e.Key == Key.System) ? e.SystemKey : e.Key;

                dataContext.Modifier.Value = Keyboard.Modifiers;

                if (!IsModifierKey(key))
                {
                    dataContext.Key.Value = key;
                }

                e.Handled = true;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _isRecording = false;
        }

        private void ToggleEditing()
        {
            if (_isEditing)
            {
                StopEditing();
            }
            else
            {
                StartEditing();
            }
        }

        private void StartEditing()
        {
            EditButton.CaptureMouse();
            EditIcon.Icon = FluentIcons.Common.Icon.PenDismiss;
            EditButton.SetResourceReference(BorderBrushProperty, "AccentFillColorDefaultBrush");
            _isEditing = true;

            Focus();
        }

        private void StopEditing()
        {
            EditButton.ReleaseMouseCapture();
            EditIcon.Icon = FluentIcons.Common.Icon.Edit;
            EditButton.BorderBrush = null;
            _isEditing = false;

            if (_isRecording)
            {
                var dataContext = (KeyGestureSelectorViewModel)DataContext;

                dataContext.Modifier.Value = ModifierKeys.None;
                dataContext.Key.Value = Key.None;
                _isRecording = false;
            }
        }

        private static bool IsModifierKey(Key key)
        {
            return key == Key.LeftCtrl || key == Key.RightCtrl ||
                   key == Key.LeftShift || key == Key.RightShift ||
                   key == Key.LeftAlt || key == Key.RightAlt ||
                   key == Key.LWin || key == Key.RWin;
        }

    }
}
