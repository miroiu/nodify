using System;
using System.Windows;
using System.Windows.Markup;

namespace Nodifier.XAML
{
    public interface IViewFor<T>
    {
    }

    public interface IViewAware
    {
        void AttachView(UIElement view);
        UIElement? View { get; }
    }

    public interface IViewManager
    {
        void OnModelChanged(DependencyObject targetLocation, object oldValue, object? newValue);
    }

    /// Source: https://github.com/canton7/Stylet
    public class ViewManager : IViewManager
    {
        private readonly IViewFactory _viewResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewManager"/> class, with the given viewFactory
        /// </summary>
        /// <param name="factory">The view factory.</param>
        public ViewManager(IViewFactory factory)
        {
            _viewResolver = factory;
        }

        /// <summary>
        /// Called by View whenever its current View.Model changes. Will locate and instantiate the correct view, and set it as the target's Content
        /// </summary>
        /// <param name="targetLocation">Thing which View.Model was changed on. Will have its Content set</param>
        /// <param name="oldValue">Previous value of View.Model</param>
        /// <param name="newValue">New value of View.Model</param>
        public virtual void OnModelChanged(DependencyObject targetLocation, object oldValue, object? newValue)
        {
            if (oldValue == newValue)
                return;

            if (newValue != null)
            {
                var view = CreateAndBindViewForModelIfNecessary(newValue);
                if (view is Window)
                {
                    var e = new InvalidOperationException(string.Format("s:View.Model=\"...\" tried to show a View of type '{0}', but that View derives from the Window class. " +
                    "Make sure any Views you display using s:View.Model=\"...\" do not derive from Window (use UserControl or similar)", view.GetType().Name));
                    throw e;
                }
                View.SetContentProperty(targetLocation, view);
            }
            else
            {
                View.SetContentProperty(targetLocation, null);
            }
        }

        /// <summary>
        /// Create a View for the given ViewModel, and bind the two together, if the model doesn't already have a view
        /// </summary>
        /// <param name="model">ViewModel to create a Veiw for</param>
        /// <returns>Newly created View, bound to the given ViewModel</returns>
        public virtual UIElement CreateAndBindViewForModelIfNecessary(object model)
        {
            var modelAsViewAware = model as IViewAware;
            if (modelAsViewAware != null && modelAsViewAware.View != null)
            {
                return modelAsViewAware.View;
            }

            var view = _viewResolver.GetView(model.GetType());

            if (view is IComponentConnector connector)
                connector.InitializeComponent();

            BindViewToModel(view, model);

            return view;
        }

        /// <summary>
        /// Given an instance of a ViewModel and an instance of its View, bind the two together
        /// </summary>
        /// <param name="view">View to bind to the ViewModel</param>
        /// <param name="viewModel">ViewModel to bind the View to</param>
        public virtual void BindViewToModel(UIElement view, object viewModel)
        {
            View.SetActionTarget(view, viewModel);

            if (view is FrameworkElement viewAsFrameworkElement)
            {
                viewAsFrameworkElement.DataContext = viewModel;
            }

            if (viewModel is IViewAware viewModelAsViewAware)
            {
                viewModelAsViewAware.AttachView(view);
            }
        }
    }
}
