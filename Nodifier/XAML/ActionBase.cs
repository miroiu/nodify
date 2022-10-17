using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Nodifier.XAML
{
    /// Source: https://github.com/canton7/Stylet
    /// <summary>
    /// Common base class for CommandAction and EventAction
    /// </summary>
    public abstract class ActionBase : DependencyObject
    {
        /// <summary>
        /// Gets the View to grab the View.ActionTarget from
        /// </summary>
        public DependencyObject? Subject { get; private set; }

        /// <summary>
        /// Gets the method name. E.g. if someone's gone Buttom Command="{s:Action MyMethod}", this is MyMethod.
        /// </summary>
        public string MethodName { get; private set; }

        /// <summary>
        /// Gets the MethodInfo for the method to call. This has to exist, or we throw a wobbly
        /// </summary>
        protected MethodInfo? TargetMethodInfo { get; private set; }

        /// <summary>
        /// Behaviour for if the target is null
        /// </summary>
        protected readonly ActionUnavailableBehaviour TargetNullBehaviour;

        /// <summary>
        /// Behaviour for if the action doesn't exist on the target
        /// </summary>
        protected readonly ActionUnavailableBehaviour ActionNonExistentBehaviour;

        /// <summary>
        /// Gets the object on which methods will be invoked
        /// </summary>
        public object Target
        {
            get => GetValue(_targetProperty);
            private set => SetValue(_targetProperty, value);
        }

        private static readonly DependencyProperty _targetProperty =
            DependencyProperty.Register("target", typeof(object), typeof(ActionBase), new PropertyMetadata(null, (d, e) =>
            {
                ((ActionBase)d).UpdateActionTarget(e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// Initialises a new instance of the <see cref="ActionBase"/> class to use <see cref="View.ActionTargetProperty"/> to get the target
        /// </summary>
        /// <param name="subject">View to grab the View.ActionTarget from</param>
        /// <param name="backupSubject">Backup subject to use if no ActionTarget could be retrieved from the subject</param>
        /// <param name="methodName">Method name. the MyMethod in Buttom Command="{s:Action MyMethod}".</param>
        /// <param name="targetNullBehaviour">Behaviour for it the relevant View.ActionTarget is null</param>
        /// <param name="actionNonExistentBehaviour">Behaviour for if the action doesn't exist on the View.ActionTarget</param>
        public ActionBase(DependencyObject? subject, DependencyObject? backupSubject, string methodName, ActionUnavailableBehaviour targetNullBehaviour, ActionUnavailableBehaviour actionNonExistentBehaviour)
            : this(methodName, targetNullBehaviour, actionNonExistentBehaviour)
        {
            Subject = subject;

            // If a 'backupSubject' was given, bind both that and 'subject' to this.Target (with a converter which picks the first
            // one that isn't View.InitialActionTarget). If it wasn't given, just bind 'subject'.

            var actionTargetBinding = new Binding()
            {
                Path = new PropertyPath(View.ActionTargetProperty),
                Mode = BindingMode.OneWay,
                Source = Subject,
            };

            if (backupSubject == null)
            {
                BindingOperations.SetBinding(this, _targetProperty, actionTargetBinding);
            }
            else
            {
                var multiBinding = new MultiBinding();
                multiBinding.Converter = new MultiBindingToActionTargetConverter();
                multiBinding.Bindings.Add(actionTargetBinding);
                multiBinding.Bindings.Add(new Binding()
                {
                    Path = new PropertyPath(View.ActionTargetProperty),
                    Mode = BindingMode.OneWay,
                    Source = backupSubject,
                });
                BindingOperations.SetBinding(this, _targetProperty, multiBinding);
            }
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ActionBase"/> class to use an explicit target
        /// </summary>
        /// <param name="target">Target to find the method on</param>
        /// <param name="methodName">Method name. the MyMethod in Buttom Command="{s:Action MyMethod}".</param>
        /// <param name="targetNullBehaviour">Behaviour for it the relevant View.ActionTarget is null</param>
        /// <param name="actionNonExistentBehaviour">Behaviour for if the action doesn't exist on the View.ActionTarget</param>
        public ActionBase(object target, string methodName, ActionUnavailableBehaviour targetNullBehaviour, ActionUnavailableBehaviour actionNonExistentBehaviour)
            : this(methodName, targetNullBehaviour, actionNonExistentBehaviour)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        private ActionBase(string methodName, ActionUnavailableBehaviour targetNullBehaviour, ActionUnavailableBehaviour actionNonExistentBehaviour)
        {
            MethodName = methodName ?? throw new ArgumentNullException(nameof(methodName));
            TargetNullBehaviour = targetNullBehaviour;
            ActionNonExistentBehaviour = actionNonExistentBehaviour;
        }

        private void UpdateActionTarget(object oldTarget, object newTarget)
        {
            MethodInfo? targetMethodInfo = null;

            // If it's being set to the initial value, ignore it
            // At this point, we're executing the View's InitializeComponent method, and the ActionTarget hasn't yet been assigned
            // If they've opted to throw if the target is null, then this will cause that exception.
            // We'll just wait until the ActionTarget is assigned, and we're called again
            if (newTarget == View.InitialActionTarget)
                return;

            if (newTarget == null)
            {
                // If it's Enable or Disable we don't do anything - CanExecute will handle this
                if (TargetNullBehaviour == ActionUnavailableBehaviour.Throw)
                {
                    var e = new InvalidOperationException($"ActionTarget on element {Subject} is null (method name is {MethodName})");
                    throw e;
                }
            }
            else
            {
                BindingFlags bindingFlags;
                if (newTarget is Type newTargetType)
                {
                    bindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;
                }
                else
                {
                    newTargetType = newTarget.GetType();
                    bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
                }
                try
                {
                    targetMethodInfo = newTargetType.GetMethod(MethodName, bindingFlags);

                    if (targetMethodInfo != null)
                        AssertTargetMethodInfo(targetMethodInfo, newTargetType);
                }
                catch (AmbiguousMatchException e)
                {
                    var ex = new AmbiguousMatchException($"Ambiguous match for {MethodName} method on {newTargetType.Name}", e);
                    throw ex;
                }
            }

            TargetMethodInfo = targetMethodInfo;

            OnTargetChanged(oldTarget, newTarget);
        }

        /// <summary>
        /// Invoked when a new non-null target is set, which has non-null MethodInfo. Used to assert that the method signature is correct
        /// </summary>
        /// <param name="targetMethodInfo">MethodInfo of method on new target</param>
        /// <param name="newTargetType">Type of new target</param>
        private protected abstract void AssertTargetMethodInfo(MethodInfo targetMethodInfo, Type newTargetType);

        /// <summary>
        /// Invoked when a new target is set, after all other action has been taken
        /// </summary>
        /// <param name="oldTarget">Previous target</param>
        /// <param name="newTarget">New target</param>
        private protected virtual void OnTargetChanged(object oldTarget, object? newTarget) { }

        /// <summary>
        /// Assert that the target is not View.InitialActionTarget
        /// </summary>
        private protected void AssertTargetSet()
        {
            // If we've made it this far and the target is still the default, then something's wrong
            // Make sure they know
            if (Target == View.InitialActionTarget)
            {
                var ex = new InvalidOperationException(string.Format("View.ActionTarget not set on control {0} (method {1}). " +
                    "This probably means the control hasn't inherited it from a parent, e.g. because a ContextMenu or Popup sits in the visual tree. " +
                    "You will need so set 's:View.ActionTarget' explicitly. See the wiki section \"Actions\" for more details.", Subject, MethodName));
                throw ex;
            }

            if (TargetMethodInfo == null && ActionNonExistentBehaviour == ActionUnavailableBehaviour.Throw)
            {
                var ex = new InvalidOperationException($"Unable to find method {MethodName} on {TargetName()}");
                throw ex;
            }
        }

        /// <summary>
        /// Invoke the target method with the given parameters
        /// </summary>
        /// <param name="parameters">Parameters to pass to the target method</param>
        private protected void InvokeTargetMethod(object?[]? parameters)
        {
            try
            {
                object? target = TargetMethodInfo!.IsStatic ? null : Target;
                object? result = TargetMethodInfo.Invoke(target, parameters);
                // Be nice and make sure that any exceptions get rethrown
                if (result is Task task)
                {
                    AwaitTask(task);
                }
            }
            catch (TargetInvocationException e)
            {
                // Be nice and unwrap this for them
                // They want a stack track for their VM method, not us
                // http://stackoverflow.com/a/17091351/1086121
                ExceptionDispatchInfo.Capture(e.InnerException!).Throw();
            }

            static async void AwaitTask(Task t) => await t;
        }

        private string TargetName()
        {
            return Target is Type t
                ? $"static target {t.Name}"
                : $"target {Target.GetType().Name}";
        }

        private class MultiBindingToActionTargetConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                Debug.Assert(values.Length == 2);

                if (values[0] != View.InitialActionTarget)
                    return values[0];

                if (values[1] != View.InitialActionTarget)
                    return values[1];

                return View.InitialActionTarget;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
