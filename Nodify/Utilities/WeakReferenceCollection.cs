using System;
using System.Collections;
using System.Collections.Generic;

namespace Nodify
{
    /// <summary>
    /// A collection of weak references to objects of type <typeparamref name="T"/>.
    /// Automatically removes dead references after a configurable number of additions.
    /// </summary>
    /// <typeparam name="T">The reference type stored in the collection.</typeparam>
    internal class WeakReferenceCollection<T> : IEnumerable<T> where T : class
    {
        private readonly int _cleanupThreshold;
        private readonly List<WeakReference<T>> _references;

        private int _cleanupCounter = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReferenceCollection{T}"/> class.
        /// </summary>
        /// <param name="initialCapacity">Initial capacity of the internal list.</param>
        /// <param name="cleanupThreshold">Number of additions after which cleanup is triggered.</param>
        public WeakReferenceCollection(int initialCapacity, int cleanupThreshold = 32)
        {
            _cleanupThreshold = cleanupThreshold;
            _references = new List<WeakReference<T>>(initialCapacity);
        }

        /// <summary>
        /// Adds a new weak reference to the specified item to the collection.
        /// Automatically triggers cleanup after a set number of additions.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            _references.Add(new WeakReference<T>(item));
            _cleanupCounter++;
            if (_cleanupCounter >= _cleanupThreshold)
            {
                _cleanupCounter = 0;
                Cleanup();
            }
        }

        /// <summary>
        /// Removes all weak references from the collection.
        /// </summary>
        public void Clear()
            => _references.Clear();

        /// <summary>
        /// Returns a fast enumerator that iterates only over live references.
        /// </summary>
        public IEnumerator<T> GetEnumerator() => new Enumerator(_references);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// Removes dead references from the internal list.
        /// </summary>
        private void Cleanup()
        {
            int writeIndex = 0;
            for (int readIndex = 0; readIndex < _references.Count; readIndex++)
            {
                var reference = _references[readIndex];
                if (reference.TryGetTarget(out _))
                {
                    if (writeIndex != readIndex)
                        _references[writeIndex] = reference;
                    writeIndex++;
                }
            }

            if (writeIndex < _references.Count)
            {
                _references.RemoveRange(writeIndex, _references.Count - writeIndex);
            }
        }

        /// <summary>
        /// Struct-based enumerator for <see cref="WeakReferenceCollection{T}"/>.
        /// Efficiently enumerates live references only.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private readonly List<WeakReference<T>> _references;
            private int _index;
            private T? _current;

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> struct.
            /// </summary>
            /// <param name="references">The backing list of weak references.</param>
            public Enumerator(List<WeakReference<T>> references)
            {
                _references = references;
                _index = -1;
                _current = default;
            }

            public T Current => _current!;
            object IEnumerator.Current => _current!;

            public bool MoveNext()
            {
                while (++_index < _references.Count)
                {
                    if (_references[_index].TryGetTarget(out var target))
                    {
                        _current = target;
                        return true;
                    }
                }

                _current = null;
                return false;
            }

            public void Reset()
            {
                _index = -1;
                _current = null;
            }

            public readonly void Dispose() { }
        }
    }

}
