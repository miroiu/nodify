// To minimize code changes, those empty namespaces serve as placeholders for the original namespaces in WPF.
// So that original usings don't have to be removed and the code can be compiled without errors.
// This will also help in the future when merging changes from the original WPF codebase.

namespace System.Windows.Data
{
    // Required or else the compiler will complain about empty namespace
    internal class Empty { }
}

namespace System.Windows.Documents
{
    internal class Empty { }
}

namespace System.Windows.Markup
{
    internal class Empty { }
}

namespace System.Windows.Media.Animation
{
    internal class Empty { }
}

namespace System.Windows.Media.Imaging
{
    internal class Empty { }
}

namespace System.Windows.Navigation
{
    internal class Empty { }
}

namespace System.Windows.Controls
{
    internal class Empty { }
}

namespace System.Windows.Shapes
{
    internal class Empty { }
}

namespace System.Windows.Threading
{
    internal class Empty { }
}

namespace System.Windows.Controls.Primitives
{
    internal class Empty { }
}

namespace System.Windows
{
    internal class ThemeInfoAttribute : Attribute
    {
        public ThemeInfoAttribute(ResourceDictionaryLocation a, ResourceDictionaryLocation b)
        {
        
        }
    }

    internal enum ResourceDictionaryLocation
    {
        None,
        SourceAssembly,
        ExternalAssembly
    }
}