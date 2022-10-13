## Changelog

#### **In development**

> - Breaking Changes:
>   - Removed Selection field from NodifyEditor
>   - Removed InitialMousePosition, CurrentMousePosition, PreviousMousePosition fields from NodifyEditor
> - Features:
>   - Added editor states which can be overriden by inheriting from NodifyEditor and implementing NodifyEditor.GetDefaultState().
>     - EditorState, EditorDefaultState, EditorSelectingState, EditorPanningState
>   - Added configurable key/mouse bindings
>     - EditorGestures.Selecting, EditorGestures.Panning, EditorGestures.Zooming
>   - Added PushState, PopState and PopAllStates to NodifyEditor
> - Bugfixes:
>   - Fixed HandleRightClickAfterPanningThreshold not working as expected
>   - Fixed DisablePanning not disabling auto panning in certain situations
> - Others:
>   - Changed the default AutoPanSpeed to 15 from 10 pixels per tick.

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
>   - Auto panning speed now scales with the zoom factor.
> - Bugfixes:
>   - Every public property or method should work with graph space coordinates
>   - Disable auto panning when panning is disabled
>   - Min zoom could be set to a very small value
>   - Bring into view was not disabling all interfering operations
