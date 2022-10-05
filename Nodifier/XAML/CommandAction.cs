using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Expressions = System.Linq.Expressions;

namespace Nodifier.XAML
{
    /// <summary>
    /// ICommand returned by ActionExtension for binding buttons, etc, to methods on a ViewModel.
    /// If the method has a parameter, CommandParameter is passed
    /// </summary>
    /// <remarks>
    /// Watches the current View.ActionTarget, and looks for a method with the given name, calling it when the ICommand is called.
    /// If a bool property with name Get(methodName) exists, it will be observed and used to enable/disable the ICommand.
    /// </remarks>
    public class CommandAction : ActionBase, ICommand
    {
        /// <summary>
        /// Generated accessor to grab the value of the guard property, or null if there is none
        /// </summary>
        private Func<bool>? _guardPropertyGetter;

        /// <summary>
        /// Initialises a new instance of the <see cref="CommandAction"/> class to use <see cref="View.ActionTargetProperty"/> to get the target
        /// </summary>
        /// <param name="subject">View to grab the View.ActionTarget from</param>
        /// <param name="backupSubject">Backup subject to use if no ActionTarget could be retrieved from the subject</param>
        /// <param name="methodName">Method name. the MyMethod in Buttom Command="{s:Action MyMethod}".</param>
        /// <param name="targetNullBehaviour">Behaviour for it the relevant View.ActionTarget is null</param>
        /// <param name="actionNonExistentBehaviour">Behaviour for if the action doesn't exist on the View.ActionTarget</param>
        public CommandAction(DependencyObject subject, DependencyObject? backupSubject, string methodName, ActionUnavailableBehaviour targetNullBehaviour, ActionUnavailableBehaviour actionNonExistentBehaviour)
            : base(subject, backupSubject, methodName, targetNullBehaviour, actionNonExistentBehaviour)
        { }

        /// <summary>
        /// Initialises a new instance of the <see cref="CommandAction"/> class to use an explicit target
        /// </summary>
        /// <param name="target">Target to find the method on</param>
        /// <param name="methodName">Method name. the MyMethod in Buttom Command="{s:Action MyMethod}".</param>
        /// <param name="targetNullBehaviour">Behaviour for it the relevant View.ActionTarget is null</param>
        /// <param name="actionNonExistentBehaviour">Behaviour for if the action doesn't exist on the View.ActionTarget</param>
        public CommandAction(object target, string methodName, ActionUnavailableBehaviour targetNullBehaviour, ActionUnavailableBehaviour actionNonExistentBehaviour)
            : base(target, methodName, targetNullBehaviour, actionNonExistentBehaviour)
        { }

        private string GuardName => $"Can{MethodName}";

        /// <summary>
        /// Invoked when a new non-null target is set, which has non-null MethodInfo. Used to assert that the method signature is correct
        /// </summary>
        /// <param name="targetMethodInfo">MethodInfo of method on new target</param>
        /// <param name="newTargetType">Type of new target</param>
        private protected override void AssertTargetMethodInfo(MethodInfo targetMethodInfo, Type newTargetType)
        {
            var methodParameters = targetMethodInfo.GetParameters();
            if (methodParameters.Length > 1)
            {
                var e = new InvalidOperationException($"Method {MethodName} on {newTargetType.Name} must have zero or one parameters");
                throw e;
            }
        }

        /// <summary>
        /// Invoked when a new target is set, after all other action has been taken
        /// </summary>
        /// <param name="oldTarget">Previous target</param>
        /// <param name="newTarget">New target</param>
        private protected override void OnTargetChanged(object oldTarget, object? newTarget)
        {
            if (oldTarget is INotifyPropertyChanged oldInpc)
                PropertyChangedEventManager.RemoveHandler(oldInpc, PropertyChangedHandler, GuardName);

            _guardPropertyGetter = null;
            var guardPropertyInfo = newTarget?.GetType().GetProperty(GuardName);
            if (guardPropertyInfo != null)
            {
                if (guardPropertyInfo.PropertyType == typeof(bool))
                {
                    var targetExpression = Expressions.Expression.Constant(newTarget);
                    var propertyAccess = Expressions.Expression.Property(targetExpression, guardPropertyInfo);
                    _guardPropertyGetter = Expressions.Expression.Lambda<Func<bool>>(propertyAccess).Compile();
                }
            }

            if (_guardPropertyGetter != null)
            {
                if (newTarget is INotifyPropertyChanged inpc)
                    PropertyChangedEventManager.AddHandler(inpc, PropertyChangedHandler, GuardName);
            }

            UpdateCanExecute();
        }

        private void PropertyChangedHandler(object? sender, PropertyChangedEventArgs e)
        {
            UpdateCanExecute();
        }

        private void UpdateCanExecute()
        {
            if (CanExecuteChanged != null)
            {
                Action action = () => CanExecuteChanged.Invoke(this, EventArgs.Empty);
                // So. While we're safe firing PropertyChanged events on a non-UI thread, we
                // are not safe firing CanExecuteChanged events on other threads...
                // Therefore make sure we're on the UI thread
                Application.Current.Dispatcher.BeginInvoke(action);
            }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object? parameter)
        {
            // This can happen if the ActionTarget hasn't been set from its default - 
            // e.g. if the button/etc in question is in a ContextMenu/Popup, which attached properties
            // aren't inherited by.
            // Show the control as enabled, but throw if they try and click on it
            if (Target == View.InitialActionTarget)
                return true;

            // It's enabled only if both the targetNull and actionNonExistent tests pass

            // Throw is handled when the target is set
            if (Target == null)
                return TargetNullBehaviour != ActionUnavailableBehaviour.Disable;

            // Throw is handled when the target is set
            if (TargetMethodInfo == null)
                return ActionNonExistentBehaviour != ActionUnavailableBehaviour.Disable;

            if (_guardPropertyGetter == null)
                return true;

            return _guardPropertyGetter();
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// The method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object? parameter)
        {
            AssertTargetSet();

            // Any throwing would have been handled prior to this
            if (Target == null || TargetMethodInfo == null)
                return;

            // This is not going to be called very often, so don't bother to generate a delegate, in the way that we do for the method guard
            object?[]? parameters = TargetMethodInfo.GetParameters().Length == 1 ? new[] { parameter } : null;
            InvokeTargetMethod(parameters);
        }
    }
}
