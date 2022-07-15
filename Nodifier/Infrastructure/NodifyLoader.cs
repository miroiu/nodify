using Stylet;
using Stylet.Xaml;
using StyletIoC;
using System.Reflection;

namespace Nodifier
{
    public class NodifyLoader : ApplicationLoader
    {
        public NodifyLoader()
        {
            Bootstrapper = new NodifyBootstrapper();
        }
    }

    public class NodifyBootstrapper : StyletIoCBootstrapperBase
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);

            builder.Assemblies.Add(Assembly.GetEntryAssembly());
            builder.Bind<IViewManager>().To<NodifyViewManager>();
        }

        protected override void Launch()
        {

        }
    }
}
