namespace Nodify.Compatibility;

/// <summary>
/// Nodify wants to override Render method in Shape, but this is not legal in Avalonia.
/// This control is a workaround to allow overriding Render method in a Shape.
/// It contains two inner controls: actual shape and actual renderer.
/// </summary>
public class WpfShape : Panel
{
    private InnerShape? _shape;
    private InnerRenderer? _renderer;

    public static readonly StyledProperty<IBrush?> FillProperty = Shape.FillProperty.AddOwner<WpfShape>();

    public static readonly StyledProperty<Stretch> StretchProperty = Shape.StretchProperty.AddOwner<WpfShape>();
    
    public static readonly StyledProperty<IBrush?> StrokeProperty = Shape.StrokeProperty.AddOwner<WpfShape>();
    
    public static readonly StyledProperty<AvaloniaList<double>?> StrokeDashArrayProperty = Shape.StrokeDashArrayProperty.AddOwner<WpfShape>();
    
    public static readonly StyledProperty<double> StrokeDashOffsetProperty = Shape.StrokeDashOffsetProperty.AddOwner<WpfShape>();
    
    public static readonly StyledProperty<double> StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner<WpfShape>();
    
    public static readonly StyledProperty<PenLineCap> StrokeLineCapProperty = Shape.StrokeLineCapProperty.AddOwner<WpfShape>();
    
    public static readonly StyledProperty<PenLineJoin> StrokeJoinProperty = Shape.StrokeJoinProperty.AddOwner<WpfShape>();
    
    protected static void AffectsGeometry<TShape>(params AvaloniaProperty[] properties) where TShape : WpfShape
    {
        foreach (AvaloniaProperty property in properties)
            property.Changed.AddClassHandler<TShape>(AffectsGeometryInvalidate);
    }
    
    protected new static void AffectsRender<T>(params AvaloniaProperty[] properties) where T : WpfShape
    {
        foreach (AvaloniaProperty property in properties)
            property.Changed.AddClassHandler<T>((control, e) => control.InvalidateVisual());
    }
    
    private static void AffectsGeometryInvalidate(WpfShape control, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == BoundsProperty && ((Rect) e.OldValue).Size == ((Rect) e.NewValue).Size)
            return;
        
        control.InvalidateGeometry();
    }

    public WpfShape()
    {
        _shape = new InnerShape();
        _shape.Bind(StretchProperty, this.GetObservable(StretchProperty), BindingPriority.Template);
        _shape.Bind(FillProperty, this.GetObservable(FillProperty), BindingPriority.Template);
        _shape.Bind(StrokeProperty, this.GetObservable(StrokeProperty), BindingPriority.Template);
        _shape.Bind(StrokeThicknessProperty, this.GetObservable(StrokeThicknessProperty), BindingPriority.Template);
        _shape.Bind(StrokeDashArrayProperty, this.GetObservable(StrokeDashArrayProperty), BindingPriority.Template);
        _shape.Bind(StrokeDashOffsetProperty, this.GetObservable(StrokeDashOffsetProperty), BindingPriority.Template);
        _shape.Bind(StrokeLineCapProperty, this.GetObservable(StrokeLineCapProperty), BindingPriority.Template);
        _shape.Bind(StrokeJoinProperty, this.GetObservable(StrokeJoinProperty), BindingPriority.Template);
        _renderer = new InnerRenderer();
        Children.Add(_shape);
        Children.Add(_renderer);
        _renderer.OnRender += OnRender;
        _shape.OnCreateDefiningGeometry += OnCreateDefiningGeometry;
    }

    private void InvalidateGeometry() => _shape?.InvalidateGeometry();
    
    private Geometry? OnCreateDefiningGeometry() => CreateDefiningGeometry();

    private new void InvalidateVisual() => _renderer?.InvalidateVisual();
    
    private void OnRender(DrawingContext drawingContext) => Render(drawingContext);

    protected virtual Geometry? CreateDefiningGeometry() => null;

    protected new virtual void Render(DrawingContext drawingContext) { }
    
    /// <summary>
    /// Gets or sets the <see cref="T:Avalonia.Media.IBrush" /> that specifies how the shape's interior is painted.
    /// </summary>
    public IBrush? Fill
    {
      get => this.GetValue<IBrush?>(FillProperty);
      set => this.SetValue<IBrush?>(FillProperty, value);
    }

    /// <summary>
    /// Gets or sets a <see cref="P:Avalonia.Controls.Shapes.Shape.Stretch" /> enumeration value that describes how the shape fills its allocated space.
    /// </summary>
    public Stretch Stretch
    {
      get => this.GetValue<Stretch>(StretchProperty);
      set => this.SetValue<Stretch>(StretchProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="T:Avalonia.Media.IBrush" /> that specifies how the shape's outline is painted.
    /// </summary>
    public IBrush? Stroke
    {
      get => this.GetValue<IBrush?>(StrokeProperty);
      set => this.SetValue<IBrush?>(StrokeProperty, value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="T:System.Double" /> values that indicate the pattern of dashes and gaps that is used to outline shapes.
    /// </summary>
    public AvaloniaList<double>? StrokeDashArray
    {
      get => this.GetValue<AvaloniaList<double>?>(StrokeDashArrayProperty);
      set => this.SetValue<AvaloniaList<double>?>(StrokeDashArrayProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that specifies the distance within the dash pattern where a dash begins.
    /// </summary>
    public double StrokeDashOffset
    {
      get => this.GetValue<double>(StrokeDashOffsetProperty);
      set => this.SetValue<double>(StrokeDashOffsetProperty, value);
    }

    /// <summary>Gets or sets the width of the shape outline.</summary>
    public double StrokeThickness
    {
      get => this.GetValue<double>(StrokeThicknessProperty);
      set => this.SetValue<double>(StrokeThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets a <see cref="T:Avalonia.Media.PenLineCap" /> enumeration value that describes the shape at the ends of a line.
    /// </summary>
    public PenLineCap StrokeLineCap
    {
      get => this.GetValue<PenLineCap>(StrokeLineCapProperty);
      set => this.SetValue<PenLineCap>(StrokeLineCapProperty, value);
    }

    /// <summary>
    /// Gets or sets a <see cref="T:Avalonia.Media.PenLineJoin" /> enumeration value that specifies the type of join that is used at the vertices of a Shape.
    /// </summary>
    public PenLineJoin StrokeJoin
    {
      get => this.GetValue<PenLineJoin>(StrokeJoinProperty);
      set => this.SetValue<PenLineJoin>(StrokeJoinProperty, value);
    }
    
    private class InnerShape : Shape
    {
        public event Func<Geometry?>? OnCreateDefiningGeometry;
    
        protected override Geometry? CreateDefiningGeometry()
        {
            return OnCreateDefiningGeometry?.Invoke();
        }

        public new void InvalidateGeometry()
        {
            base.InvalidateGeometry();
        }
    }

    private class InnerRenderer : Control
    {
        public event Action<DrawingContext>? OnRender;
    
        public override void Render(DrawingContext context)
        {
            base.Render(context);
            OnRender?.Invoke(context);
        }
    }
}