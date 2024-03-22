using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify
{
    [TemplatePart(Name = ElementScrollViewer, Type = typeof(ScrollViewer))]
    public class TabControlEx : TabControl
    {
        private const string ElementScrollViewer = "PART_ScrollViewer";

        public static readonly StyledProperty<ICommand?> AddTabCommandProperty = AvaloniaProperty.Register<TabControlEx, ICommand?>(nameof(AddTabCommand));
        public static readonly StyledProperty<bool> AutoScrollToEndProperty = AvaloniaProperty.Register<TabControlEx, bool>(nameof(AutoScrollToEnd));

        public ICommand? AddTabCommand
        {
            get { return (ICommand?)GetValue(AddTabCommandProperty); }
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

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            ScrollViewer = e.NameScope.Find<ScrollViewer>(ElementScrollViewer);
            if(ScrollViewer != null)
            {
                ScrollViewer.ScrollChanged += OnScrollChanged;
            }
        }

        private void OnScrollChanged(object? sender, ScrollChangedEventArgs e)
        {
            if(e.ExtentDelta.X > 0 && ScrollViewer.Viewport.Width < ScrollViewer.Extent.Width && AutoScrollToEnd)
            {
                ScrollViewer?.ScrollToEnd();
            }
        }

        protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
        {
            return new TabItemEx();
        }

        protected override bool NeedsContainerOverride(object? item, int index, out object? recycleKey)
        {
            return NeedsContainer<TabItemEx>(item, out recycleKey);
        }
    }
}
