using FluentIcons.Common;
using System.Windows;
using System.Windows.Controls;

namespace Nodify.Workflow.Shell
{
    /// <summary>
    /// Interaction logic for KeybindingsCategory.xaml
    /// </summary>
    public partial class KeybindingsCategory : UserControl
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(Icon), typeof(KeybindingsCategory), new PropertyMetadata(Icon.Warning));
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label), typeof(string), typeof(KeybindingsCategory), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof(Description), typeof(string), typeof(KeybindingsCategory), new PropertyMetadata(string.Empty));

        public Icon Icon
        {
            get => (Icon)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public KeybindingsCategory()
        {
            InitializeComponent();
        }
    }
}
