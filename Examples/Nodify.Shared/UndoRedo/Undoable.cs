using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Expression = System.Linq.Expressions.Expression;

namespace Nodify.UndoRedo
{
    [Flags]
    public enum PropertyFlags
    {
        Disable = 0,
        Enable = 1
    }

    public abstract class Undoable : ObservableObject
    {
        private readonly HashSet<string> _trackedProperties = new HashSet<string>();

        public IActionsHistory History { get; }

        private void RecordHistory<TPropType>(string propName, TPropType previous, TPropType current)
        {
            if (_trackedProperties.Contains(propName))
            {
                var prop = PropertyCache.Get(GetType(), propName);
                History.Record(() => prop.SetValue(this, current), () => prop.SetValue(this, previous), propName);
            }
        }

        protected void RecordProperty(string propName, PropertyFlags flags = PropertyFlags.Enable)
        {
            if (flags == PropertyFlags.Disable)
            {
                _trackedProperties.Remove(propName);
            }
            else if (flags.HasFlag(PropertyFlags.Enable))
            {
                _trackedProperties.Add(propName);
            }
        }

        protected void RecordProperty<TType>(Expression<Func<TType, object?>> selector, PropertyFlags flags = PropertyFlags.Enable)
        {
            if (!RuntimeFeature.IsDynamicCodeSupported)
                return;

            string name = GetPropertyName(selector);
            RecordProperty(name, flags);
        }

        private static string GetPropertyName(Expression memberAccess)
            => memberAccess switch
            {
                LambdaExpression lambda => GetPropertyName(lambda.Body),
                MemberExpression mbr => mbr.Member.Name,
                UnaryExpression unary => GetPropertyName(unary.Operand),
                _ => throw new Exception($"Member name could not be extracted from {memberAccess}.")
            };

        protected override bool SetProperty<TPropType>(ref TPropType field, TPropType value, [CallerMemberName] string propertyName = "")
        {
            TPropType prev = field;
            if (base.SetProperty(ref field, value, propertyName))
            {
                RecordHistory(propertyName, prev, value);
                return true;
            }

            return false;
        }

        public Undoable()
        {
            History = ActionsHistory.Global;
        }

        public Undoable(IActionsHistory history)
        {
            History = history;
        }
    }
}
