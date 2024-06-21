using Nodify.Shapes.Canvas.UndoRedo;
using Nodify.Shapes.UndoRedo;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Shapes.Canvas
{
    public class CanvasViewModel : PropertyChangedBase
    {
        public NodifyObservableCollection<ShapeViewModel> Shapes { get; } = new NodifyObservableCollection<ShapeViewModel>();
        public NodifyObservableCollection<ShapeViewModel> SelectedShapes { get; } = new NodifyObservableCollection<ShapeViewModel>();

        public NodifyObservableCollection<ICanvasDecorator> Decorators { get; } = new NodifyObservableCollection<ICanvasDecorator>();
        public NodifyObservableCollection<ConnectionViewModel> Connections { get; } = new NodifyObservableCollection<ConnectionViewModel>();

        private ShapeToolbarViewModel ShapeToolbar { get; } = new ShapeToolbarViewModel();
        public CanvasToolbarViewModel CanvasToolbar { get; }

        public ICommand MoveShapesStartedCommand { get; }
        public ICommand MoveShapesCompletedCommand { get; }
        public ICommand SelectShapesStartedCommand { get; }
        public ICommand SelectShapesCompletedCommand { get; }
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

            UndoCommand = new RequeryCommand(UndoRedo.Undo, () => UndoRedo.CanUndo);
            RedoCommand = new RequeryCommand(UndoRedo.Redo, () => UndoRedo.CanRedo);

            MoveShapesStartedCommand = new DelegateCommand(MoveShapesStartedHandler);
            MoveShapesCompletedCommand = new DelegateCommand(MoveShapesCompletedHandler);

            SelectShapesStartedCommand = new DelegateCommand(SelectShapesStartedHandler);
            SelectShapesCompletedCommand = new DelegateCommand(SelectShapesCompletedHandler);

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
            UndoRedo.ExecuteAction(new MoveShapesAction(this));
            ShapeToolbar.Hide();
        }

        private void MoveShapesCompletedHandler()
        {
            if (UndoRedo.Current is MoveShapesAction movesShapes)
            {
                movesShapes.SaveLocations();
            }

            ShapeToolbar.Show();
        }

        // TODO: This does not fire for single selection (ItemContainer click), therefore I added IsSelected to ShapeViewModel.
        private void SelectShapesStartedHandler()
        {
            // The workaround is to record all IsSelected changes into one history item using History.Pause and History.Resume
            UndoRedo.Pause("Select shapes");
            //UndoRedo.ExecuteAction(new SelectShapesAction(this));
        }

        private void SelectShapesCompletedHandler()
        {
            UndoRedo.Resume();
            //if (UndoRedo.Current is SelectShapesAction selectShapes)
            //{
            //    selectShapes.SaveSelection();
            //}
        }

        private void FillCanvasWithShapes()
        {
            // Disable undo redo to avoid recording object construction
            UndoRedo.IsEnabled = false;

            for (int i = 0; i < 5; i++)
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
                Location = new Point(100, 50)
            };
            Shapes.Add(ellipse);

            var rectangle = new RectangleViewModel
            {
                Location = new Point(400, 100)
            };
            Shapes.Add(rectangle);

            Connections.Add(new ConnectionViewModel(ellipse.RightConnector, rectangle.LeftConnector));

            var ellipse2 = new EllipseViewModel
            {
                Location = new Point(100, 250)
            };
            Shapes.Add(ellipse2);

            var rectangle2 = new RectangleViewModel
            {
                Location = new Point(450, 400)
            };
            Shapes.Add(rectangle2);

            Connections.Add(new ConnectionViewModel(ellipse2.BottomConnector, rectangle2.TopConnector));

            Connections.Add(new ConnectionViewModel(rectangle.RightConnector, rectangle2.RightConnector));

            SelectedShapes.Add(rectangle);

            Decorators.Add(ShapeToolbar);
            Decorators.AddRange(Cursors);

            // Re-enable undo redo
            UndoRedo.IsEnabled = true;
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
            using (UndoRedo.Batch("Delete selection"))
            {
                var selection = SelectedShapes.ToList();

                var action = new DelegateAction(() => Shapes.RemoveRange(selection), () => Shapes.AddRange(selection), "Delete shapes");
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
