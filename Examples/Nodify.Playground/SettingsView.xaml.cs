using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Nodify.Playground
{
    public partial class SettingsView : UserControl
    {
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(IEnumerable<ISettingViewModel>), typeof(SettingsView),
                new PropertyMetadata(null));

        public IEnumerable<ISettingViewModel> Items
        {
            get { return (IEnumerable<ISettingViewModel>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public SettingsView()
        {
            InitializeComponent();
        }
    }
}