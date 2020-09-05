using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.StateMachine
{
    [TemplatePart(Name = ElementTextBox, Type = typeof(TextBox))]
    public class EditableTextBlock : Control
    {
        private const string ElementTextBox = "PART_TextBox";

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(EditableTextBlock), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty IsEditingProperty = DependencyProperty.Register(nameof(IsEditing), typeof(bool), typeof(EditableTextBlock), new FrameworkPropertyMetadata(BoxValue.False, OnIsEditingChanged, CoerceIsEditing));
        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(EditableTextBlock), new FrameworkPropertyMetadata(BoxValue.True));
        public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register(nameof(AcceptsReturn), typeof(bool), typeof(EditableTextBlock), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(nameof(TextWrapping), typeof(TextWrapping), typeof(EditableTextBlock), new FrameworkPropertyMetadata(TextWrapping.Wrap));

        private static void OnIsEditingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        private static object CoerceIsEditing(DependencyObject d, object value)
        {
            if (!((EditableTextBlock)d).IsEditable)
            {
                return BoxValue.False;
            }

            return value;
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsEditing
        {
            get => (bool)GetValue(IsEditingProperty);
            set => SetValue(IsEditingProperty, value);
        }

        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
        }

        public bool AcceptsReturn
        {
            get => (bool)GetValue(AcceptsReturnProperty);
            set => SetValue(AcceptsReturnProperty, value);
        }

        public TextWrapping TextWrapping
        {
            get => (TextWrapping)GetValue(TextWrappingProperty);
            set => SetValue(TextWrappingProperty, value);
        }

        protected TextBox? TextBox { get; private set; }

        static EditableTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableTextBlock), new FrameworkPropertyMetadata(typeof(EditableTextBlock)));
            FocusableProperty.OverrideMetadata(typeof(EditableTextBlock), new FrameworkPropertyMetadata(BoxValue.True));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            TextBox = GetTemplateChild(ElementTextBox) as TextBox;

            if (TextBox != null)
            {
                TextBox.LostFocus += OnLostFocus;
                TextBox.LostKeyboardFocus += OnLostFocus;
                TextBox.IsVisibleChanged += OnTextBoxVisiblityChanged;

                if (IsEditing)
                {
                    TextBox.Focus();
                    TextBox.SelectAll();
                }
            }
        }

        private void OnTextBoxVisiblityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsEditing && TextBox != null)
            {
                if (TextBox.Focus())
                {
                    TextBox.SelectAll();
                }
                else
                {
                    IsEditing = false;
                }
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (IsEditing)
            {
                e.Handled = true;
            }
            else if (IsEditable && e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                IsEditing = true;
                e.Handled = true;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (IsEditing)
            {
                e.Handled = true;
            }
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            IsEditing = false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (IsEditing && e.Key == Key.Escape)
            {
                IsEditing = false;
            }
        }
    }
}
