## Changelog

#### **In development**

> - Breaking Changes:
> - Features:
> - Bugfixes:

#### **Version 7.1.0**

> - Breaking Changes:
>	- Added ProcessHandledEvents to IInputHandler and removed it from InputProcessor
>	- Renamed EditorGestures.Editor.ResetViewportLocation to EditorGestures.Editor.ResetViewport
> - Features:
>	- Introduced a new BringIntoView method overload in NodifyEditor that accepts an offset from the viewport edges
>	- Added BringIntoViewEdgeOffset to NodifyEditor to control the viewport edge offset when bringing the focused element into view
>	- Added ResetViewport to NodifyEditor to reset the viewport's location and zoom
>	- Improved tab and directional navigation, ensuring that focused elements are automatically brought into view
>	- Added keyboard navigation layers for nodes, connections and decorators; restricting keyboard navigation to the active layer
>	- Added ActiveNavigationLayer, ActivateNextNavigationLayer, ActivatePreviousNavigationLayer, RegisterNavigationLayer, RemoveNavigationLayer and ActivateNavigationLayer to NodifyEditor for keyboard layers management
>	- Added KeyboardNavigationLayer property to NodifyEditor that allows navigating through the ItemContainers
>	- Added AutoRegisterConnectionsLayer, AutoRegisterDecoratorsLayer, AutoFocusFirstElement, AutoPanOnNodeFocus, PanViewportOnKeyboardDrag and MinimumNavigationStepSize to NodifyEditor
>	- Added EditorGestures.Editor.Keyboard for keyboard navigation gestures
>	- Added FindNextFocusTarget, OnElementFocused and OnKeyboardNavigationLayerActivated virtual methods to NodifyEditor
>	- Added new gestures for keyboard navigation available in EditorGestures.Editor.Keyboard
>	- Added ToggleContentSelection to GroupingNode and its corresponding gesture to toggle the selection of nodes inside the group
>	- Added ZoomIn, ZoomOut and ResetViewport methods to the Minimap control
>	- Added ZoomIn, ZoomOut, ResetViewport and Pan gestures to EditorGestures.Minimap
>	- Added NavigationStepSize static property to Minimap
>	- Added Unbind to all gestures inside EditorGestures
>	- Added the KeyComboGesture that requires a trigger key to be held down before pressing a combo key
>	- Added FocusVisualPen and FocusVisualPadding dependency properties to BaseConnection
>	- Added default focus visuals for base editor controls that can be included by referencing the FocusVisual.xaml file
>	- Added MaxHotKeys and HotKeysDisplayMode static configuration fields to PendingConnection
>	- Added HotKeyControl with its corresponding theme resources to display the hotkeys for a pending connection

#### **Version 7.0.4**

> - Features:
>	- Added AsRef extension method to InputGesture to convert it to an InputGestureRef
> - Bugfixes:
>	- Fixed an issue where the gesture used for EditorGestures.Editor.SelectAll extracted from the ApplicationCommands was assumed to be a KeyGesture
>	- Fixed overrides of DrawDirectionalArrowheadGeometry virtual method not working in subclasses of the built in connections
>	- Fixed a memory leak caused by the auto panning timer

#### **Version 7.0.3**

> - Bugfixes:
>	- Fixed an issue where the SelectedEvent and UnselectedEvent events on the ItemContainer were not raised when the selection was completed

#### **Version 7.0.2**

> - Features:
>	- Added EditorGestures.Editor.SelectAll 
> - Bugfixes:
>	- Fixed an issue where the EditorCommands.SelectAll gesture could not be customized

#### **Version 7.0.1**

> - Bugfixes:
>	- Fixed an issue where connections would not gain focus when selected, which could prevent editor keybindings from functioning in certain scenarios
>	- Resolved an issue where selecting a node did not deselect connections and vice versa
>	- Fixed a bug preventing ItemContainers from being selected when the mouse could not be captured
>	- Fixed an issue with key detection in Japanese IME environments, causing issues with the MouseGesture

#### **Version 7.0.0**

> - Breaking Changes:
>	- Made the setter of NodifyEditor.IsPanning private
>	- Made SelectionHelper internal
>	- Renamed HandleRightClickAfterPanningThreshold to MouseActionSuppressionThreshold in NodifyEditor
>	- Renamed StartCutting to BeginCutting in NodifyEditor
>	- Renamed Connector.EnableStickyConnections to ConnectorState.EnabledToggledConnectingMode
>	- Renamed PushItems to UpdatePushedArea and StartPushingItems to BeginPushingItems in NodifyEditor
>	- Renamed UnselectAllConnection to UnselectAllConnections in NodifyEditor
>	- Removed DragStarted, DragDelta and DragCompleted routed events from ItemContainer
>	- Replaced the System.Windows.Input.MouseGesture with Nodify.Interactivity.MouseGesture for default EditorGesture mappings
>	- Removed State, GetInitialState, PushState, PopState and PopAllStates from NodifyEditor and ItemContainer
>	- Replaced EditorState and ContainerState with InputElementState
>	- Moved AllowCuttingCancellation from CuttingLine to NodifyEditor
>	- Moved AllowDraggingCancellation from ItemContainer to NodifyEditor
>	- Moved EditorGestures under the Nodify.Interactivity namespace
>	- Moved editor events under the Nodify.Events namespace
> - Features:
>	- Added BeginPanning, UpdatePanning, EndPanning, CancelPanning and AllowPanningCancellation to NodifyEditor and Minimap
>	- Added MouseLocation, ZoomAtPosition and GetLocationInsideMinimap to Minimap
>	- Added UpdateCuttingLine to NodifyEditor
>	- Added Select, BeginSelecting, UpdateSelection, EndSelecting, CancelSelecting and AllowSelectionCancellation to NodifyEditor
>	- Added IsDragging, BeginDragging, UpdateDragging, EndDragging and CancelDragging to NodifyEditor
>	- Added AlignSelection and AlignContainers methods to NodifyEditor
>	- Added LockSelection and UnlockSelection methods to NodifyEditor and EditorCommands
>	- Added ItemsMoved routed event to NodifyEditor
>	- Added HasCustomContextMenu dependency property to NodifyEditor, ItemContainer, Connector and BaseConnection
>	- Added Select, BeginDragging, UpdateDragging, EndDragging and CancelDragging to ItemContainer
>	- Added PreserveSelectionOnRightClick configuration field to ItemContainer
>	- Added BeginConnecting, UpdatePendingConnection, EndConnecting, CancelConnecting and RemoveConnections methods to Connector
>	- Added FindTargetConnector and FindConnectionTarget methods to Connector
>	- Added a custom MouseGesture with support for key combinations
>	- Added InputProcessor to NodifyEditor, ItemContainer, Connector, BaseConnection and Minimap, enabling the extension of controls with custom states
>	- Added DragState to simplify creating click-and-drag interactions, with support for initiating and completing them using the keyboard
>	- Added InputElementStateStack, InputElementStateStack.DragState and InputElementStateStack.InputElementState to manage transitions between states in UI elements
>	- Added InputProcessor.Shared to enable the addition of global input handlers
>	- Move the viewport to the mouse position when zooming on the Minimap if ResizeToViewport is false
>	- Added SplitAtLocation and Remove methods to BaseConnection
>	- Added AllowPanningWhileSelecting, AllowPanningWhileCutting and AllowPanningWhilePushingItems to EditorState
>	- Added AllowZoomingWhilePanning, AllowZoomingWhileSelecting, AllowZoomingWhileCutting and AllowZoomingWhilePushingItems to EditorState
>	- Added EnableToggledSelectingMode, EnableToggledPanningMode, EnableToggledPushingItemsMode and EnableToggledCuttingMode to EditorState
>	- Added MinimapState.EnableToggledPanningMode
>	- Added ContainerState.EnableToggledDraggingMode
>	- Added Unbind to InputGestureRef and EditorGestures.SelectionGestures
>	- Added EnableHitTesting to PendingConnection
> - Bugfixes:
>	- Fixed an issue where the ItemContainer was selected by releasing the mouse button on it, even when the mouse was not captured
>	- Fixed an issue where the ItemContainer could open its context menu even when it was not selected
>	- Fixed an issue where the Home button caused the editor to fail to display items when contained within a ScrollViewer
>	- Fixed an issue where connector optimization did not work when SelectedItems was not data-bound
>	- Fixed EditorCommands.Align to perform a single arrange invalidation instead of one for each aligned container
>	- Fixed an issue where controls would capture the mouse unnecessarily; they now capture it only in response to a defined gesture
>	- Fixed an issue where the minimap could update the viewport without having the mouse captured
>	- Fixed ItemContainer.Select and NodifyEditor.SelectArea to clear the existing selection and select the containers within the same transaction
>	- Fixed an issue where editor interactions failed to cancel upon losing mouse capture
>	- Fixed an issue where selecting a new connection would not clear the previous selection within the same transaction
	
#### **Version 6.6.0**

> - Features:
>	- Added InputGroupStyle and OutputGroupStyle to Node
>	- Added PanWithMouseWheel, PanHorizontalModifierKey and PanVerticalModifierKey to EditorGestures.Editor
>	- Added CornerRadius dependency property to LineConnection, CircuitConnection and StepConnection
>	- Added EditorGestures.Editor.PushItems gesture used to start pushing ItemContainers vertically or horizontally
>	- Added PushedAreaStyle, PushedAreaOrientation and IsPushingItems dependency properties to NodifyEditor
>	- Added NodifyEditor.SnapToGrid utility function
> - Bugfixes:
>	- Fixed ItemContainer.BorderBrush and ItemContainer.SelectedBrush not reacting to theme changes

#### **Version 6.5.0**

> - Features:
>	- Added SelectedConnection, SelectedConnections, CanSelectMultipleConnections and CanSelectMultipleItems dependency properties to NodifyEditor
>	- Added IsSelected and IsSelectable attached dependency properties to BaseConnection
>	- Added PrioritizeBaseConnectionForSelection static field to BaseConnection
>	- Added EditorGestures.Connection.Selection
>	- Added support for ScrollViewer in NodifyEditor (implements IScrollInfo)
>	- Added NodifyEditor.ScrollIncrement dependency property

#### **Version 6.4.0**

> - Features:
>	- Added OutlineBrush and OutlineThickness dependency properties to BaseConnection to support increasing the selection area without increasing the stroke thickness
>	- Added IsAnimatingDirectionalArrows and DirectionalArrowsAnimationDuration dependency properties to BaseConnection to support controlling the animation from XAML

#### **Version 6.3.0**

> - Features:
>	- Added a CuttingLine control that removes intersecting connections
>	- Added CuttingLineStyle, CuttingStartedCommand, CuttingCompletedCommand, IsCutting, EnableCuttingLinePreview and CuttingConnectionTypes to NodifyEditor
>	- Added EditorGestures.Editor.Cutting and EditorGestures.Editor.CancelAction
> - Bugfixes:
>	- Fixed connection styles not inheriting from the BaseConnection style

#### **Version 6.2.0**

> - Features:
>	- Added a Minimap control and EditorGestures.Minimap
>	- Added ContentContainerStyle, HeaderContainerStyle and FooterContainerStyle dependency properties to Node
>	- Added BringIntoView that takes a Rect parameter to NodifyEditor
>	- Added the NodifyEditor's DataContext as the parameter of the ItemsSelectStartedCommand, ItemsSelectCompletedCommand, ItemsDragStartedCommand and ItemsDragCompletedCommand commands
> - Bugfixes:
>	- Fixed hover effect and padding of NodeInput and NodeOutput for vertical orientation
>	- Fixed ItemContainers being selected sometimes when double clicking the canvas

#### **Version 6.1.0**

> - Features:
>	- Added new built-in connection type: StepConnection
> - Bugfixes:
>	- Fixed CircuitConnection directional arrows not interpolating correctly
>	- Fixed BaseConnection SplitEvent and DisconnectEvent not being raised if the corresponding command is null
>	- Fixed DecoratorContainer scaling with zoom when not referencing a theme in App.xaml
>	- Fixed style not applying to the default Connection template outside App.xaml
	
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
