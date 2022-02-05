using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify
{
    [TemplatePart(Name = ElementScrollViewer, Type = typeof(ScrollViewer))]
    public class TabControlEx : TabControl
    {
        private const string ElementScrollViewer = "PART_ScrollViewer";

        public static readonly DependencyProperty AddTabCommandProperty = DependencyProperty.Register(nameof(AddTabCommand), typeof(ICommand), typeof(TabControlEx), new PropertyMetadata(null));
        public static readonly DependencyProperty AutoScrollToEndProperty = DependencyProperty.Register(nameof(AutoScrollToEnd), typeof(bool), typeof(TabControlEx), new PropertyMetadata(false));

        public ICommand AddTabCommand
        {
            get { return (ICommand)GetValue(AddTabCommandProperty); }
            set { SetValue(AddTabCommandProperty, value); }
        }
        public bool AutoScrollToEnd
        {
            get { return (bool)GetValue(AutoScrollToEndProperty); }
            set { SetValue(AutoScrollToEndProperty, value); }
        }

        protected ScrollViewer? ScrollViewer { get; private set; }

        static TabControlEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControlEx), new FrameworkPropertyMetadata(typeof(TabControlEx)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ScrollViewer =  GetTemplateChild(ElementScrollViewer) as ScrollViewer;
            if(ScrollViewer != null)
            {
                ScrollViewer.ScrollChanged += OnScrollChanged;
            }
        }

        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if(e.ExtentWidthChange > 0 && e.ViewportWidth < e.ExtentWidth && AutoScrollToEnd)
            {
                ScrollViewer?.ScrollToRightEnd();
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TabItemEx();
        }
    }
}
