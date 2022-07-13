using Stylet;
using Stylet.Xaml;
using StyletIoC;

namespace NodifyBlueprint
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

            builder.Bind<IViewManager>().To<NodifyViewManager>();
        }

        protected override void Launch()
        {

        }
    }
}
