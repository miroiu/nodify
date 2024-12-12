using Nodify.Interactivity;
using System;
using System.Linq;
using System.Windows;
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

        internal static readonly EditorGestures EditorGestures = new EditorGestures();

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

                    if (_locked)
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

        public CanvasToolbarViewModel(CanvasViewModel canvas)
        {
            // copy any user modifications
            EditorGestures.Apply(EditorGestures.Mappings);

            ToggleLockCommand = new DelegateCommand(() => Locked = !Locked);
            Canvas = canvas;
        }

        public ShapeViewModel CreateShapeAtLocation(Point location)
        {
            using (Canvas.UndoRedo.Batch("Create shape"))
            {
                ShapeViewModel shape = SelectedTool switch
                {
                    CanvasTool.Ellipse => new EllipseViewModel(),
                    CanvasTool.Rectangle => new RectangleViewModel(),
                    CanvasTool.Triangle => new TriangleViewModel(),
                    CanvasTool.None => throw new InvalidOperationException("Cannot draw in this state"),
                    _ => throw new NotImplementedException(nameof(CanvasTool)),
                };

                shape.Location = location;
                shape.Text = "Double click to edit";

                Canvas.AddShape(shape);
                Canvas.SelectedShapes.Clear();
                Canvas.SelectedShapes.Add(shape);

                return shape;
            }
        }
    }
}
