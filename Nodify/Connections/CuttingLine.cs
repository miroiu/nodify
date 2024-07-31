using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Nodify
{
    public class CuttingLine : WpfShape
    {
        public static readonly StyledProperty<Point> StartPointProperty = AvaloniaProperty.Register<CuttingLine, Point>(nameof(StartPoint));
        public static readonly StyledProperty<Point> EndPointProperty = AvaloniaProperty.Register<CuttingLine, Point>(nameof(EndPoint));

        /// <summary>
        /// Will be set for <see cref="BaseConnection"/>s and custom connections when the cutting line intersects with them if <see cref="NodifyEditor.EnableCuttingLinePreview"/> is true.
        /// </summary>
        public static readonly AttachedProperty<bool> IsOverElementProperty = PendingConnection.IsOverElementProperty.AddOwner<CuttingLine>();

        public static bool GetIsOverElement(UIElement elem)
            => (bool)elem.GetValue(IsOverElementProperty);

        public static void SetIsOverElement(UIElement elem, bool value)
            => elem.SetValue(IsOverElementProperty, value);

        /// <summary>
        /// Gets or sets whether cancelling a cutting operation is allowed.
        /// </summary>
        public static bool AllowCuttingCancellation { get; set; } = true;

        /// <summary>
        /// Gets or sets the start point.
        /// </summary>
        public Point StartPoint
        {
            get => (Point)GetValue(StartPointProperty);
            set => SetValue(StartPointProperty, value);
        }

        /// <summary>
        /// Gets or sets the end point.
        /// </summary>
        public Point EndPoint
        {
            get => (Point)GetValue(EndPointProperty);
            set => SetValue(EndPointProperty, value);
        }

        protected override Geometry? CreateDefiningGeometry()
        {
            StreamGeometry _geometry = new StreamGeometry
            {
                //FillRule = FillRule.EvenOdd
            };

            using (StreamGeometryContext context = _geometry.Open())
            {
                context.BeginFigure(StartPoint, false, false);
                context.LineTo(EndPoint, true, true);
            }

            return _geometry;
        }

        static CuttingLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CuttingLine), new FrameworkPropertyMetadata(typeof(CuttingLine)));
            IsHitTestVisibleProperty.OverrideMetadata(typeof(CuttingLine), new StyledPropertyMetadata<bool>(BoxValue.False));
            AffectsGeometry<CuttingLine>(StartPointProperty, EndPointProperty);
            AffectsRender<CuttingLine>(StartPointProperty, EndPointProperty);
        }

        protected override void Render(DrawingContext drawingContext)
        {
            base.Render(drawingContext);

            drawingContext.DrawEllipse(Fill, null, StartPoint, StrokeThickness * 1.2, StrokeThickness * 1.2);
            drawingContext.DrawEllipse(Fill, null, EndPoint, StrokeThickness * 1.2, StrokeThickness * 1.2);
        }
    }
}
