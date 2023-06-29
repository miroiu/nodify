using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Nodify.Playground
{
    public partial class SettingsView : UserControl
    {
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(nameof(Items), typeof(IEnumerable<ISettingViewModel>), typeof(SettingsView));

        public IEnumerable<ISettingViewModel> Items
        {
            get => (IEnumerable<ISettingViewModel>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public SettingsView()
        {
            InitializeComponent();
        }
    }
}