using System;
using System.Reflection;
using System.Windows;

namespace Nodifier.XAML
{
    /// <summary>
    /// Created by ActionExtension, this can return a delegate suitable adding binding to an event, and can call a method on the View.ActionTarget
    /// </summary>
    public class EventAction : ActionBase
    {
        private static readonly MethodInfo[] _invokeCommandMethodInfos = new[]
        {
            typeof(EventAction).GetMethod("InvokeEventArgsCommand", BindingFlags.NonPublic | BindingFlags.Instance),
            typeof(EventAction).GetMethod("InvokeDependencyCommand", BindingFlags.NonPublic | BindingFlags.Instance),
        }!;

        /// <summary>
        /// Type of event handler
        /// </summary>
        private readonly Type _eventHandlerType;

        /// <summary>
        /// Initialises a new instance of the <see cref="EventAction"/> classto use <see cref="View.ActionTargetProperty"/> to get the target
        /// </summary>
        /// <param name="subject">View whose View.ActionTarget we watch</param>
        /// <param name="backupSubject">Backup subject to use if no ActionTarget could be retrieved from the subject</param>
        /// <param name="eventHandlerType">Type of event handler we're returning a delegate for</param>
        /// <param name="methodName">The MyMethod in {s:Action MyMethod}, this is what we call when the event's fired</param>
        /// <param name="targetNullBehaviour">Behaviour for it the relevant View.ActionTarget is null</param>
        /// <param name="actionNonExistentBehaviour">Behaviour for if the action doesn't exist on the View.ActionTarget</param>
        public EventAction(DependencyObject? subject, DependencyObject? backupSubject, Type eventHandlerType, string methodName, ActionUnavailableBehaviour targetNullBehaviour, ActionUnavailableBehaviour actionNonExistentBehaviour)
            : base(subject, backupSubject, methodName, targetNullBehaviour, actionNonExistentBehaviour)
        {
            AssertBehaviours(targetNullBehaviour, actionNonExistentBehaviour);
            _eventHandlerType = eventHandlerType;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="EventAction"/> class to use an explicit target
        /// </summary>
        /// <param name="target">Target to find the method on</param>
        /// <param name="eventHandlerType">Type of event handler we're returning a delegate for</param>
        /// <param name="methodName">The MyMethod in {s:Action MyMethod}, this is what we call when the event's fired</param>
        /// <param name="targetNullBehaviour">Behaviour for it the relevant View.ActionTarget is null</param>
        /// <param name="actionNonExistentBehaviour">Behaviour for if the action doesn't exist on the View.ActionTarget</param>
        public EventAction(object target, Type eventHandlerType, string methodName, ActionUnavailableBehaviour targetNullBehaviour, ActionUnavailableBehaviour actionNonExistentBehaviour)
            : base(target, methodName, targetNullBehaviour, actionNonExistentBehaviour)
        {
            AssertBehaviours(targetNullBehaviour, actionNonExistentBehaviour);
            _eventHandlerType = eventHandlerType;
        }

        private static void AssertBehaviours(ActionUnavailableBehaviour targetNullBehaviour, ActionUnavailableBehaviour actionNonExistentBehaviour)
        {
            if (targetNullBehaviour == ActionUnavailableBehaviour.Disable)
                throw new ArgumentException("Setting NullTarget = Disable is unsupported when used on an Event");
            if (actionNonExistentBehaviour == ActionUnavailableBehaviour.Disable)
                throw new ArgumentException("Setting ActionNotFound = Disable is unsupported when used on an Event");
        }

        /// <summary>
        /// Invoked when a new non-null target is set, which has non-null MethodInfo. Used to assert that the method signature is correct
        /// </summary>
        /// <param name="targetMethodInfo">MethodInfo of method on new target</param>
        /// <param name="newTargetType">Type of new target</param>
        private protected override void AssertTargetMethodInfo(MethodInfo targetMethodInfo, Type newTargetType)
        {
            var methodParameters = targetMethodInfo.GetParameters();
            if (!(methodParameters.Length == 0 ||
                (methodParameters.Length == 1 && (typeof(EventArgs).IsAssignableFrom(methodParameters[0].ParameterType) || methodParameters[0].ParameterType == typeof(DependencyPropertyChangedEventArgs))) ||
                (methodParameters.Length == 2 && (typeof(EventArgs).IsAssignableFrom(methodParameters[1].ParameterType) || methodParameters[1].ParameterType == typeof(DependencyPropertyChangedEventArgs)))))
            {
                var e = new InvalidOperationException($"Method {MethodName} on {newTargetType.Name} must have the signatures Method(), Method(EventArgsOrSubClass e), or Method(object sender, EventArgsOrSubClass e)");
                throw e;
            }
        }

        /// <summary>
        /// Return a delegate which can be added to the targetProperty
        /// </summary>
        /// <returns>An event hander, which, when invoked, will invoke the action</returns>
        public Delegate GetDelegate()
        {
            Delegate? del = null;
            foreach (var invokeCommandMethodInfo in _invokeCommandMethodInfos)
            {
                del = Delegate.CreateDelegate(_eventHandlerType, this, invokeCommandMethodInfo, false);
                if (del != null)
                    break;
            }

            if (del == null)
            {
                string msg = string.Format("Event being bound to does not have a signature we know about. Method {0} on target {1}. Valid signatures are:" +
                    "Valid signatures are:\n" +
                    " - '(object sender, EventArgsOrSubclass e)'\n" +
                    " - '(object sender, DependencyPropertyChangedEventArgs e)'", MethodName, Target);
                var e = new InvalidOperationException(msg);
                throw e;
            }

            return del;
        }

        private void InvokeEventArgsCommand(object sender, EventArgs e)
        {
            InvokeCommand(sender, e);
        }

        // ReSharper disable once UnusedMember.Local
        private void InvokeCommand(object sender, object e)
        {
            AssertTargetSet();

            // Any throwing will have been handled above
            if (Target == null || TargetMethodInfo == null)
                return;

            object[]? parameters = TargetMethodInfo.GetParameters().Length switch
            {
                1 => new object[] { e },
                2 => new[] { sender, e },
                _ => null,
            };
            InvokeTargetMethod(parameters);
        }
    }
}
