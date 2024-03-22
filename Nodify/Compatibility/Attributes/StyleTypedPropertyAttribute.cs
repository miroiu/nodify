namespace Nodify.Compatibility;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
internal class StyleTypedPropertyAttribute : Attribute
{
    public string Property { get; set; }

    public Type StyleTargetType { get; set; }
}