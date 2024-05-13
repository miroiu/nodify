## Changelog

#### **In development**

> - Breaking Changes:
> - Features:
> - Bugfixes:
	
#### **Version 6.0.0**

> - Breaking Changes:
>	- Added a parameter for the orientation to DrawArrowGeometry, DrawDefaultArrowhead, DrawRectangleArrowhead and DrawEllipseArrowhead in BaseConnection
>	- Added source and target parameters to GetTextPosition in BaseConnection
>	- EditorGestures is now a singleton instead of a static class (can be inherited to create custom mappings)
>	- Selection gestures for ItemContainer and GroupingNode are now separated from the NodifyEditor selection gestures
>	- Renamed EditorGestures.Editor.Zoom to ZoomModifierKey
> - Features:
>	- Added SourceOrientation and TargetOrientation to BaseConnection to support vertical connectors (vertical/mixed connection orientation)
>	- Added DirectionalArrowsCount to BaseConnection to allow drawing multipe arrows on a connection flowing in the connection direction
>	- Added DrawDirectionalArrowsGeometry and DrawDirectionalArrowheadGeometry to BaseConnection to allow customizing the directional arrows
>	- Improved EditorGestures to allow changing input gestures at runtime
>	- Added new gesture types: AnyGesture, AllGestures, and InputGestureRef
>	- Added Orientation dependency property to NodeInput and NodeOutput
>	- Added DirectionalArrowsOffset dependency property to BaseConnection
>	- Added StartAnimation and StopAnimation methods to BaseConnection
> - Bugfixes:
>	- Fixed BaseConnection.Text not always displaying in the center of the connection
>	- Fixed a bug where the item container would incorrectly transition to the dragging state on mouse over

#### **Version 5.2.0**

> - Features:
>	- Added Text to BaseConnection, allowing displaying of text on connections
>	- Added Foreground, FontSize, FontWeight, FontStyle, FontStretch and FontFamily to BaseConnection, allowing styling the displaying text
> - Bugfixes:
>   - Fixed MouseCapture not being released when EnableStickyConnections is enabled and the PendingConnection is canceled by a key gesture 

#### **Version 5.1.0**

> - Features:
>   - Added ItemContainer.SelectedBorderThickness dependency property
>   - Added NodifyEditor.GetLocationInsideEditor
> - Bugfixes:
>   - Fixed PendingConnection.PreviewTarget not being set to null when there is no actual target
>   - Fixed PendingConnection.PreviewTarget not being set on Connector.PendingConnectionStartedEvent
>   - Fixed PendingConnection.PreviewTarget not being set to null on Connector.PendingConnectionCompletedEvent
>   - Fixed connectors panel not being affected by Node.VerticalAlignment
>   - Changing BorderThickness causes layout shift when selecting an item container
>   - Fixed the unintentional movement caused by snapping correction

#### **Version 5.0.2**

> - Bugfixes:
>   - Fixed NodeOutput content horizontal alignment
>   - Fixed Connector not opening Context Menu

#### **Version 5.0.1**

> - Bugfixes:
>   - Returning false from PendingConnection.StartedCommand.CanExecute does not stop the creation of a pending connection
>   - BaseConnection.ArrowEnds does not display correctly when BaseConnection.Direction is ConnectionDirection.Backward

#### **Version 5.0.0**

> - Breaking Changes:
>   - Removed BaseConnection.GetArrowHeadPoints
>   - Removed BaseConnection.OffsetMode
>   - Changed return type of BaseConnection.DrawLineGeometry to support both arrowheads no matter the number of points on the line
>   - Changed the default for BaseConnection.SourceOffset and BaseConnection.TargetOffset from Size(0, 0) to Size(14, 0)
>   - Changed the default for BaseConnection.ArrowSize from Size(7, 6) to Size(8, 8)
> - Features:
>   - Added BaseConnection.SourceOffsetMode and BaseConnection.TargetOffsetMode
>   - Added BaseConnection.ArrowEnds dependency property to allow configurable arrowhead ends
>   - Added BaseConnection.ArrowShape dependency property to allow configurable arrowhead shape
>   - Added NodifyEditor.EnableDraggingContainersOptimizations to allow receiving ItemContainer.Location updates in realtime
>   - Added ConnectionOffsetMode.Static to allow offsetting the source and target points of the connection on the X and the Y axis without revolving around the source or target points

#### **Version 4.1.0**

> - Features:
>   - Added EditorGestures.Selection.DefaultMouseAction to make it easier to change between mouse buttons for selection
>   - Added EditorGestures.Selection.Cancel gesture to cancel the selection operation reverting to the previous selection
>   - Added ItemsSelectStartedCommand and ItemsSelectCompletedCommand dependency properties to NodifyEditor for better undo/redo support
> - Bugfixes:
>   - Fixed NodifyEditor.SelectedItems being empty after selection is completed
>   - Fixed drag canceling when Drag and CancelAction are bound to the same gesture

#### **Version 4.0.1**

> - Bugfixes:
>   - Fixed DisablePanning not working anymore

#### **Version 4.0.0**

> - Breaking Changes:
>   - Removed Selection field from NodifyEditor
>   - Removed InitialMousePosition, CurrentMousePosition, PreviousMousePosition fields from NodifyEditor
>   - Removed ItemContainer.DraggableHost (use Editor.ItemsHost instead)
>   - Made SelectionType required in SelectionHelper
>   - Moved GroupingNode.SwitchMovementModeModifierKey to EditorGestures.GroupingNode
>   - Pending connections are now restricted to connect only to Connectors or to NodifyEditors and ItemContainers if PendingConnection.AllowOnlyConnectors is false
> - Features:
>   - Added Connector.EnableStickyConnections to allow completing pending connections in two steps
>   - Added editor states which can be overriden by inheriting from NodifyEditor and implementing NodifyEditor.GetInitialState()
>     - EditorState - base class for all editor states
>     - EditorDefaultState
>     - EditorSelectingState
>     - EditorPanningState
>   - Added container states which can be overriden by inheriting from ItemContainer and implementing ItemContainer.GetInitialState()
>     - ContainerState - base class for all container states
>     - ContainerDefaultState
>     - ContainerDraggingState
>   - Added MultiGesture utility that can combine multiple input gestures into one gesture
>   - Added configurable input gestures for NodifyEditor, ItemContainer, Connector, BaseConnection and GroupingNode to EditorGestures
>   - Added State, PushState, PopState and PopAllStates to NodifyEditor and ItemContainer
>   - Changed the default AutoPanSpeed to 15 from 10 pixels per tick
>   - Allow setting ItemContainer.IsPreviewingLocation from derived classes
> - Bugfixes:
>   - Fixed HandleRightClickAfterPanningThreshold not working as expected
>   - Fixed DisablePanning not disabling auto panning in certain situations
>   - Fixed GroupingNode selection not working with multiple selection modes
>   - Fixed PendingConnection connecting cross editors

#### **Version 3.0.0**

> - Breaking Changes:
>   - Changed Decorators from UIElement collection to IEnumerable
> - Features:
>   - Added ItemsExtent and DecoratorsExtent dependency properties to NodifyEditor
>   - Added DecoratorTemplate dependency property to NodifyEditor
>   - Added FitToScreenExtentMargin static field to NodifyEditor
>   - Added Extent dependency property to NodifyCanvas
> - Bugfixes:
>   - Selection rectangle and Decorators are no longer scaled with the viewport zoom
>   - Fixed connector anchor not updating when container size changed

#### **Version 2.0.1**

> - Bugfixes:
>   - Fixed pending connection default style

#### **Version 2.0.0**

> - Breaking Changes:
>   - Renamed Offset to ViewportLocation in NodifyEditor
>   - Renamed Scale to ViewportZoom in NodifyEditor
>   - Renamed MinScale to MinViewportZoom in NodifyEditor
>   - Renamed MaxScale to MaxViewportZoom in NodifyEditor
>   - Renamed AppliedTransform to ViewportTransform in NodifyEditor
>   - Renamed DirectionalConnection to LineConnection
>   - Removed BringIntoViewAnimationDuration from NodifyEditor
>   - Removed Viewport dependency property from NodifyEditor
>   - Removed ActualSize dependency property from StateNode
>   - Removed Icon dependency property from Node as the icon can _(and should)_ be added in the HeaderTemplate if necessary
>   - PART_ItemsHost is now required for NodifyEditor to work
>   - ItemContainers cannot be used outside a NodifyEditor anymore
>   - ZoomAtPosition now requires graph space coordinates instead of screen space coordinates
>   - Removed custom value converters
>   - Made DependencyObjectExtensions internal
>   - Removed the <http://miroiu.github.io/winfx/xaml/nodify> xaml prefix
> - Features:
>   - Added ResizeStartedEvent routed event to GroupingNode
>   - Added ViewportSize - **OneWayToSource** dependency property to NodifyEditor
>   - Added ActualSize - **OneWayToSource** dependency property to ItemContainer
>   - Added DecoratorContainer and DecoratorContainerStyle dependency properties to NodifyEditor
>   - Added RemoveConnectionCommand command to NodifyEditor
>   - Added DisconnectCommand and SplitCommand commands to BaseConnection
>   - Added ContentBrush dependency property to NodifyEditor
>   - Added HasFooter dependency property to Node
>   - Added FitToScreen command to NodifyEditor and EditorCommands
>   - Added onFinish callback to BringIntoView in NodifyEditor
>   - Added ArrowSize and Spacing dependency properties to all connections inheriting from BaseConnection
>   - Added BringIntoViewMaxDuration dependency property to NodifyEditor
>   - Added BringIntoViewSpeed dependency property to NodifyEditor
>   - Auto panning speed now scales with the zoom factor
> - Bugfixes:
>   - Every public property or method should work with graph space coordinates
>   - Disable auto panning when panning is disabled
>   - Min zoom could be set to a very small value
>   - Bring into view was not disabling all interfering operations
