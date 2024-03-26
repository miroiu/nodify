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
    public partial class PendingConnection : ContentControl
    {
        #region Dependency Properties

        public static readonly StyledProperty<Point> SourceAnchorProperty = AvaloniaProperty.Register<PendingConnection, Point>(nameof(SourceAnchor), BoxValue.Point);
        public static readonly StyledProperty<Point> TargetAnchorProperty = AvaloniaProperty.Register<PendingConnection, Point>(nameof(TargetAnchor), BoxValue.Point);
        public static readonly StyledProperty<object> SourceProperty = AvaloniaProperty.Register<PendingConnection, object>(nameof(Source));
        public static readonly StyledProperty<object> TargetProperty = AvaloniaProperty.Register<PendingConnection, object>(nameof(Target));
        public static readonly StyledProperty<object> PreviewTargetProperty = AvaloniaProperty.Register<PendingConnection, object>(nameof(PreviewTarget));
        public static readonly StyledProperty<bool> EnablePreviewProperty = AvaloniaProperty.Register<PendingConnection, bool>(nameof(EnablePreview), BoxValue.False);
        public static readonly StyledProperty<double> StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner<PendingConnection>();
        public static readonly StyledProperty<AvaloniaList<double>?> StrokeDashArrayProperty = Shape.StrokeDashArrayProperty.AddOwner<PendingConnection>();
        public static readonly StyledProperty<IBrush?> StrokeProperty = Shape.StrokeProperty.AddOwner<PendingConnection>();
        public static readonly StyledProperty<bool> AllowOnlyConnectorsProperty = AvaloniaProperty.Register<PendingConnection, bool>(nameof(AllowOnlyConnectors), BoxValue.True);
        public static readonly StyledProperty<bool> EnableSnappingProperty = AvaloniaProperty.Register<PendingConnection, bool>(nameof(EnableSnapping), BoxValue.False);
        public static readonly StyledProperty<ConnectionDirection> DirectionProperty = BaseConnection.DirectionProperty.AddOwner<PendingConnection>();
        public new static readonly StyledProperty<bool> IsVisibleProperty = AvaloniaProperty.Register<PendingConnection, bool>(nameof(IsVisibleProperty), BoxValue.True, defaultBindingMode: BindingMode.TwoWay);
        
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
        public AvaloniaList<double>? StrokeDashArray
        {
            get => GetValue(StrokeDashArrayProperty);
            set => SetValue(StrokeDashArrayProperty, value);
        }

        /// <summary>
        /// Gets or sets the stroke color of the connection.
        /// </summary>
        public IBrush Stroke
        {
            get => (IBrush?)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        /// <summary>
        /// Gets or sets the visibility of the connection.
        /// </summary>
        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
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

        private static readonly AttachedProperty<bool> AllowOnlyConnectorsAttachedProperty = AvaloniaProperty.RegisterAttached<PendingConnection, Control, bool>("AllowOnlyConnectorsAttached", true);
        /// <summary>
        /// Will be set for <see cref="Connector"/>s and <see cref="ItemContainer"/>s when the pending connection is over the element if <see cref="EnablePreview"/> or <see cref="EnableSnapping"/> is true.
        /// </summary>
        public static readonly AttachedProperty<bool> IsOverElementProperty = AvaloniaProperty.RegisterAttached<PendingConnection, Control, bool>("IsOverElement");

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

        public static readonly StyledProperty<ICommand> StartedCommandProperty = AvaloniaProperty.Register<PendingConnection, ICommand>(nameof(StartedCommand));
        public static readonly StyledProperty<ICommand> CompletedCommandProperty = AvaloniaProperty.Register<PendingConnection, ICommand>(nameof(CompletedCommand));

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
            AffectsRender<PendingConnection>(SourceAnchorProperty, TargetAnchorProperty);
            AllowOnlyConnectorsProperty.Changed.AddClassHandler<PendingConnection>(OnAllowOnlyConnectorsChanged);
            IsOverElementProperty.Changed.AddClassHandler<Control>((c, e) =>
            {
                c.Classes.Set("isOverElement", (bool)e.NewValue);
            });
        }

        /// <inheritdoc />
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

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

        protected virtual void OnPendingConnectionStarted(object? sender, PendingConnectionEventArgs e)
        {
            if (!e.Handled && !e.Canceled)
            {
                e.Handled = true;
                e.Canceled = !StartedCommand?.CanExecute(e.SourceConnector) ?? false;

                SetCurrentValue(TargetProperty, null);
                IsVisible = !e.Canceled;
                SetCurrentValue(SourceAnchorProperty, e.Anchor);
                SetCurrentValue(TargetAnchorProperty, new Point(e.Anchor.X + e.OffsetX, e.Anchor.Y + e.OffsetY));
                SetCurrentValue(SourceProperty, e.SourceConnector);

                if (!e.Canceled)
                {
                    StartedCommand?.Execute(Source);
                }

                if(EnablePreview)
                {
                    SetCurrentValue(PreviewTargetProperty, e.SourceConnector);
                }
            }
        }

        protected virtual void OnPendingConnectionDrag(object? sender, PendingConnectionEventArgs e)
        {
            if (!e.Handled && IsVisible)
            {
                e.Handled = true;
                SetCurrentValue(TargetAnchorProperty, new Point(e.Anchor.X + e.OffsetX, e.Anchor.Y + e.OffsetY));

                if (Editor != null && (EnablePreview || EnableSnapping))
                {
                    // Look for a potential connector
                    FrameworkElement? connector = GetPotentialConnector(Editor, AllowOnlyConnectors, e);

                    // Update the connector's anchor and snap to it if snapping is enabled
                    if (EnableSnapping && connector is Connector target)
                    {
                        target.UpdateAnchor();
                        SetCurrentValue(TargetAnchorProperty, target.Anchor);
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
                            SetCurrentValue(PreviewTargetProperty, connector?.DataContext);
                        }

                        _previousConnector = connector;
                    }
                }
            }
        }

        protected virtual void OnPendingConnectionCompleted(object? sender, PendingConnectionEventArgs e)
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
                    SetCurrentValue(TargetProperty, e.TargetConnector);

                    // Invoke the CompletedCommand if event is not handled
                    if (CompletedCommand?.CanExecute(Target) ?? false)
                    {
                        CompletedCommand?.Execute(Target);
                    }
                }

                if(EnablePreview)
                {
                    SetCurrentValue(PreviewTargetProperty, null);
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>Searches for a potential connector prioritizing <see cref="Connector"/>s</summary>
        /// <param name="editor">The editor to scan for connectors or item containers.</param>
        /// <param name="allowOnlyConnectors">Will also look for <see cref="ItemContainer"/>s if false.</param>
        /// <returns>A connector, an item container, the editor or null.</returns>
        internal static FrameworkElement? GetPotentialConnector(NodifyEditor editor, bool allowOnlyConnectors, EventArgs? e)
        {
            Point position = default;
            if (e is MouseEventArgs mouseEventArgs)
                position = mouseEventArgs.GetPosition(editor.ItemsHost);
            else if (e is MouseButtonEventArgs mouseButtonEventArgs)
                position = mouseButtonEventArgs.GetPosition(editor.ItemsHost);
            else if (e is PendingConnectionEventArgs pendingConnectionEventArgs)
                position = pendingConnectionEventArgs.GetPosition(editor.ItemsHost);
            
            Connector? connector = editor.ItemsHost.GetElementUnderMouse<Connector>(position);
            if (connector != null && connector.Editor == editor)
                return connector;

            if (allowOnlyConnectors)
                return null;

            var itemContainer = editor.ItemsHost.GetElementUnderMouse<ItemContainer>(position);
            if (itemContainer != null && itemContainer.Editor == editor)
                return itemContainer;

            return editor;
        }

        #endregion
    }
}