using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;

namespace Nodify.Shapes.Web;

internal partial class Program
{
    private static async Task Main(string[] args)
    {
        await BuildAvaloniaApp()
            .WithInterFont()
            .StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}