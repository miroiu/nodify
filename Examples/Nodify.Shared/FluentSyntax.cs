using System;
using System.Collections.Generic;
using System.Linq;

namespace Nodify
{
    public static class FluentSyntax
    {
        public static void Then<T>(this T caller, Action<T> action)
            => action?.Invoke(caller);

        public static bool Then(this bool condition, Action action)
        {
            if (condition)
            {
                action();
            }

            return condition;
        }

        public static bool Else(this bool condition, Action action)
        {
            if (!condition)
            {
                action();
            }

            return condition;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection is IList<T> list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    action(list[i]);
                }
            }
            else
            {
                foreach (var item in collection)
                {
                    action(item);
                }
            }

            return collection;
        }

        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            values.ForEach(collection.Add);
            return collection;
        }

        public static ICollection<T> RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            values.ForEach(v => collection.Remove(v));
            return collection;
        }

        public static ICollection<T> RemoveOne<T>(this ICollection<T> collection, Func<T, bool> search)
        {
            if (collection.FirstOrDefault(search) is { } x)
            {
                collection.Remove(x);
            }

            return collection;
        }
    }
}
