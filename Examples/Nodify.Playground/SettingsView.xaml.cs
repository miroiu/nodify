using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Nodify.Playground
{
    public partial class SettingsView : UserControl
    {
        public static readonly AvaloniaProperty<IEnumerable<ISettingViewModel>> ItemsProperty =
            AvaloniaProperty.Register<SettingsView, IEnumerable<ISettingViewModel>>(nameof(Items));

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