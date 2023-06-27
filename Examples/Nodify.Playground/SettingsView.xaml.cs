using System.Windows;
using System.Windows.Controls;

namespace Nodify.Playground
{
    public partial class SettingsView : UserControl
    {
        public static readonly DependencyProperty SettingsItemsProperty =
            DependencyProperty.Register("SettingsItems", typeof(object), typeof(SettingsView),
                new PropertyMetadata(null));

        public object SettingsItems
        {
            get { return GetValue(SettingsItemsProperty); }
            set { SetValue(SettingsItemsProperty, value); }
        }

        public SettingsView()
        {
            InitializeComponent();
        }
    }
}