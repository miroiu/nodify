using System.Windows;
using System.Windows.Input;
using Nodify.Compatibility;
using Nodify.Shared;

namespace Nodify
{
    [TemplatePart(Name = ElementTextBox, Type = typeof(TextBox))]
    internal class EditableTextBlock : WpfControl
    {
        private const string ElementTextBox = "PART_TextBox";

        public static readonly StyledProperty<bool> IsEditingProperty = AvaloniaProperty.Register<EditableTextBlock, bool>(nameof(IsEditing), false, defaultBindingMode: BindingMode.TwoWay, coerce: CoerceIsEditing);
        public static readonly StyledProperty<bool> IsEditableProperty = AvaloniaProperty.Register<EditableTextBlock, bool>(nameof(IsEditable), true);
        public static readonly StyledProperty<string?> TextProperty = TextBlock.TextProperty.AddOwner<EditableTextBlock>();
        public static readonly StyledProperty<bool> AcceptsReturnProperty = TextBox.AcceptsReturnProperty.AddOwner<EditableTextBlock>();
        public static readonly StyledProperty<TextWrapping> TextWrappingProperty = TextBlock.TextWrappingProperty.AddOwner<EditableTextBlock>();
        public static readonly StyledProperty<TextTrimming> TextTrimmingProperty = TextBlock.TextTrimmingProperty.AddOwner<EditableTextBlock>();
        public static readonly StyledProperty<int> MinLinesProperty = TextBox.MinLinesProperty.AddOwner<EditableTextBlock>();
        public static readonly StyledProperty<int> MaxLinesProperty = TextBox.MaxLinesProperty.AddOwner<EditableTextBlock>();
        public static readonly StyledProperty<int> MaxLengthProperty = TextBox.MaxLengthProperty.AddOwner<EditableTextBlock>();
        public static readonly StyledProperty<VerticalAlignment> VerticalContentAlignmentProperty = ContentControl.VerticalContentAlignmentProperty.AddOwner<EditableTextBlock>();
        public static readonly StyledProperty<HorizontalAlignment> HorizontalContentAlignmentProperty = ContentControl.HorizontalContentAlignmentProperty.AddOwner<EditableTextBlock>();

        private static void OnIsEditingChanged(AvaloniaObject d, AvaloniaPropertyChangedEventArgs e) { }

        private static bool CoerceIsEditing(AvaloniaObject d, bool value)
        {
            if (!((EditableTextBlock)d).IsEditable)
            {
                return false;
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
        
        public VerticalAlignment VerticalContentAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalContentAlignmentProperty);
            set => SetValue(VerticalContentAlignmentProperty, value);
        }
        
        public HorizontalAlignment HorizontalContentAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty);
            set => SetValue(HorizontalContentAlignmentProperty, value);
        }

        protected TextBox? TextBox { get; private set; }

        static EditableTextBlock()
        {
            TextWrappingProperty.OverrideDefaultValue<EditableTextBlock>(TextWrapping.Wrap);
            TextTrimmingProperty.OverrideDefaultValue<EditableTextBlock>(TextTrimming.CharacterEllipsis);
            IsEditingProperty.Changed.AddClassHandler<EditableTextBlock>(OnIsEditingChanged);
            TextProperty.OverrideMetadata<EditableTextBlock>(new StyledPropertyMetadata<string?>(defaultBindingMode: BindingMode.TwoWay));
            FocusableProperty.OverrideDefaultValue<EditableTextBlock>(true);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            TextBox = e.NameScope.Find<TextBox>(ElementTextBox);

            if (TextBox != null)
            {
                TextBox.LostFocus += OnLostFocus;
                TextBox.GetObservable(IsVisibleProperty)
                    .Subscribe(new AnonuymousObserver<bool>(OnTextBoxVisiblityChanged));

                if (IsEditing)
                {
                    TextBox.Focus();
                    TextBox.SelectAll();
                }
            }
        }

        private void OnTextBoxVisiblityChanged(bool e)
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

        private void OnLostFocus(object? sender, RoutedEventArgs e)
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
        private class AnonuymousObserver<T> : System.IObserver<T>
        {
            private readonly System.Action<T> _onNext;
            private readonly System.Action<System.Exception>? _onError;
            private readonly System.Action? _onCompleted;

            public AnonuymousObserver(System.Action<T> onNext, System.Action<System.Exception>? onError = null, System.Action? onCompleted = null)
            {
                _onNext = onNext;
                _onError = onError;
                _onCompleted = onCompleted;
            }

            public void OnCompleted() => _onCompleted?.Invoke();
            public void OnError(System.Exception error) => _onError?.Invoke(error);
            public void OnNext(T value) => _onNext?.Invoke(value);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);
            if (change.Property == IsEditingProperty)
                PseudoClasses.Set(":editing", (bool)change.NewValue);
        }
    }
}
