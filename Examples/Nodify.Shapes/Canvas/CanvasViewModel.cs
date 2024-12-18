using Nodify.Shapes.Canvas.UndoRedo;
using Nodify.UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Shapes.Canvas
{
    public class CanvasViewModel : ObservableObject
    {
        private readonly NodifyObservableCollection<ShapeViewModel> _shapes = new NodifyObservableCollection<ShapeViewModel>();
        public IReadOnlyCollection<ShapeViewModel> Shapes => _shapes;
        public NodifyObservableCollection<ShapeViewModel> SelectedShapes { get; } = new NodifyObservableCollection<ShapeViewModel>();

        public NodifyObservableCollection<ICanvasDecorator> Decorators { get; } = new NodifyObservableCollection<ICanvasDecorator>();
        public NodifyObservableCollection<ConnectionViewModel> Connections { get; } = new NodifyObservableCollection<ConnectionViewModel>();

        private ShapeToolbarViewModel ShapeToolbar { get; } = new ShapeToolbarViewModel();
        public CanvasToolbarViewModel CanvasToolbar { get; }

        public ICommand MoveShapesStartedCommand { get; }
        public ICommand MoveShapesCompletedCommand { get; }
        public ICommand ResizeShapeStartedCommand { get; }
        public ICommand ResizeShapeCompletedCommand { get; }
        public ICommand CreateConnectionCommand { get; }
        public ICommand RemoveConnectionCommand { get; }
        public ICommand DeleteSelectionCommand { get; }

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        public NodifyObservableCollection<UserCursorViewModel> Cursors { get; } = new NodifyObservableCollection<UserCursorViewModel>();
        public IActionsHistory UndoRedo { get; } = ActionsHistory.Global;

        private readonly Random _rand = new Random();

        public CanvasViewModel()
        {
            CanvasToolbar = new CanvasToolbarViewModel(this);

            UndoCommand = new RequeryCommand(UndoRedo.Undo, () => UndoRedo.CanUndo && !CanvasToolbar.Locked);
            RedoCommand = new RequeryCommand(UndoRedo.Redo, () => UndoRedo.CanRedo && !CanvasToolbar.Locked);

            MoveShapesStartedCommand = new DelegateCommand(MoveShapesStartedHandler);
            MoveShapesCompletedCommand = new DelegateCommand(MoveShapesCompletedHandler);

            ResizeShapeStartedCommand = new DelegateCommand(ResizeShapeStartedHandler);
            ResizeShapeCompletedCommand = new DelegateCommand(ResizeShapeCompletedHandler);

            DeleteSelectionCommand = new DelegateCommand(DeleteSelection, () => !CanvasToolbar.Locked);

            CreateConnectionCommand = new DelegateCommand<(object Source, object Target)>(
                ((object Source, object Target) pendingConnection) => AddConnection((ConnectorViewModel)pendingConnection.Source, (ConnectorViewModel)pendingConnection.Target),
                ((object Source, object Target) pendingConnection) => CanConnect((ConnectorViewModel)pendingConnection.Source, (ConnectorViewModel)pendingConnection.Target));

            RemoveConnectionCommand = new DelegateCommand<ConnectionViewModel>(RemoveConnection);

            SelectedShapes.WhenAdded(shape =>
            {
                ShapeToolbar.Shape = SelectedShapes.Count == 1 ? shape : null;
            });

            SelectedShapes.WhenRemoved(shape =>
            {
                ShapeToolbar.Shape = SelectedShapes.Count == 1 ? SelectedShapes.Single() : null;
            });

            SelectedShapes.WhenCleared(shapes => ShapeToolbar.Shape = null);

            FillCanvasWithShapes();
        }

        private void MoveShapesStartedHandler()
        {
            ShapeToolbar.Hide();
        }

        private void MoveShapesCompletedHandler()
        {
            ShapeToolbar.Show();
        }

        private void ResizeShapeStartedHandler()
        {
            UndoRedo.ExecuteAction(new ResizeShapesAction(this));
        }

        private void ResizeShapeCompletedHandler()
        {
            if (UndoRedo.Current is ResizeShapesAction resizeShapes)
            {
                resizeShapes.SaveSizes();
            }
        }

        private void FillCanvasWithShapes()
        {
            // Disable undo redo to avoid recording object construction
            UndoRedo.IsEnabled = false;

            var cursorCount = _rand.Next(3, 6);
            for (int i = 0; i < cursorCount; i++)
            {
                var color = ShapeViewModel.Colors[_rand.Next(0, ShapeViewModel.Colors.Count)];
                Cursors.Add(new UserCursorViewModel
                {
                    Name = $"User {i + 1}",
                    Color = color,
                    Location = new Point(_rand.Next(0, 1000), _rand.Next(0, 1000))
                });
            }

            var ellipse = new EllipseViewModel
            {
                Location = new Point(100, 50),
                Width = 150,
                Height = 150
            };
            _shapes.Add(ellipse);

            var rectangle = new RectangleViewModel
            {
                Location = new Point(400, 100),
                Width = 150,
                Height = 150
            };
            _shapes.Add(rectangle);

            Connections.Add(new ConnectionViewModel(ellipse.RightConnector, rectangle.LeftConnector));

            var ellipse2 = new EllipseViewModel
            {
                Location = new Point(100, 250),
                Width = 150,
                Height = 150
            };
            _shapes.Add(ellipse2);

            var rectangle2 = new RectangleViewModel
            {
                Location = new Point(450, 400),
                Width = 150,
                Height = 150
            };
            _shapes.Add(rectangle2);

            var triangle = new TriangleViewModel
            {
                Location = new Point(800, 200),
                Width = 150,
                Height = 150
            };
            _shapes.Add(triangle);

            Connections.Add(new ConnectionViewModel(ellipse2.BottomConnector, rectangle2.TopConnector));
            Connections.Add(new ConnectionViewModel(rectangle.RightConnector, rectangle2.RightConnector));

            SelectedShapes.Add(triangle);

            Decorators.Add(ShapeToolbar);
            Decorators.AddRange(Cursors);

            // Re-enable undo redo
            UndoRedo.IsEnabled = true;
        }

        public void AddShape(ShapeViewModel shape)
        {
            var action = new DelegateAction(() => _shapes.Add(shape), () => _shapes.Remove(shape), "Add shape");
            UndoRedo.ExecuteAction(action);
        }

        private void AddConnection(ConnectorViewModel source, ConnectorViewModel target)
        {
            var connection = new ConnectionViewModel(source, target);
            var action = new DelegateAction(() => Connections.Add(connection), () => Connections.Remove(connection), "Connect");
            UndoRedo.ExecuteAction(action);
        }

        private void RemoveConnection(ConnectionViewModel connection)
        {
            var action = new DelegateAction(() => Connections.Remove(connection), () => Connections.Add(connection), "Disconnect");
            UndoRedo.ExecuteAction(action);
        }

        private bool CanConnect(ConnectorViewModel? source, ConnectorViewModel? target)
        {
            return source != null
                && target != null
                && source != target
                && !Connections.Contains(new ConnectionViewModel(source, target));
        }

        public void DeleteSelection()
        {
            if (SelectedShapes.Count == 0)
                return;

            using (UndoRedo.Batch("Delete selection"))
            {
                var selection = SelectedShapes.ToList();

                var action = new DelegateAction(() => _shapes.RemoveRange(selection), () => _shapes.AddRange(selection), "Delete shapes");
                UndoRedo.ExecuteAction(action);

                foreach (var shape in selection)
                {
                    var connectors = new[] { shape.LeftConnector, shape.RightConnector, shape.TopConnector, shape.BottomConnector };
                    var connectionsToRemove = Connections.Where(x => connectors.Contains(x.Source) || connectors.Contains(x.Target)).ToList();

                    foreach (var connection in connectionsToRemove)
                    {
                        RemoveConnection(connection);
                    }
                }
            }
        }
    }
}
