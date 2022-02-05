using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify
{
    public class TabItemEx : TabItem
    {
        public static readonly DependencyProperty CloseTabCommandProperty = DependencyProperty.Register(nameof(CloseTabCommand), typeof(ICommand), typeof(TabItemEx), new PropertyMetadata(null));
        public static readonly DependencyProperty CloseTabCommandParameterProperty = DependencyProperty.Register(nameof(CloseTabCommandParameter), typeof(object), typeof(TabItemEx), new PropertyMetadata(null));
        
        public ICommand CloseTabCommand
        {
            get { return (ICommand)GetValue(CloseTabCommandProperty); }
            set { SetValue(CloseTabCommandProperty, value); }
        }

        public object CloseTabCommandParameter
        {
            get { return (object)GetValue(CloseTabCommandParameterProperty); }
            set { SetValue(CloseTabCommandParameterProperty, value); }
        }

        static TabItemEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItemEx), new FrameworkPropertyMetadata(typeof(TabItemEx)));
        }
    }
}
