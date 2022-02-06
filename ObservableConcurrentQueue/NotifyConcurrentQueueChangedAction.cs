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
        ///     The enqueue
        /// </summary>
        Enqueue, 

        /// <summary>
        ///     The de-queue
        /// </summary>
        Dequeue, 

        /// <summary>
        ///     The peek
        /// </summary>
        Peek, 

        /// <summary>
        ///     The empty
        /// </summary>
        Empty
    }
}