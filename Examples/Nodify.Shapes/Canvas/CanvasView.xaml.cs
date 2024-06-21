using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
    }
}
