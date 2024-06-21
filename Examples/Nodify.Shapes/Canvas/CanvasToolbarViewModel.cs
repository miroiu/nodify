using System.Windows.Input;

namespace Nodify.Shapes.Canvas
{
    public class CanvasToolbarViewModel : PropertyChangedBase
    {
        private static readonly EditorGestures EditorGestures;

        private bool _locked;
        public bool Locked
        {
            get => _locked;
            set
            {
                if (SetProperty(ref _locked, value))
                {
                    var newMappings = _locked ? UnboundGestureMappings.Instance : EditorGestures;
                    EditorGestures.Mappings.Apply(newMappings);
                }
            }
        }

        public ICommand ToggleLockCommand { get; set; }

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
            Editor.Pan.Value = new AnyGesture(new MouseGesture(MouseAction.LeftClick), new MouseGesture(MouseAction.RightClick), new MouseGesture(MouseAction.MiddleClick));
            ItemContainer.Selection.Apply(SelectionGestures.None);
            Connection.Disconnect.Value = MultiGesture.None;
            Connector.Connect.Value = MultiGesture.None;
        }
    }
}
