namespace Nodify;

internal partial class MinimapPanel
{
    private Point viewportLocation;
    
    static MinimapPanel()
    {
        AffectsMeasure<MinimapPanel>(ViewportSizeProperty, ViewportLocationProperty);
    }
}