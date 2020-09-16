using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Nodify.Shared;

namespace Nodify
{
    [TemplatePart(Name = ElementTextBox, Type = typeof(TextBox))]
    public class EditableTextBlock : Control
    {
        private const string ElementTextBox = "PART_TextBox";

        public static readonly DependencyProperty IsEditingProperty = DependencyProperty.Register(nameof(IsEditing), typeof(bool), typeof(EditableTextBlock), new FrameworkPropertyMetadata(BoxValue.False, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsEditingChanged, CoerceIsEditing));
        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(EditableTextBlock), new FrameworkPropertyMetadata(BoxValue.True));
        public static readonly DependencyProperty TextProperty = TextBlock.TextProperty.AddOwner(typeof(EditableTextBlock), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty AcceptsReturnProperty = TextBoxBase.AcceptsReturnProperty.AddOwner(typeof(EditableTextBlock), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty TextWrappingProperty = TextBlock.TextWrappingProperty.AddOwner(typeof(EditableTextBlock), new FrameworkPropertyMetadata(TextWrapping.Wrap));
        public static readonly DependencyProperty TextTrimmingProperty = TextBlock.TextTrimmingProperty.AddOwner(typeof(EditableTextBlock), new FrameworkPropertyMetadata(TextTrimming.CharacterEllipsis));
        public static readonly DependencyProperty MinLinesProperty = TextBox.MinLinesProperty.AddOwner(typeof(EditableTextBlock));
        public static readonly DependencyProperty MaxLinesProperty = TextBox.MaxLinesProperty.AddOwner(typeof(EditableTextBlock));
        public static readonly DependencyProperty MaxLengthProperty = TextBox.MaxLengthProperty.AddOwner(typeof(EditableTextBlock));

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

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public int MinLines
        {
            get => (int)GetValue(MinLinesProperty);
            set => SetValue(MaxLinesProperty, value);
        }

        public int MaxLines
        {
            get => (int)GetValue(MaxLinesProperty);
            set => SetValue(MaxLinesProperty, value);
        }

        public TextWrapping TextWrapping
        {
            get => (TextWrapping)GetValue(TextWrappingProperty);
            set => SetValue(TextWrappingProperty, value);
        }

        public TextTrimming TextTrimming
        {
            get => (TextTrimming)GetValue(TextTrimmingProperty);
            set => SetValue(TextTrimmingProperty, value);
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
            if (IsEditing && e.Key == Key.Escape || !AcceptsReturn && e.Key == Key.Enter)
            {
                IsEditing = false;
            }

            if(e.Key == Key.Enter && IsFocused && !IsEditing)
            {
                IsEditing = true;
            }
        }
    }
}
