using Nodify.Events;
using Nodify.Shapes.Canvas.UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Nodify.Shapes.Canvas
{
    public partial class CanvasView : UserControl
    {
        private readonly DispatcherTimer _moveToLocationTimer;
        private readonly DispatcherTimer _generateLocationTimer;
        private readonly Random _random = new Random();
        private readonly Dictionary<UserCursorViewModel, Point> _moveToLocations = new Dictionary<UserCursorViewModel, Point>();

        public CanvasView()
        {
            InitializeComponent();
            _moveToLocationTimer = new DispatcherTimer(TimeSpan.FromSeconds(1d / 60d), DispatcherPriority.Background, OnMoveToLocationTick, Dispatcher);
            _generateLocationTimer = new DispatcherTimer(TimeSpan.FromSeconds(3), DispatcherPriority.Background, OnGenerateNewLocation, Dispatcher);
        }

        public override void OnApplyTemplate()
        {
            _moveToLocationTimer.Start();
            _generateLocationTimer.Start();

            OnGenerateNewLocation(this, EventArgs.Empty);

            base.OnApplyTemplate();
        }

        private void OnGenerateNewLocation(object? sender, EventArgs e)
        {
            var canvasVM = (CanvasViewModel)DataContext;

            foreach (var cursor in canvasVM.Cursors)
            {
                _moveToLocations[cursor] = Editor.MouseLocation + new Vector(_random.Next(-1500, 1500), _random.Next(-1000, 1000));
            }
        }

        private void OnMoveToLocationTick(object? sender, EventArgs e)
        {
            var canvasVM = (CanvasViewModel)DataContext;
            double speed = 0.015d;

            for (int i = 0; i < canvasVM.Cursors.Count; i++)
            {
                var user = canvasVM.Cursors[i];
                var targetLocation = _moveToLocations[user];

                Vector dir = targetLocation - user.Location;

                var newLocation = user.Location + dir * speed;
                user.Location = newLocation;
            }
        }

        #region Drawing shapes

        private ShapeViewModel? _drawingShape;
        private Point _initialLocation;

        private void Editor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var toolbarVm = ((CanvasViewModel)DataContext).CanvasToolbar;
            if (toolbarVm.SelectedTool != CanvasTool.None && DrawingGesturesMappings.Instance.Draw.Matches(this, e))
            {
                _initialLocation = Editor.MouseLocation;
                _drawingShape = toolbarVm.CreateShapeAtLocation(Editor.MouseLocation);
            }
        }

        private void Editor_MouseMove(object sender, MouseEventArgs e)
        {
            if (_drawingShape != null)
            {
                _drawingShape.Width = Math.Abs(Editor.MouseLocation.X - _initialLocation.X);
                _drawingShape.Height = Math.Abs(Editor.MouseLocation.Y - _initialLocation.Y);

                if (Editor.MouseLocation.X < _initialLocation.X)
                {
                    _drawingShape.Location = new Point(Editor.MouseLocation.X, _drawingShape.Location.Y);
                }

                if (Editor.MouseLocation.Y < _initialLocation.Y)
                {
                    _drawingShape.Location = new Point(_drawingShape.Location.X, Editor.MouseLocation.Y);
                }
            }
        }

        private void Editor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _drawingShape = null;
        }

        #endregion

        private void Minimap_Zoom(object sender, ZoomEventArgs e)
        {
            Editor.ZoomAtPosition(e.Zoom, e.Location);
        }

        private void Editor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedNodes = e.AddedItems.OfType<ShapeViewModel>().ToList();
            var deselectedNodes = e.RemovedItems.OfType<ShapeViewModel>().ToList();

            if (selectedNodes.Count == 0 && deselectedNodes.Count == 0)
            {
                // The selection event most likely contains the selected connections
                return;
            }

            string batchLabel = selectedNodes.Count > 0 ? "Select shapes" : "Deselect shapes";
            var canvasVM = (CanvasViewModel)DataContext;

            using (canvasVM.UndoRedo.Batch(batchLabel))
            {
                var selectNodes = new SelectShapesAction(selectedNodes, canvasVM);
                var deselectNodes = new DeselectShapesAction(deselectedNodes, canvasVM);

                canvasVM.UndoRedo.Record(deselectNodes);
                canvasVM.UndoRedo.Record(selectNodes);
            }
        }

        private void Editor_ItemsMoved(object sender, ItemsMovedEventArgs e)
        {
            var shapes = e.Items.Cast<ShapeViewModel>().ToList();

            var canvasVM = (CanvasViewModel)DataContext;
            using (canvasVM.UndoRedo.Batch("Move shapes"))
            {
                var moveShapes = new MoveShapesAction(shapes, e.Offset);
                canvasVM.UndoRedo.Record(moveShapes);
            }
        }
    }
}
