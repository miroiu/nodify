using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Nodify
{
    public class PendingConnection : ContentControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty SourceAnchorProperty = DependencyProperty.Register(nameof(SourceAnchor), typeof(Point), typeof(PendingConnection), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty TargetAnchorProperty = DependencyProperty.Register(nameof(TargetAnchor), typeof(Point), typeof(PendingConnection), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(object), typeof(PendingConnection));
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(nameof(Target), typeof(object), typeof(PendingConnection));
        public static readonly DependencyProperty PreviewTargetProperty = DependencyProperty.Register(nameof(PreviewTarget), typeof(object), typeof(PendingConnection));
        public static readonly DependencyProperty EnablePreviewProperty = DependencyProperty.Register(nameof(EnablePreview), typeof(bool), typeof(PendingConnection), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(typeof(PendingConnection));
        public static readonly DependencyProperty StrokeDashArrayProperty = Shape.StrokeDashArrayProperty.AddOwner(typeof(PendingConnection));
        public static readonly DependencyProperty AllowOnlyConnectorsProperty = DependencyProperty.Register(nameof(AllowOnlyConnectors), typeof(bool), typeof(PendingConnection), new FrameworkPropertyMetadata(BoxValue.True, OnAllowOnlyConnectorsChanged));

        private static readonly DependencyProperty AllowOnlyConnectorsAttachedProperty = DependencyProperty.Register("AllowOnlyConnectorsAttached", typeof(bool), typeof(PendingConnection), new FrameworkPropertyMetadata(BoxValue.True));

        internal static bool GetAllowOnlyConnectorsAttached(UIElement elem)
            => (bool)elem.GetValue(AllowOnlyConnectorsAttachedProperty);

        internal static void SetAllowOnlyConnectorsAttached(UIElement elem, bool value)
            => elem.SetValue(AllowOnlyConnectorsAttachedProperty, value);

        private static void OnAllowOnlyConnectorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = ((PendingConnection)d).Editor;

            if (editor != null)
            {
                SetAllowOnlyConnectorsAttached(editor, (bool)e.NewValue);
            }
        }

        public Point SourceAnchor
        {
            get => (Point)GetValue(SourceAnchorProperty);
            set => SetValue(SourceAnchorProperty, value);
        }

        public Point TargetAnchor
        {
            get => (Point)GetValue(TargetAnchorProperty);
            set => SetValue(TargetAnchorProperty, value);
        }

        public object? Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public object? Target
        {
            get => GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        public bool EnablePreview
        {
            get => (bool)GetValue(EnablePreviewProperty);
            set => SetValue(EnablePreviewProperty, value);
        }

        public object? PreviewTarget
        {
            get => GetValue(PreviewTargetProperty);
            set => SetValue(PreviewTargetProperty, value);
        }

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public DoubleCollection StrokeDashArray
        {
            get => (DoubleCollection)GetValue(StrokeDashArrayProperty);
            set => SetValue(StrokeDashArrayProperty, value);
        }

        public new bool IsVisible
        {
            get => base.IsVisible;
            set => Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool AllowOnlyConnectors
        {
            get => (bool)GetValue(AllowOnlyConnectorsProperty);
            set => SetValue(AllowOnlyConnectorsProperty, value);
        }

        #endregion

        protected NodifyEditor? Editor { get; private set; }
        private Connector? _previousConnector;

        static PendingConnection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PendingConnection), new FrameworkPropertyMetadata(typeof(PendingConnection)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Editor = this.GetParentOfType<NodifyEditor>();

            if (Editor != null)
            {
                Editor.AddHandler(Connector.PendingConnectionStartedEvent, new PendingConnectionEventHandler(OnPendingConnectionStarted));
                Editor.AddHandler(Connector.PendingConnectionDragEvent, new PendingConnectionEventHandler(OnPendingConnectionDrag));
                Editor.AddHandler(Connector.PendingConnectionCompletedEvent, new PendingConnectionEventHandler(OnPendingConnectionCompleted));
                SetAllowOnlyConnectorsAttached(Editor, AllowOnlyConnectors);
            }
        }

        protected virtual void OnPendingConnectionStarted(object sender, PendingConnectionEventArgs e)
        {
            Source = e.SourceConnector;
            IsVisible = true;
            SourceAnchor = e.Anchor;
            TargetAnchor = new Point(e.Anchor.X + e.OffsetX, e.Anchor.Y + e.OffsetY);
        }

        protected virtual void OnPendingConnectionDrag(object sender, PendingConnectionEventArgs e)
        {
            if (IsVisible)
            {
                TargetAnchor = new Point(e.Anchor.X + e.OffsetX, e.Anchor.Y + e.OffsetY);

                if (Editor != null && EnablePreview)
                {
                    FrameworkElement? target = Editor?.ItemsHost;
                    target ??= Editor;

                    FrameworkElement? connector = GetPreviewTarget(target!);

                    if (_previousConnector != null)
                    {
                        _previousConnector.IsPendingConnectionOver = false;
                    }

                    if (connector != e.OriginalSource && connector is Connector con)
                    {
                        con.IsPendingConnectionOver = true;
                        _previousConnector = con;
                    }

                    var context = connector?.DataContext;
                    if (PreviewTarget != context)
                    {
                        PreviewTarget = context;
                    }
                }
            }
        }

        private FrameworkElement? GetPreviewTarget(FrameworkElement container)
        {
            FrameworkElement? connector = container.GetElementUnderMouse<Connector>();

            if (connector == null && !AllowOnlyConnectors)
            {
                connector = container.GetElementUnderMouse<Node>() ?? container.GetElementUnderMouse<FrameworkElement>();
            }

            return connector;
        }

        protected virtual void OnPendingConnectionCompleted(object sender, PendingConnectionEventArgs e)
        {
            if (IsVisible)
            {
                IsVisible = false;
                Target = e.TargetConnector;

                if (_previousConnector != null)
                {
                    _previousConnector.IsPendingConnectionOver = false;
                    _previousConnector = null;
                }
            }
        }
    }
}
