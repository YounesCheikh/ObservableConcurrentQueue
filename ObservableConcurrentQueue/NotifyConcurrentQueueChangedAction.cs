// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableConcurrentQueue.cs" company="Prioricy">
//   This work is licensed under the Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
//   To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-sa/4.0/.
// </copyright>
// <Author>
// Younes Cheikh
// </Author>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable once CheckNamespace
namespace System.Collections.Concurrent
{
    /// <summary>
    ///     The notify concurrent queue changed action.
    /// </summary>
    public enum NotifyConcurrentQueueChangedAction
    {
        /// <summary>
        ///     New Item was added to the queue
        /// </summary>
        Enqueue, 

        /// <summary>
        ///     Item dequeued from the queue
        /// </summary>
        Dequeue, 

        /// <summary>
        ///     Item peeked from the queue without being dequeued.
        /// </summary>
        Peek, 

        /// <summary>
        ///     The last item in the queue was dequed and the queue is empty.
        /// </summary>
        Empty
    }
}