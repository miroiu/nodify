using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Nodifier
{
    public interface IViewResolver
    {
        UIElement GetView(Type modelType);
    }

    public class DefaultViewResolver : IViewResolver
    {
        private readonly IViewCollection _views;
        private readonly IServiceProvider _serviceProvider;

        public DefaultViewResolver(IServiceProvider provider, IViewCollection views)
        {
            _serviceProvider = provider;
            _views = views;
        }

        public UIElement GetView(Type modelType)
        {
            var viewType = _views.Get(modelType);

            if (viewType is null)
            {
                throw new InvalidOperationException($"No views were registered for type: {modelType.FullName}");
            }

            var view = (UIElement)_serviceProvider.GetRequiredService(viewType);
            return view;
        }
    }
}
