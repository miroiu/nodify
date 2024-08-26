using System.Threading;

namespace Nodify;

public partial class BaseConnection
{
    private CancellationTokenSource? animationTokenSource;

    static BaseConnection()
    {
        AffectsRender<BaseConnection>(SourceProperty, TargetProperty, SourceOffsetProperty, TargetOffsetProperty,
            SourceOffsetModeProperty, TargetOffsetModeProperty, DirectionProperty, SpacingProperty, ArrowSizeProperty, ArrowEndsProperty, ArrowShapeProperty, TextProperty, DirectionalArrowsCountProperty, DirectionalArrowsOffsetProperty, OutlineThicknessProperty, OutlineBrushProperty);
        AffectsGeometry<BaseConnection>(SourceProperty, TargetProperty, SourceOffsetProperty, TargetOffsetProperty,
            SourceOffsetModeProperty, TargetOffsetModeProperty, DirectionProperty, SpacingProperty, ArrowSizeProperty, ArrowEndsProperty, ArrowShapeProperty, SourceOrientationProperty, TargetOrientationProperty, DirectionalArrowsCountProperty, DirectionalArrowsOffsetProperty, OutlineThicknessProperty, OutlineBrushProperty);
        OutlineBrushProperty.Changed.AddClassHandler<BaseConnection>(OnOutlinePenChanged);
        OutlineThicknessProperty.Changed.AddClassHandler<BaseConnection>(OnOutlinePenChanged);
    }
}