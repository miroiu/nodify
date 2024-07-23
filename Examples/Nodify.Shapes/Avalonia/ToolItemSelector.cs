using System;
using Avalonia.Controls.Templates;
using Nodify.Shapes.Canvas;

namespace Nodify.Shapes.Controls;

public class CanvasToolItemSelector : IDataTemplate
{
    public IDataTemplate NoneTemplate { get; set; }
    public IDataTemplate EllipseTemplate { get; set; }
    public IDataTemplate RectangleTemplate { get; set; }
    public IDataTemplate TriangleTemplate { get; set; }

    public Control? Build(object? param)
    {
        if (param is CanvasTool tool)
        {
            return tool switch
            {
                CanvasTool.None => NoneTemplate.Build(null),
                CanvasTool.Ellipse => EllipseTemplate.Build(null),
                CanvasTool.Rectangle => RectangleTemplate.Build(null),
                CanvasTool.Triangle => TriangleTemplate.Build(null),
                _ => throw new ArgumentOutOfRangeException(nameof(tool), tool, null)
            };
        }

        return null;
    }

    public bool Match(object? data)
    {
        return data is CanvasTool;
    }
}