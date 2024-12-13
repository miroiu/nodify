using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using Nodify.Interactivity;

namespace Nodify
{
    [StyleTypedProperty(Property = nameof(CuttingLineStyle), StyleTargetType = typeof(CuttingLine))]
    public partial class NodifyEditor
    {
        #region Dependency properties

        protected static readonly DependencyPropertyKey CuttingLineStartPropertyKey = DependencyProperty.RegisterReadOnly(nameof(CuttingLineStart), typeof(Point), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Point));
        public static readonly DependencyProperty CuttingLineStartProperty = CuttingLineStartPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey CuttingLineEndPropertyKey = DependencyProperty.RegisterReadOnly(nameof(CuttingLineEnd), typeof(Point), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.Point));
        public static readonly DependencyProperty CuttingLineEndProperty = CuttingLineEndPropertyKey.DependencyProperty;

        protected static readonly DependencyPropertyKey IsCuttingPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsCutting), typeof(bool), typeof(NodifyEditor), new FrameworkPropertyMetadata(BoxValue.False, OnIsCuttingChanged));
        public static readonly DependencyProperty IsCuttingProperty = IsCuttingPropertyKey.DependencyProperty;

        public static readonly DependencyProperty CuttingLineStyleProperty = DependencyProperty.Register(nameof(CuttingLineStyle), typeof(Style), typeof(NodifyEditor));

        public static readonly DependencyProperty CuttingStartedCommandProperty = DependencyProperty.Register(nameof(CuttingStartedCommand), typeof(ICommand), typeof(NodifyEditor));
        public static readonly DependencyProperty CuttingCompletedCommandProperty = DependencyProperty.Register(nameof(CuttingCompletedCommand), typeof(ICommand), typeof(NodifyEditor));

        private static void OnIsCuttingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = (NodifyEditor)d;
            if ((bool)e.NewValue == true)
                editor.OnCuttingStarted();
            else
                editor.OnCuttingCompleted();
        }

        private void OnCuttingCompleted()
        {
            if (CuttingCompletedCommand?.CanExecute(DataContext) ?? false)
                CuttingCompletedCommand.Execute(DataContext);
        }

        private void OnCuttingStarted()
        {
            if (CuttingStartedCommand?.CanExecute(DataContext) ?? false)
                CuttingStartedCommand.Execute(DataContext);
        }

        /// <summary>
        /// Gets or sets the style to use for the cutting line.
        /// </summary>
        public Style CuttingLineStyle
        {
            get => (Style)GetValue(CuttingLineStyleProperty);
            set => SetValue(CuttingLineStyleProperty, value);
        }

        /// <summary>
        /// Gets the start point of the <see cref="CuttingLine"/> while <see cref="IsCutting"/> is true.
        /// </summary>
        public Point CuttingLineStart
        {
            get => (Point)GetValue(CuttingLineStartProperty);
            private set => SetValue(CuttingLineStartPropertyKey, value);
        }

        /// <summary>
        /// Gets the end point of the <see cref="CuttingLine"/> while <see cref="IsCutting"/> is true.
        /// </summary>
        public Point CuttingLineEnd
        {
            get => (Point)GetValue(CuttingLineEndProperty);
            private set => SetValue(CuttingLineEndPropertyKey, value);
        }

        /// <summary>
        /// Gets a value that indicates whether a cutting operation is in progress.
        /// </summary>
        public bool IsCutting
        {
            get => (bool)GetValue(IsCuttingProperty);
            private set => SetValue(IsCuttingPropertyKey, value);
        }

        /// <summary>Invoked when a cutting operation is started.</summary>
        public ICommand? CuttingStartedCommand
        {
            get => (ICommand?)GetValue(CuttingStartedCommandProperty);
            set => SetValue(CuttingStartedCommandProperty, value);
        }

        /// <summary>Invoked when a cutting operation is completed.</summary>
        public ICommand? CuttingCompletedCommand
        {
            get => (ICommand?)GetValue(CuttingCompletedCommandProperty);
            set => SetValue(CuttingCompletedCommandProperty, value);
        }

        #endregion

        /// <summary>
        /// Gets or sets whether cancelling a cutting operation is allowed (see <see cref="EditorGestures.NodifyEditorGestures.CancelAction"/>).
        /// </summary>
        public static bool AllowCuttingCancellation { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the cutting line should apply the preview style to the interesected elements.
        /// </summary>
        /// <remarks>
        /// This may hurt performance because intersection must be calculated on mouse move.
        /// </remarks>
        public static bool EnableCuttingLinePreview { get; set; } = false;

        /// <summary>
        /// The list of supported connection types for cutting. Type must be derived from <see cref="FrameworkElement" />.
        /// </summary>
        public static readonly HashSet<Type> CuttingConnectionTypes = new HashSet<Type>();

        private List<FrameworkElement>? _cuttingLinePreviousConnections;
        private readonly LineGeometry _cuttingLineGeometry = new LineGeometry();

        /// <summary>
        /// Starts the cutting operation at the current <see cref="MouseLocation"/>. Call <see cref="EndCutting"/> to complete the operation or <see cref="CancelCutting"/> to abort it.
        /// </summary>
        /// <remarks>This method has no effect if a cutting operation is already in progress.</remarks>
        public void BeginCutting()
            => BeginCutting(MouseLocation);

        /// <summary>
        /// Starts the cutting operation at the specified location. Call <see cref="EndCutting"/> to complete the operation or <see cref="CancelCutting"/> to abort it.
        /// </summary>
        /// <remarks>This method has no effect if a cutting operation is already in progress.</remarks>
        /// <param name="location">The starting location for cutting items, in graph space coordinates.</param>
        public void BeginCutting(Point location)
        {
            if (IsCutting)
            {
                return;
            }

            CuttingLineStart = location;
            CuttingLineEnd = location;
            IsCutting = true;

            _cuttingLineGeometry.StartPoint = location;
            _cuttingLineGeometry.EndPoint = location;
        }

        /// <summary>
        /// Updates the current cutting line position and the style for the intersecting elements if <see cref="EnableCuttingLinePreview"/> is true.
        /// </summary>
        /// <param name="amount">The amount to adjust the cutting line's endpoint.</param>
        public void UpdateCuttingLine(Vector amount)
        {
            CuttingLineEnd += amount;

            UpdateCuttingLine(CuttingLineEnd);
        }

        /// <summary>
        /// Updates the current cutting line position and the style for the intersecting elements if <see cref="EnableCuttingLinePreview"/> is true.
        /// </summary>
        /// <param name="location">The location of the cutting line's endpoint.</param>
        public void UpdateCuttingLine(Point location)
        {
            Debug.Assert(IsCutting);
            CuttingLineEnd = location;

            if (EnableCuttingLinePreview)
            {
                _cuttingLineGeometry.EndPoint = CuttingLineEnd;

                ResetConnectionStyle();
                ApplyConnectionStyle();
            }
        }

        /// <summary>
        /// Cancels the current cutting operation without applying any changes if <see cref="AllowCuttingCancellation"/> is true.
        /// Otherwise, it ends the cutting operation by calling <see cref="EndCutting"/>.
        /// </summary>
        /// <remarks>This method has no effect if there's no cutting operation in progress.</remarks>
        public void CancelCutting()
        {
            if (!AllowCuttingCancellation)
            {
                EndCutting();
                return;
            }

            if (IsCutting)
            {
                ResetConnectionStyle();
                IsCutting = false;
            }
        }

        /// <summary>
        /// Completes the cutting operation and applies the changes.
        /// </summary>
        /// <remarks>This method has no effect if there's no cutting operation in progress.</remarks>
        public void EndCutting()
        {
            if (!IsCutting)
            {
                return;
            }

            ResetConnectionStyle();

            var lineGeometry = new LineGeometry(CuttingLineStart, CuttingLineEnd);
            var connections = ConnectionsHost.GetIntersectingElements(lineGeometry, CuttingConnectionTypes);

            if (RemoveConnectionCommand != null)
            {
                foreach (var connection in connections)
                {
                    OnRemoveConnection(connection.DataContext);
                }
            }
            else
            {
                RemoveSupportedConnections(connections);
            }

            IsCutting = false;
        }

        private static void RemoveSupportedConnections(List<FrameworkElement> connections)
        {
            foreach (var connection in connections)
            {
                if (connection is BaseConnection bc)
                {
                    bc.Remove();
                }
            }
        }

        private void ApplyConnectionStyle()
        {
            var connections = ConnectionsHost.GetIntersectingElements(_cuttingLineGeometry, CuttingConnectionTypes);
            foreach (var connection in connections)
            {
                CuttingLine.SetIsOverElement(connection, true);
            }

            _cuttingLinePreviousConnections = connections;
        }

        private void ResetConnectionStyle()
        {
            if (_cuttingLinePreviousConnections != null)
            {
                foreach (var connection in _cuttingLinePreviousConnections)
                {
                    CuttingLine.SetIsOverElement(connection, false);
                }

                _cuttingLinePreviousConnections = null;
            }
        }
    }
}
