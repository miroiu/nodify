namespace Nodify.Compatibility;

internal class ContentPropertyAttribute : Attribute
{
    public ContentPropertyAttribute(string name)
    {
        // in Avalonia use [Content] attribute on the property itself
    }
}