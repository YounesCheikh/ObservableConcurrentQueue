// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableConcurrentQueue.cs" company="Prioricy">
//   This work is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
//   To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-sa/4.0/.
// </copyright>
// <Author>
// Cheikh Younes
// </Author>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable once CheckNamespace
namespace System.Collections.Concurrent
{
    /// <summary>
    /// The observable concurrent queue.
    /// </summary>
    /// <typeparam name="T">
    /// The content type
    /// </typeparam>
    public sealed class ObservableConcurrentQueue<T> : ConcurrentQueue<T>
    {
        /// <summary>
        /// The is thread safe
        /// </summary>
        private readonly bool _isThreadSafe;

        /// <summary>
        /// The enqueue synchronize object
        /// </summary>
        private readonly object _enqueueSyncObj = new object();

        /// <summary>
        /// The dequeue synchronize object
        /// </summary>
        private readonly object _dequeueSyncObj = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableConcurrentQueue{T}"/> class.
        /// </summary>
        /// <param name="isThreadSafe">if set to <c>true</c> [is thread safe].</param>
        public ObservableConcurrentQueue(bool isThreadSafe = false)
        {
            _isThreadSafe = isThreadSafe;
        }

        #region Public Events

        /// <summary>
        ///     Occurs when concurrent queue elements [changed].
        /// </summary>
        public event ConcurrentQueueChangedEventHandler<T> ContentChanged;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds an object to the end of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add to the end of the <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1"/>
        ///     . The value can be a null reference (Nothing in Visual Basic) for reference types.
        /// </param>
        public new void Enqueue(T item)
        {
            if(_isThreadSafe)
            {
                lock (_enqueueSyncObj)
                {
                    EnqueueItem(item);
                }
            }
            else
            {
                EnqueueItem(item);
            }
        }

        
        /// <summary>
        /// Attempts to remove and return the object at the beginning of the
        ///     <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1"/>.
        /// </summary>
        /// <param name="result">
        /// When this method returns, if the operation was successful, <paramref name="result"/> contains the
        ///     object removed. If no object was available to be removed, the value is unspecified.
        /// </param>
        /// <returns>
        /// true if an element was removed and returned from the beginning of the
        ///     <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1"/> successfully; otherwise, false.
        /// </returns>
        public new bool TryDequeue(out T result)
        {
            if (_isThreadSafe)
            {
                lock (_dequeueSyncObj)
                {
                    return TryDequeueItem(out result);
                }
            }
            else
                return TryDequeueItem(out result);
        }

        /// <summary>
        /// Attempts to return an object from the beginning of the
        ///     <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1"/> without removing it.
        /// </summary>
        /// <param name="result">
        /// When this method returns, <paramref name="result"/> contains an object from the beginning of the
        ///     <see cref="T:System.Collections.Concurrent.ConcurrentQueue`1"/> or an unspecified value if the operation failed.
        /// </param>
        /// <returns>
        /// true if and object was returned successfully; otherwise, false.
        /// </returns>
        public new bool TryPeek(out T result)
        {
            var retValue = base.TryPeek(out result);
            if (retValue)
            {
                // Raise item dequeued event
                this.OnContentChanged(
                    new NotifyConcurrentQueueChangedEventArgs<T>(NotifyConcurrentQueueChangedAction.Peek, result));
            }

            return retValue;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:Changed"/> event.
        /// </summary>
        /// <param name="args">
        /// The <see cref="NotifyConcurrentQueueChangedEventArgs{T}"/> instance containing the event data.
        /// </param>
        private void OnContentChanged(NotifyConcurrentQueueChangedEventArgs<T> args)
        {
            this.ContentChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Enqueues the item.
        /// </summary>
        /// <param name="item">The item.</param>
        private void EnqueueItem(T item)
        {
            base.Enqueue(item);

            // Raise event added event
            this.OnContentChanged(
                new NotifyConcurrentQueueChangedEventArgs<T>(NotifyConcurrentQueueChangedAction.Enqueue, item));
        }

        /// <summary>
        /// Tries the dequeue item.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>true if and object was returned successfully; otherwise, false.</returns>
        private bool TryDequeueItem(out T result)
        {
            if (!base.TryDequeue(out result))
            {
                return false;
            }

            // Raise item dequeued event
            this.OnContentChanged(
                new NotifyConcurrentQueueChangedEventArgs<T>(NotifyConcurrentQueueChangedAction.Dequeue, result));

            if (this.IsEmpty)
            {
                // Raise Queue empty event
                this.OnContentChanged(
                    new NotifyConcurrentQueueChangedEventArgs<T>(NotifyConcurrentQueueChangedAction.Empty));
            }

            return true;
        }


        #endregion
    }
}