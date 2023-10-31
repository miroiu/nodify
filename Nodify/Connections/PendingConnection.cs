using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Nodify
{
    /// <summary>
    /// Represents a pending connection usually started by a <see cref="Connector"/> which invokes the <see cref="CompletedCommand"/> when completed.
    /// </summary>
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
        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(typeof(PendingConnection));
        public static readonly DependencyProperty AllowOnlyConnectorsProperty = DependencyProperty.Register(nameof(AllowOnlyConnectors), typeof(bool), typeof(PendingConnection), new FrameworkPropertyMetadata(BoxValue.True, OnAllowOnlyConnectorsChanged));
        public static readonly DependencyProperty EnableSnappingProperty = DependencyProperty.Register(nameof(EnableSnapping), typeof(bool), typeof(PendingConnection), new FrameworkPropertyMetadata(BoxValue.False));
        public static readonly DependencyProperty DirectionProperty = BaseConnection.DirectionProperty.AddOwner(typeof(PendingConnection));
        public new static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(nameof(IsVisible), typeof(bool), typeof(PendingConnection), new FrameworkPropertyMetadata(BoxValue.False, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnVisibilityChanged));

        private static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var connection = (PendingConnection)d;
            connection.Visibility = ((bool)e.NewValue) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Gets or sets the starting point for the connection.
        /// </summary>
        public Point SourceAnchor
        {
            get => (Point)GetValue(SourceAnchorProperty);
            set => SetValue(SourceAnchorProperty, value);
        }

        /// <summary>
        /// Gets or sets the end point for the connection.
        /// </summary>
        public Point TargetAnchor
        {
            get => (Point)GetValue(TargetAnchorProperty);
            set => SetValue(TargetAnchorProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Connector"/>'s <see cref="FrameworkElement.DataContext"/> that started this pending connection.
        /// </summary>
        public object? Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Connector"/>'s <see cref="FrameworkElement.DataContext"/> (or potentially an <see cref="ItemContainer"/>'s <see cref="FrameworkElement.DataContext"/> if <see cref="AllowOnlyConnectors"/> is false) that the <see cref="Source"/> can connect to.
        /// Only set when the connection is completed (see <see cref="CompletedCommand"/>).
        /// </summary>
        public object? Target
        {
            get => GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        /// <summary>
        /// <see cref="PreviewTarget"/> will be updated with a potential <see cref="Connector"/>'s <see cref="FrameworkElement.DataContext"/> if this is true.
        /// </summary>
        public bool EnablePreview
        {
            get => (bool)GetValue(EnablePreviewProperty);
            set => SetValue(EnablePreviewProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Connector"/> or the <see cref="ItemContainer"/> (if <see cref="AllowOnlyConnectors"/> is false) that we're previewing.
        /// </summary>
        public object? PreviewTarget
        {
            get => GetValue(PreviewTargetProperty);
            set => SetValue(PreviewTargetProperty, value);
        }

        /// <summary>
        /// Enables snapping the <see cref="TargetAnchor"/> to a possible <see cref="Target"/> connector.
        /// </summary>
        public bool EnableSnapping
        {
            get => (bool)GetValue(EnableSnappingProperty);
            set => SetValue(EnableSnappingProperty, value);
        }

        /// <summary>
        /// If true will preview and connect only to <see cref="Connector"/>s, otherwise will also enable <see cref="ItemContainer"/>s.
        /// </summary>
        public bool AllowOnlyConnectors
        {
            get => (bool)GetValue(AllowOnlyConnectorsProperty);
            set => SetValue(AllowOnlyConnectorsProperty, value);
        }

        /// <summary>
        /// Gets or set the connection thickness.
        /// </summary>
        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets the pattern of dashes and gaps that is used to outline the connection.
        /// </summary>
        public DoubleCollection StrokeDashArray
        {
            get => (DoubleCollection)GetValue(StrokeDashArrayProperty);
            set => SetValue(StrokeDashArrayProperty, value);
        }

        /// <summary>
        /// Gets or sets the stroke color of the connection.
        /// </summary>
        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        /// <summary>
        /// Gets or sets the visibility of the connection.
        /// </summary>
        public new bool IsVisible
        {
            get => base.IsVisible;
            set => SetValue(IsVisibleProperty, value);
        }

        /// <summary>
        /// Gets or sets the direction of this connection.
        /// </summary>
        public ConnectionDirection Direction
        {
            get => (ConnectionDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        #endregion

        #region Attached Properties

        private static readonly DependencyProperty AllowOnlyConnectorsAttachedProperty = DependencyProperty.RegisterAttached("AllowOnlyConnectorsAttached", typeof(bool), typeof(PendingConnection), new FrameworkPropertyMetadata(BoxValue.True));
        /// <summary>
        /// Will be set for <see cref="Connector"/>s and <see cref="ItemContainer"/>s when the pending connection is over the element if <see cref="EnablePreview"/> or <see cref="EnableSnapping"/> is true.
        /// </summary>
        public static readonly DependencyProperty IsOverElementProperty = DependencyProperty.RegisterAttached("IsOverElement", typeof(bool), typeof(PendingConnection), new FrameworkPropertyMetadata(BoxValue.False));

        internal static bool GetAllowOnlyConnectorsAttached(UIElement elem)
            => (bool)elem.GetValue(AllowOnlyConnectorsAttachedProperty);

        internal static void SetAllowOnlyConnectorsAttached(UIElement elem, bool value)
            => elem.SetValue(AllowOnlyConnectorsAttachedProperty, value);

        public static bool GetIsOverElement(UIElement elem)
            => (bool)elem.GetValue(IsOverElementProperty);

        public static void SetIsOverElement(UIElement elem, bool value)
            => elem.SetValue(IsOverElementProperty, value);

        private static void OnAllowOnlyConnectorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NodifyEditor? editor = ((PendingConnection)d).Editor;

            if (editor != null)
            {
                SetAllowOnlyConnectorsAttached(editor, (bool)e.NewValue);
            }
        }

        #endregion

        #region Commands

        public static readonly DependencyProperty StartedCommandProperty = DependencyProperty.Register(nameof(StartedCommand), typeof(ICommand), typeof(PendingConnection));
        public static readonly DependencyProperty CompletedCommandProperty = DependencyProperty.Register(nameof(CompletedCommand), typeof(ICommand), typeof(PendingConnection));

        /// <summary>
        /// Gets or sets the command to invoke when the pending connection is started.
        /// Will not be invoked if <see cref="NodifyEditor.ConnectionStartedCommand"/> is used.
        /// <see cref="Source"/> will be set to the <see cref="Connector"/>'s <see cref="FrameworkElement.DataContext"/> that started this connection and will also be the command's parameter.
        /// </summary>
        public ICommand? StartedCommand
        {
            get => (ICommand?)GetValue(StartedCommandProperty);
            set => SetValue(StartedCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the command to invoke when the pending connection is completed.
        /// Will not be invoked if <see cref="NodifyEditor.ConnectionCompletedCommand"/> is used.
        /// <see cref="Target"/> will be set to the desired <see cref="Connector"/>'s <see cref="FrameworkElement.DataContext"/> and will also be the command's parameter.
        /// </summary>
        public ICommand? CompletedCommand
        {
            get => (ICommand?)GetValue(CompletedCommandProperty);
            set => SetValue(CompletedCommandProperty, value);
        }

        #endregion

        #region Fields

        /// <summary>
        /// Gets the <see cref="NodifyEditor"/> that owns this <see cref="PendingConnection"/>.
        /// </summary>
        protected NodifyEditor? Editor { get; private set; }

        private FrameworkElement? _previousConnector;

        #endregion

        static PendingConnection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PendingConnection), new FrameworkPropertyMetadata(typeof(PendingConnection)));
        }

        /// <inheritdoc />
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

        #region Event Handlers

        protected virtual void OnPendingConnectionStarted(object sender, PendingConnectionEventArgs e)
        {
            if (!e.Handled && !e.Canceled)
            {
                e.Handled = true;
                e.Canceled = !StartedCommand?.CanExecute(e.SourceConnector) ?? false;
                    
                Target = null;
                IsVisible = !e.Canceled;
                SourceAnchor = e.Anchor;
                TargetAnchor = new Point(e.Anchor.X + e.OffsetX, e.Anchor.Y + e.OffsetY);
                Source = e.SourceConnector;

                if (!e.Canceled)
                {
                    StartedCommand?.Execute(Source);
                }

                if(EnablePreview)
                {
                    PreviewTarget = e.SourceConnector;
                }
            }
        }

        protected virtual void OnPendingConnectionDrag(object sender, PendingConnectionEventArgs e)
        {
            if (!e.Handled && IsVisible)
            {
                e.Handled = true;
                TargetAnchor = new Point(e.Anchor.X + e.OffsetX, e.Anchor.Y + e.OffsetY);

                if (Editor != null && (EnablePreview || EnableSnapping))
                {
                    // Look for a potential connector
                    FrameworkElement? connector = GetPotentialConnector(Editor, AllowOnlyConnectors);

                    // Update the connector's anchor and snap to it if snapping is enabled
                    if (EnableSnapping && connector is Connector target)
                    {
                        target.UpdateAnchor();
                        TargetAnchor = target.Anchor;
                    }

                    // If it's not the same connector
                    if (connector != _previousConnector)
                    {
                        if (_previousConnector != null)
                        {
                            SetIsOverElement(_previousConnector, false);
                        }

                        // And we have a connector
                        if (connector != null)
                        {
                            SetIsOverElement(connector, true);
                        }

                        // Update the preview target if enabled
                        if (EnablePreview)
                        {
                            PreviewTarget = connector?.DataContext;
                        }

                        _previousConnector = connector;
                    }
                }
            }
        }

        protected virtual void OnPendingConnectionCompleted(object sender, PendingConnectionEventArgs e)
        {
            if (!e.Handled && IsVisible)
            {
                e.Handled = true;
                IsVisible = false;

                if (_previousConnector != null)
                {
                    SetIsOverElement(_previousConnector, false);
                    _previousConnector = null;
                }

                if (!e.Canceled)
                {
                    Target = e.TargetConnector;

                    // Invoke the CompletedCommand if event is not handled
                    if (CompletedCommand?.CanExecute(Target) ?? false)
                    {
                        CompletedCommand?.Execute(Target);
                    }
                }

                if(EnablePreview)
                {
                    PreviewTarget = null;
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>Searches for a potential connector prioritizing <see cref="Connector"/>s</summary>
        /// <param name="editor">The editor to scan for connectors or item containers.</param>
        /// <param name="allowOnlyConnectors">Will also look for <see cref="ItemContainer"/>s if false.</param>
        /// <returns>A connector, an item container, the editor or null.</returns>
        internal static FrameworkElement? GetPotentialConnector(NodifyEditor editor, bool allowOnlyConnectors)
        {
            Connector? connector = editor.ItemsHost.GetElementUnderMouse<Connector>();
            if (connector != null && connector.Editor == editor)
                return connector;

            if (allowOnlyConnectors)
                return null;

            var itemContainer = editor.ItemsHost.GetElementUnderMouse<ItemContainer>();
            if (itemContainer != null && itemContainer.Editor == editor)
                return itemContainer;

            return editor;
        }

        #endregion
    }
}