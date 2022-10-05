using System;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows;
using System.Xaml;

namespace Nodifier.XAML
{
    /// <summary>
    /// What to do if the given target is null, or if the given action doesn't exist on the target
    /// </summary>
    public enum ActionUnavailableBehaviour
    {
        /// <summary>
        /// The default behaviour. What this is depends on whether this applies to an action or target, and an event or ICommand
        /// </summary>
        Default,

        /// <summary>
        /// Enable the control anyway. Clicking/etc the control won't do anything
        /// </summary>
        Enable,

        /// <summary>
        /// Disable the control. This is only valid for commands, not events
        /// </summary>
        Disable,

        /// <summary>
        /// An exception will be thrown when the control is clicked
        /// </summary>
        Throw
    }

    /// <summary>
    /// MarkupExtension used for binding Commands and Events to methods on the View.ActionTarget
    /// </summary>
    public class ActionExtension : MarkupExtension
    {
        /// <summary>
        /// Gets or sets the name of the method to call
        /// </summary>
        [ConstructorArgument("method")]
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets a target to override that set with View.ActionTarget
        /// </summary>
        public object Target { get; set; }

        /// <summary>
        /// Gets or sets the behaviour if the View.ActionTarget is nulil
        /// </summary>
        public ActionUnavailableBehaviour NullTarget { get; set; }

        /// <summary>
        /// Gets or sets the behaviour if the action itself isn't found on the View.ActionTarget
        /// </summary>
        public ActionUnavailableBehaviour ActionNotFound { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="ActionExtension"/> class
        /// </summary>
        public ActionExtension()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ActionExtension"/> class with the given method name
        /// </summary>
        /// <param name="method">Name of the method to call</param>
        public ActionExtension(string method)
        {
            Method = method;
        }

        private ActionUnavailableBehaviour CommandNullTargetBehaviour => NullTarget == ActionUnavailableBehaviour.Default ? ActionUnavailableBehaviour.Disable : NullTarget;

        private ActionUnavailableBehaviour CommandActionNotFoundBehaviour => ActionNotFound == ActionUnavailableBehaviour.Default ? ActionUnavailableBehaviour.Throw : ActionNotFound;

        private ActionUnavailableBehaviour EventNullTargetBehaviour => NullTarget == ActionUnavailableBehaviour.Default ? ActionUnavailableBehaviour.Enable : NullTarget;

        private ActionUnavailableBehaviour EventActionNotFoundBehaviour => ActionNotFound == ActionUnavailableBehaviour.Default ? ActionUnavailableBehaviour.Throw : ActionNotFound;

        /// <summary>
        /// When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Method == null)
                throw new InvalidOperationException("Method has not been set");

            IProvideValueTarget valueService = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget))!;

            switch (valueService.TargetObject)
            {
                case DependencyObject targetObject:
                    return HandleDependencyObject(serviceProvider, valueService, targetObject);
                case CommandBinding commandBinding:
                    return CreateEventAction(serviceProvider, null, ((EventInfo)valueService.TargetProperty).EventHandlerType!, isCommandBinding: true);
                default:
                    // Seems this is the case when we're in a template. We'll get called again properly in a second.
                    // http://social.msdn.microsoft.com/Forums/vstudio/en-US/a9ead3d5-a4e4-4f9c-b507-b7a7d530c6a9/gaining-access-to-target-object-instead-of-shareddp-in-custom-markupextensions-providevalue-method?forum=wpf
                    return this;
            }
        }

        private object HandleDependencyObject(IServiceProvider serviceProvider, IProvideValueTarget valueService, DependencyObject targetObject)
        {
            switch (valueService.TargetProperty)
            {
                case DependencyProperty dependencyProperty when dependencyProperty.PropertyType == typeof(ICommand):
                    // If they're in design mode and haven't set View.ActionTarget, default to looking sensible
                    return CreateCommandAction(serviceProvider, targetObject);
                case EventInfo eventInfo:
                    return CreateEventAction(serviceProvider, targetObject, eventInfo.EventHandlerType!);
                case MethodInfo methodInfo: // For attached events
                    {
                        var parameters = methodInfo.GetParameters();
                        if (parameters.Length == 2 && typeof(Delegate).IsAssignableFrom(parameters[1].ParameterType))
                        {
                            return CreateEventAction(serviceProvider, targetObject, parameters[1].ParameterType);
                        }
                        throw new ArgumentException("Action used with an attached event (or something similar) which didn't follow the normal pattern");
                    }
                default:
                    throw new ArgumentException("Can only use ActionExtension with a Command property or an event handler");
            }
        }

        private ICommand CreateCommandAction(IServiceProvider serviceProvider, DependencyObject targetObject)
        {
            if (Target == null)
            {
                var rootObjectProvider = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider))!;
                var rootObject = rootObjectProvider?.RootObject as DependencyObject;
                return new CommandAction(targetObject, rootObject, Method, CommandNullTargetBehaviour, CommandActionNotFoundBehaviour);
            }
            else
            {
                return new CommandAction(Target, Method, CommandNullTargetBehaviour, CommandActionNotFoundBehaviour);
            }
        }

        private Delegate CreateEventAction(IServiceProvider serviceProvider, DependencyObject? targetObject, Type eventType, bool isCommandBinding = false)
        {
            EventAction ec;
            if (Target == null)
            {
                var rootObjectProvider = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider))!;
                var rootObject = rootObjectProvider?.RootObject as DependencyObject;
                if (isCommandBinding)
                {
                    if (rootObject == null)
                        throw new InvalidOperationException("Action may only be used with CommandBinding from a XAML view (unable to retrieve IRootObjectProvider.RootObject)");
                    ec = new EventAction(rootObject, null, eventType, Method, EventNullTargetBehaviour, EventActionNotFoundBehaviour);
                }
                else
                {
                    ec = new EventAction(targetObject, rootObject, eventType, Method, EventNullTargetBehaviour, EventActionNotFoundBehaviour);
                }
            }
            else
            {
                ec = new EventAction(Target, eventType, Method, EventNullTargetBehaviour, EventActionNotFoundBehaviour);
            }

            return ec.GetDelegate();
        }
    }
}
