using System;
using System.Linq;
using System.Windows.Input;

namespace Nodify.Shapes.Canvas
{
    public enum CanvasTool
    {
        None,
        Ellipse,
        Rectangle,
        Triangle
    }

    public class CanvasToolbarViewModel : ObservableObject
    {
        public static readonly CanvasTool[] AvailableTools = Enum.GetValues(typeof(CanvasTool)).Cast<CanvasTool>().ToArray();

        internal static readonly EditorGestures EditorGestures;

        private bool _locked;
        public bool Locked
        {
            get => _locked;
            set
            {
                if (SetProperty(ref _locked, value))
                {
                    var newMappings = _locked ? LockedGestureMappings.Instance : EditorGestures;
                    EditorGestures.Mappings.Apply(newMappings);

                    if(_locked)
                    {
                        _selectedTool = CanvasTool.None;
                        OnPropertyChanged(nameof(SelectedTool));
                    }
                }
            }
        }

        public ICommand ToggleLockCommand { get; set; }

        private CanvasTool _selectedTool = CanvasTool.None;
        public CanvasTool SelectedTool
        {
            get => _selectedTool;
            set
            {
                if (SetProperty(ref _selectedTool, Locked ? CanvasTool.None : value))
                {
                    var newMappings = _selectedTool == CanvasTool.None ? EditorGestures : DrawingGesturesMappings.Instance;
                    EditorGestures.Mappings.Apply(newMappings);
                }
            }
        }

        public CanvasViewModel Canvas { get; }

        static CanvasToolbarViewModel()
        {
            EditorGestures = new EditorGestures();
            // copy any user modifications
            EditorGestures.Apply(EditorGestures.Mappings);
        }

        public CanvasToolbarViewModel(CanvasViewModel canvas)
        {
            ToggleLockCommand = new DelegateCommand(() => Locked = !Locked);
            Canvas = canvas;
        }
    }

    public class UnboundGestureMappings : EditorGestures
    {
        public static readonly UnboundGestureMappings Instance = new UnboundGestureMappings();

        public UnboundGestureMappings()
        {
            Editor.Selection.Apply(SelectionGestures.None);
            ItemContainer.Selection.Apply(SelectionGestures.None);
            Connection.Disconnect.Value = MultiGesture.None;
            Connector.Connect.Value = MultiGesture.None;
        }
    }

    public class LockedGestureMappings : EditorGestures
    {
        public static readonly LockedGestureMappings Instance = new LockedGestureMappings();

        public LockedGestureMappings()
        {
            Apply(UnboundGestureMappings.Instance);

            Editor.Pan.Value = new AnyGesture(new MouseGesture(MouseAction.LeftClick), new MouseGesture(MouseAction.RightClick), new MouseGesture(MouseAction.MiddleClick));
        }
    }

    public class DrawingGesturesMappings : EditorGestures
    {
        public static readonly DrawingGesturesMappings Instance = new DrawingGesturesMappings();

        public DrawingGesturesMappings()
        {
            Apply(UnboundGestureMappings.Instance);
        }
    }
}
