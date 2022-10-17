using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Expression = System.Linq.Expressions.Expression;

namespace Nodifier
{
    [Flags]
    public enum PropertyFlags
    {
        None = 0,
        Serialize = 1,
        TrackHistory = 2
    }

    public abstract class Undoable : PropertyChangedBase
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

        protected void ConfigurePoperty(string propName, PropertyFlags flags)
        {
            if (flags == PropertyFlags.None)
            {
                // TODO: remove from serializable properties list
                _trackedProperties.Remove(propName);
                return;
            }

            if (flags.HasFlag(PropertyFlags.TrackHistory))
            {
                _trackedProperties.Add(propName);
            }

            if (flags.HasFlag(PropertyFlags.Serialize))
            {
                // TODO: add to serializable properties list
            }
        }

        protected void ConfigurePoperty<TType>(Expression<Func<TType, object?>> selector, PropertyFlags flags)
        {
            string name = GetPropertyName(selector);
            ConfigurePoperty(name, flags);
        }

        private static string GetPropertyName(Expression memberAccess)
            => memberAccess switch
            {
                LambdaExpression lambda => GetPropertyName(lambda.Body),
                MemberExpression mbr => mbr.Member.Name,
                UnaryExpression unary => GetPropertyName(unary.Operand),
                _ => throw new Exception("Member name could not be extracted.")
            };

        protected override bool SetAndNotify<TPropType>(ref TPropType field, TPropType value, [CallerMemberName] string propertyName = "")
        {
            TPropType prev = field;
            if (base.SetAndNotify(ref field, value, propertyName))
            {
                RecordHistory(propertyName, prev, value);
                return true;
            }

            return false;
        }

        public Undoable()
        {
            History = new ActionsHistory();
        }

        public Undoable(IActionsHistory history)
        {
            History = history;
        }
    }
}
