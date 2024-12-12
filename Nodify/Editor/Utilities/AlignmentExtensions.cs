using System.Collections.Generic;
using System;
using System.Linq;
using System.Windows;

namespace Nodify
{
    internal static class AlignmentExtensions
    {
        public static void Align(this IEnumerable<ItemContainer> values, Alignment alignment, ItemContainer? relativeTo)
        {
            var containers = values as IReadOnlyCollection<ItemContainer> ?? values.ToList();
            switch (alignment)
            {
                case Alignment.Top:
                    AlignTop(containers, relativeTo);
                    break;

                case Alignment.Left:
                    AlignLeft(containers, relativeTo);
                    break;

                case Alignment.Bottom:
                    AlignBottom(containers, relativeTo);
                    break;

                case Alignment.Right:
                    AlignRight(containers, relativeTo);
                    break;

                case Alignment.Middle:
                    AlignMiddle(containers, relativeTo);
                    break;

                case Alignment.Center:
                    AlignCenter(containers, relativeTo);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null);
            }
        }

        private static void AlignTop(IReadOnlyCollection<ItemContainer> containers, ItemContainer? instigator)
        {
            double top = instigator?.Location.Y ?? containers.Min(x => x.Location.Y);
            foreach (var c in containers)
            {
                c.Location = new Point(c.Location.X, top);
            }
        }

        private static void AlignLeft(IReadOnlyCollection<ItemContainer> containers, ItemContainer? instigator)
        {
            double left = instigator?.Location.X ?? containers.Min(x => x.Location.X);
            foreach (var c in containers)
            {
                c.Location = new Point(left, c.Location.Y);
            }
        }

        private static void AlignBottom(IReadOnlyCollection<ItemContainer> containers, ItemContainer? instigator)
        {
            double bottom = instigator != null ? instigator.Location.Y + instigator.ActualHeight : containers.Max(x => x.Location.Y + x.ActualHeight);
            foreach (var c in containers)
            {
                c.Location = new Point(c.Location.X, bottom - c.ActualHeight);
            }
        }

        private static void AlignRight(IReadOnlyCollection<ItemContainer> containers, ItemContainer? instigator)
        {
            double right = instigator != null ? instigator.Location.X + instigator.ActualWidth : containers.Max(x => x.Location.X + x.ActualWidth);
            foreach (var c in containers)
            {
                c.Location = new Point(right - c.ActualWidth, c.Location.Y);
            }
        }

        private static void AlignMiddle(IReadOnlyCollection<ItemContainer> containers, ItemContainer? instigator)
        {
            double mid = instigator != null ? instigator.Location.Y + instigator.ActualHeight / 2 : containers.Average(c => c.Location.Y + c.ActualHeight / 2);
            foreach (var c in containers)
            {
                c.Location = new Point(c.Location.X, mid - c.ActualHeight / 2);
            }
        }

        private static void AlignCenter(IReadOnlyCollection<ItemContainer> containers, ItemContainer? instigator)
        {
            double center = instigator != null ? instigator.Location.X + instigator.ActualWidth / 2 : containers.Average(c => c.Location.X + c.ActualWidth / 2);
            foreach (var c in containers)
            {
                c.Location = new Point(center - c.ActualWidth / 2, c.Location.Y);
            }
        }
    }
}
