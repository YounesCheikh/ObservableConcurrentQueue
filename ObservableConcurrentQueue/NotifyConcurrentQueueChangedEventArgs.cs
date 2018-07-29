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
    /// The notify concurrent queue changed event args.
    /// </summary>
    /// <typeparam name="T">
    /// The item type
    /// </typeparam>
    public class NotifyConcurrentQueueChangedEventArgs<T> : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyConcurrentQueueChangedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="changedItem">
        /// The changed item.
        /// </param>
        public NotifyConcurrentQueueChangedEventArgs(NotifyConcurrentQueueChangedAction action, T changedItem)
        {
            this.Action = action;
            this.ChangedItem = changedItem;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyConcurrentQueueChangedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        public NotifyConcurrentQueueChangedEventArgs(NotifyConcurrentQueueChangedAction action)
        {
            this.Action = action;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the action.
        /// </summary>
        /// <value>
        ///     The action.
        /// </value>
        public NotifyConcurrentQueueChangedAction Action { get; private set; }

        /// <summary>
        ///     Gets the changed item.
        /// </summary>
        /// <value>
        ///     The changed item.
        /// </value>
        public T ChangedItem { get; private set; }

        #endregion
    }
}